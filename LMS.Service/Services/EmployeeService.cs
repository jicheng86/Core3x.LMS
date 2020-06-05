using LMS.IRepository;
using LMS.IRepository.IRepositories;
using LMS.IService.IServices;
using LMS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Service.Services
{
    public class EmployeeService : ServiceBase<Employee>, IEmployeeService
    {
        public EmployeeService(IEmployeeRepository repository) : base(repository)
        {
        }
    }
}
