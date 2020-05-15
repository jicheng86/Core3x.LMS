using LMS.IRepository;
using LMS.IService.IServices;
using LMS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Service.Services
{
    public class EmployeeService : ServiceBase<Employee>, IEmployeeService
    {
        public EmployeeService(IRepositoryBase<Employee> repository) : base(repository)
        {
        }
    }
}
