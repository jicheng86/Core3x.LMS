using LMS.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.IService
{
    public interface IServiceBase<T> where T : class
    {
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        T Create(T entity);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="whereLambda">Lambda查询条件</param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Delete(T entity);
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        T Update(T entity);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="whereLambda">Lambda查询条件</param>
        /// <returns></returns>
        T GetEntity(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="whereLambda">Lambda查询条件</param>
        /// <returns></returns>
        Task<T> GetEntityAsync(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="whereLambda">Lambda查询条件</param>
        /// <returns></returns>
        Task<IQueryable<T>> GetEntityListAsync(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="whereLambda">lambda查询条件</param>
        /// <param name="orderLambda">Lambda排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="total">查询数据总条数</param>
        /// <returns>分页数据集合</returns>
        Task<PageData<T>> LoadPageDataListAsync(Expression<Func<T, bool>> whereLambda, Expression<Func<T, object>> orderLambda, int pageIndex, int pageSize, bool isDesc = false);

        /// <summary>
        /// 数据响应提交
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangeAsync();
    }
}
