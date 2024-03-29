﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using LMS.IRepository;
using LMS.Model;
using LMS.Model.Dto;
using LMS.Model.Entities;

namespace LMS.IRepository.IRepositories
{
    public interface ICorporationRepository : IRepositoryBase<Corporation>
    {
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="whereLambda">lambda查询条件</param>
        /// <param name="orderLambda">Lambda排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="total">查询数据总条数</param>
        /// <returns>分页数据集合</returns>
        PageData<CorporationDto> LoadPageDataList(Expression<Func<Corporation, bool>> whereLambda, Expression<Func<Corporation, object>> orderLambda, int pageIndex, int pageSize, bool isDesc = false);
    }
}
