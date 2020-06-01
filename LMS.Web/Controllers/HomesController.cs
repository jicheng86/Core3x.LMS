using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace LMS.Web.Controllers
{
    public class HomesController : Controller
    {
        private readonly ILogger<HomesController> _logger;

        public HomesController(ILogger<HomesController> logger)
        {
            _logger = logger;
        }
        public IActionResult Home()
        {
            ControllerActionDescriptor actionDescriptor = ControllerContext.ActionDescriptor;
            _logger.LogDebug(message: $"记录LogDebug日志,发生在{actionDescriptor.ControllerName}/{actionDescriptor.ActionName}");
            _logger.LogInformation(message: $"记录LogInformation日志,发生在{actionDescriptor.ControllerName}/{actionDescriptor.ActionName}");
            _logger.LogError(message: $"记录LogError日志,发生在{actionDescriptor.ControllerName}/{actionDescriptor.ActionName}");
            _logger.LogWarning(message: $"记录LogWarning日志,发生在{actionDescriptor.ControllerName}/{actionDescriptor.ActionName}");
            _logger.LogCritical(message: $"记录LogCritical日志,发生在{actionDescriptor.ControllerName}/{actionDescriptor.ActionName}");
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index_v2()
        {
            return View();
        }
        public IActionResult Layouts()
        {
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
