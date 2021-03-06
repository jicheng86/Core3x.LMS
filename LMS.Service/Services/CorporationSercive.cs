﻿using AutoMapper;

using LMS.IRepository;
using LMS.IRepository.IRepositories;
using LMS.IService.IServices;
using LMS.Model;
using LMS.Model.Dto;
using LMS.Model.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Services
{
    public class CorporationService : ServiceBase<Corporation>, ICorporationService
    {
        public CorporationService(ICorporationRepository repository) : base(repository)
        {
            corporationRepository = repository;
        }

        public ICorporationRepository corporationRepository { get; }

        public PageData<CorporationDto> LoadPageDataList(Expression<Func<Corporation, bool>> whereLambda,
                                                         Expression<Func<Corporation, object>> orderLambda,
                                                         int pageIndex, int pageSize, bool isDesc = false)
        {
            return corporationRepository.LoadPageDataList(whereLambda, orderLambda, pageIndex, pageSize, isDesc);
        }
        /// <summary>
        /// 是否已存在
        /// </summary>
        /// <param name="corporation">实体</param>
        /// <returns></returns>
        public bool IsExisted(Corporation corporation)
        {
            if (corporation == null)
            {
                return true;
            }
            if (IsExisted(c => c.Name == corporation.Name && c.IsDeleted == false && c.IsActive == true))
            {
                return true;
            }
            return false;
        }
    }
}
