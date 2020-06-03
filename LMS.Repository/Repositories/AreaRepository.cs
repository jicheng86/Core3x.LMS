using LMS.IRepository.IRepositories;
using LMS.Model.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Repository.Repositories
{
    public class AreaRepository : RepositoryBase<Area>, IAreaRepository
    {
        public AreaRepository(EFDbContext context) : base(context)
        {
        }
    }
}
