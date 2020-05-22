using LMS.IRepository;
using LMS.IService.IServices;
using LMS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Service.Services
{
    public class PositionService : ServiceBase<Position>, IPositionService
    {
        public PositionService(IRepositoryBase<Position> repository) : base(repository)
        {
        }
    }
}
