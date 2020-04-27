using LMS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace LMS.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>
    {
        public EmployeeRepository(EFDbContext eFDbContext) : base(eFDbContext)
        {
        }
        public int GetInf()
        {
            dbContext.Entry(new Employee()).State = EntityState.Modified;
           // dbContext.Employees.FirstOrDefaultAsync();
            return 0;
        }

    }
}
