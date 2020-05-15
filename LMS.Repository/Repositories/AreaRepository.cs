using LMS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Repository.Repositories
{
    public class AreaRepository : RepositoryBase<Area>
    {
        public AreaRepository(EFDbContext context) : base(context)
        {
        }
    }
}
