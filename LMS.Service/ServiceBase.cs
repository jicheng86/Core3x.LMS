using LMS.IRepository;
using LMS.IService;
using LMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service
{
    public abstract class ServiceBase<T> : IServiceBase<T> where T : class, new()
    {
        protected IRepositoryBase<T> RepositoryBase { get; set; }

        public T Create(T entity)
        {
            return RepositoryBase.Create(entity);
        }

        public bool Delete(Expression<Func<T, bool>> whereLambda)
        {
            return RepositoryBase.Delete(whereLambda);
        }

        public bool Delete(T entity)
        {
            return RepositoryBase.Delete(entity);
        }

        public T GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetEntityAsync(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> GetEntityListAsync(Expression<Func<T, bool>> whereLambda)
        {
            throw new NotImplementedException();
        }

        public Task<PageData<T>> LoadPageDataListAsync(Expression<Func<T, bool>> whereLambda, Expression<Func<T, object>> orderLambda, int pageIndex, int pageSize, bool isDesc = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangeAsync()
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
