using LMS.IRepository;
using LMS.IService.IServices;
using LMS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Service.Services
{
    public class DepartmentService : ServiceBase<Department>, IDepartmentService
    {
        public DepartmentService(IRepositoryBase<Department> repository) : base(repository)
        {
        }
    }
}
