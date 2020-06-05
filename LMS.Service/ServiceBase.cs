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
        /// <summary>
        /// 通过在子类的构造函数中注入，这里是基类，不用构造函数
        /// 构造函数注入
        /// </summary>
        protected IRepositoryBase<T> Repository { get; set; }

        public ServiceBase(IRepositoryBase<T> repository)
        {
            Repository = repository;
        }

        public T Create(T entity)
        {
            return Repository.Create(entity);
        }

        public bool Delete(Expression<Func<T, bool>> whereLambda)
        {
            return Repository.Delete(whereLambda);
        }

        public bool Delete(T entity)
        {
            return Repository.Delete(entity);
        }

        public T GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            return Repository.GetEntity(whereLambda);
        }

        public Task<T> GetEntityAsync(Expression<Func<T, bool>> whereLambda)
        {
            return Repository.GetEntityAsync(whereLambda);
        }

        public Task<IQueryable<T>> GetEntityListAsync(Expression<Func<T, bool>> whereLambda)
        {
            return Repository.GetEntityListAsync(whereLambda);
        }

     

        public Task<int> SaveChangeAsync()
        {
            return Repository.SaveChangeAsync();
        }

        public T Update(T entity)
        {
            return Repository.Update(entity);
        }
    }
}
