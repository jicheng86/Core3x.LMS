using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LMS.Model;
using LMS.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using static LMS.Model.Enums.EnumCollection;

namespace LMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        [Route("api/login")]
        public IActionResult Login([FromBody] Employee employee)
        {
            //从数据库验证用户名，密码 
            //验证通过 否则 返回Unauthorized

            //创建claim
            var authClaims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,employee.Name),
                new Claim(JwtRegisteredClaimNames.Jti,employee.CreatorUserId.ToString())
            };
            IdentityModelEventSource.ShowPII = true;
            //签名秘钥 可以放到json文件中
            var SigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jicheng"));

            JwtSecurityToken securityToken = new JwtSecurityToken(
                   issuer: "suer",
                   audience: "audience",
                   expires: DateTime.Now.AddHours(2),
                   claims: authClaims,
                   signingCredentials: new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256)
                   );

            //返回token和过期时间
            JsonData jsonData = new JsonData
            {
                Data = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                    expiration = securityToken.ValidTo
                },
                Code = RespondStatusCode.Success,
                Message = "操作成功"
            };

            return Ok(jsonData);
        }
    }
}