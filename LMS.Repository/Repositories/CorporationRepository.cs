using AutoMapper;
using AutoMapper.QueryableExtensions;

using LMS.IRepository.IRepositories;
using LMS.Model;
using LMS.Model.Dto;
using LMS.Model.Entities;

using Microsoft.EntityFrameworkCore.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repository.Repositories
{
    public class CorporationRepository : RepositoryBase<Corporation>, ICorporationRepository
    {
        public CorporationRepository(EFDbContext context, IMapper mapper) : base(context)
        {
            Mapper = mapper;
        }

        public IMapper Mapper { get; }

        public PageData<CorporationDto> LoadPageDataList(Expression<Func<Corporation, bool>> whereLambda,
                                                         Expression<Func<Corporation, object>> orderLambda,
                                                         int pageIndex, int pageSize, bool isDesc = false)
        {
            var dataSource = dbContext.Corporations.Where(w => true);
            if (whereLambda != null)
            {
                dataSource = dataSource.Where(whereLambda);
            }
            PageData<CorporationDto> resualData = new PageData<CorporationDto>();
            if (dataSource == null || !dataSource.Any())
            {
                return resualData;
            }
            if (orderLambda != null)
            {
                dataSource = isDesc ? dataSource.OrderByDescending(orderLambda) : dataSource.OrderByDescending(orderLambda);
            }
            resualData.Total = dataSource.Count();
            dataSource = dataSource.Skip(pageIndex).Take(pageSize);
            resualData.Data = dataSource.ProjectTo<CorporationDto>(configuration: Mapper.ConfigurationProvider).ToList();
            //pageData.Data = Mapper.ProjectTo<CorporationDto>(dataSource).ToList();
            return resualData;
        }


    }
}
