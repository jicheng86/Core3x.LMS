using LMS.Model;
using LMS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Repository.Repositories
{
    public class CorporationRepository : RepositoryBase<Corporation>
    {
        public CorporationRepository(EFDbContext context) : base(context)
        {
        }
    }
}
