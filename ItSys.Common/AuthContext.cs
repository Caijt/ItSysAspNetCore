using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ItSys.Common
{
    public class AuthContext
    {
        public int UserId { get; set; }
        public IPAddress LoginIP { get; set; }
        public bool IsLogin
        {
            get
            {
                return UserId != 0;
            }
        }
        public AuthContext(IHttpContextAccessor httpContextAccessor)
        {
            var uid = httpContextAccessor.HttpContext.User.FindFirst("uid")?.Value;
            if (uid != null)
            {
                UserId = int.Parse(uid);
            }
            LoginIP = httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
        }
    }
}
