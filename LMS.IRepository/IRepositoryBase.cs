using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LMS.IRepository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    public interface IRepositoryBase<T> where T : class, new()
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
        /// <param name="ID">实体主键ID</param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> whereLambda);
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
        Task<IQueryable<T>> LoadPageDataListAsync(Expression<Func<T, bool>> whereLambda, Expression<Func<T, object>> orderLambda, int pageIndex, int pageSize, ref int total);

        /// <summary>
        /// 数据响应提交
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangeAsync();
    }
}
