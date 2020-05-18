using LMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.IService
{
    public interface IJwtAuthenticateService
    {
        /// <summary>
        /// 当前访问是否已授权jwt
        /// </summary>
        /// <param name="request"></param>
        /// <param name="jwtString"></param>
        /// <returns></returns>
        bool IsAuthenticated(RequestDto request, out string jwtString);
    }
}
