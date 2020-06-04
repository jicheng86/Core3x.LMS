using LMS.IRepository.IRepositories;
using LMS.Model;
using LMS.Model.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Repository.Repositories
{
    public class CorporationRepository : RepositoryBase<Corporation>, ICorporationRepository
    {
        public CorporationRepository(EFDbContext context) : base(context)
        {
        }
    }
}
