using LMS.IRepository;
using LMS.IService.IServices;
using LMS.Model.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Service.Services
{
    public class CorporationSercive : ServiceBase<Corporation>, ICorporationService
    {
        public CorporationSercive(IRepositoryBase<Corporation> repository) : base(repository)
        {
        }
    }
}
