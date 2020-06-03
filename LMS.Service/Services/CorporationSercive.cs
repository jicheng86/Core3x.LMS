using LMS.IRepository;
using LMS.IRepository.IRepositories;
using LMS.IService.IServices;
using LMS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Service.Services
{
    public class CorporationSercive : ServiceBase<Corporation>, ICorporationService
    {
        public CorporationSercive(ICorporationRepository repository)
        {
            base.Repository = repository;
        }
    }
}
