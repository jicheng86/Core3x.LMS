using LMS.Model;
using LMS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Repository.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>
    {
        public DepartmentRepository(EFDbContext context) : base(context)
        {
        }
    }
}
