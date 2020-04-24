using LMS.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        private readonly DbContext dbContext;

        /// <summary>
        /// 实现类初始化时：初始化数据库上下文
        /// </summary>
        /// <param name="context"></param>
        public RepositoryBase(DbContext context)
        {
            this.dbContext = context;
        }

        public T Create(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return entity;
        }

        public bool Delete(Expression<Func<T, bool>> whereLambda)
        {
            T entity = GetEntity(whereLambda);
            //context.Set<T>()
            return entity == null ? false : Update(entity) != null;
        }

        public T GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            T entity = dbContext.Set<T>().Where(whereLambda).FirstOrDefault();
            return entity;
        }

        public Task<T> GetEntityAsync(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> GetEntityListAsync(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> LoadPageDataListAsync(Expression<Func<T, bool>> whereLambda, Expression<Func<T, object>> orderLambda, int pageIndex, int pageSize, ref int total)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
