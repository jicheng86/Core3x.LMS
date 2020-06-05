using LMS.IRepository;
using LMS.IRepository.IRepositories;
using LMS.IService.IServices;
using LMS.Model;
using LMS.Model.Entities;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Services
{
    public class AreaService : ServiceBase<Area>, IAreaService
    {
        public AreaService(IAreaRepository repository) : base(repository)
        {
        }

        public Task<PageData<Area>> LoadPageDataListAsync2(Expression<Func<Area, bool>> whereLambda, Expression<Func<Area, object>> orderLambda, int pageIndex, int pageSize, bool isDesc = false)
        {
            throw new NotImplementedException();
        }
    }
}
