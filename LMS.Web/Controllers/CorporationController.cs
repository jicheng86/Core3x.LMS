using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.IService.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Controllers
{
    public class CorporationController : Controller
    {
        public ICorporationService CorporationService { get; }

        public CorporationController(ICorporationService corporationService)
        {
            CorporationService = corporationService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CorporationList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Creation()
        {
            return View();
        }
    }
}