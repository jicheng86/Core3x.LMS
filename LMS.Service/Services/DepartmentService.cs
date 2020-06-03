using LMS.IRepository;
using LMS.IRepository.IRepositories;
using LMS.IService.IServices;
using LMS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Service.Services
{
    public class DepartmentService : ServiceBase<Department>, IDepartmentService
    {
        public DepartmentService(IDepartmentRepository repository)
        {
            base.Repository = repository;
        }
    }
}
