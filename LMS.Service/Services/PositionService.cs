using LMS.IRepository;
using LMS.IRepository.IRepositories;
using LMS.IService.IServices;
using LMS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Service.Services
{
    public class PositionService : ServiceBase<Position>, IPositionService
    {
        public PositionService(IPositionRepository repository)
        {
            base.Repository = repository;
        }
    }
}
