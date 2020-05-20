using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;

namespace LMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        public ILogger<DefaultController> Logger { get; }

        public DefaultController(ILogger<DefaultController> logger)
        {
            Logger = logger;
        }
        // GET: api/Default
        [HttpGet]
        public IEnumerable<string> Get()
        {
            ControllerActionDescriptor actionDescriptor = ControllerContext.ActionDescriptor;
            Logger.LogDebug(message: $"记录LogDebug日志,发生在{actionDescriptor.ControllerName}/{actionDescriptor.ActionName}");
            Logger.LogInformation(message: $"记录LogInformation日志,发生在{actionDescriptor.ControllerName}/{actionDescriptor.ActionName}");
            Logger.LogError(message: $"记录LogError日志,发生在{actionDescriptor.ControllerName}/{actionDescriptor.ActionName}");
            Logger.LogWarning(message: $"记录LogWarning日志,发生在{actionDescriptor.ControllerName}/{actionDescriptor.ActionName}");
            Logger.LogCritical(message: $"记录LogCritical日志,发生在{actionDescriptor.ControllerName}/{actionDescriptor.ActionName}");
            return new string[] { "value1", "value2" };
        }

        // GET: api/Default/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Default
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
