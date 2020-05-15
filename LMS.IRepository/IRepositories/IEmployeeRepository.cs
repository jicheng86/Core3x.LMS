using LMS.Model;
using LMS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.IRepository.IRepositories
{
    /// <summary>
    /// 员工仓储接口
    /// </summary>
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        Task<Employee> DoSelf(Employee entity);
    }
}
