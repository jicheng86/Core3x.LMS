using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.IService;
using LMS.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LMS.WebApi.Controllers
{
    [Route("api/JwtAuthentication")]
    [ApiController]
    public class JwtAuthenticationController : ControllerBase
    {
        private readonly ILogger<JwtAuthenticationController> logger;
        private readonly IJwtAuthenticateService jwtAuthenticateService;
        public JwtAuthenticationController(ILogger<JwtAuthenticationController> logger, IJwtAuthenticateService jwtAuthenticateService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.jwtAuthenticateService = jwtAuthenticateService ?? throw new ArgumentNullException(nameof(jwtAuthenticateService));
        }
        [AllowAnonymous]
        [HttpPost(), Route("requestToken")]
        public ActionResult RequestToken([FromBody] RequestDto request)
        {
            var ActionDescriptor = ControllerContext.ActionDescriptor;
            logger.LogDebug("LogDebug日志，请求地址：" + ActionDescriptor.ControllerName + "/" + ActionDescriptor.ActionName);
            logger.LogInformation("LogInformation日志，请求地址：" + ActionDescriptor.ControllerName + "/" + ActionDescriptor.ActionName);
            logger.LogWarning("ogWarning日志，请求地址：" + ActionDescriptor.ControllerName + "/" + ActionDescriptor.ActionName);
            logger.LogError("LogError日志，请求地址：" + ActionDescriptor.ControllerName + "/" + ActionDescriptor.ActionName);
            logger.LogCritical("LogError日志，请求地址：" + ActionDescriptor.ControllerName + "/" + ActionDescriptor.ActionName);
            logger.LogTrace("LogError日志，请求地址：" + ActionDescriptor.ControllerName + "/" + ActionDescriptor.ActionName);
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            string token;
            if (jwtAuthenticateService.IsAuthenticated(request, out token))
            {
                return Ok(token);
            }

            return BadRequest("Invalid Request");
        }

    }
}