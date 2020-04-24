using LMS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace LMS.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>
    {
        public EmployeeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
