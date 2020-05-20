using LMS.IService;
using LMS.Model.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS.Service
{
    /// <summary>
    /// token认证服务
    /// </summary>
    public class JwtAuthenticationService : IJwtAuthenticateService
    {
        public JwtOptions JwtOptions { get; }

        public bool IsAuthenticated(RequestDto request, JwtOptions JwtOptions, out string jwtString)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var claims = new[] { new Claim(ClaimTypes.Name, request.Name) };
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Secret));
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: JwtOptions.Issuer,
                                                                     audience: JwtOptions.Audience,
                                                                     claims: claims,
                                                                     expires: DateTime.Now.AddMinutes(JwtOptions.AccessExpiration),
                                                                     signingCredentials: credentials
                                                                     );
            jwtString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return true;
        }
    }
}
