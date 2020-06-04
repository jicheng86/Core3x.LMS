using LMS.IRepository.IRepositories;
using LMS.Model;
using LMS.Model.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Repository.Repositories
{
    public class PositionRepositiry : RepositoryBase<Position>, IPositionRepository
    {
        public PositionRepositiry(EFDbContext context) : base(context)
        {
        }
    }
}
