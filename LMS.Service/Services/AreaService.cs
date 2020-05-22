using LMS.IRepository;
using LMS.IService.IServices;
using LMS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Service.Services
{
    public class AreaService : ServiceBase<Area>, IAreaService
    {
        public AreaService(IRepositoryBase<Area> repository) : base(repository)
        {
        }
    }
}
