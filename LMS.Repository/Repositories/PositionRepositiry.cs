using LMS.Model;
using LMS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Repository.Repositories
{
    class PositionRepositiry : RepositoryBase<Position>
    {
        public PositionRepositiry(EFDbContext context) : base(context)
        {
        }
    }
}
