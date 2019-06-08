using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using ItSys.Helper;
using ItSys.Dto;
using ItSys.Common;
using ItSys.ApiGroup;
using ItSys.Service;

namespace ItSys.Controllers
{
    /// <summary>
    /// 用户认证
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    [ApiGroup(ApiGroupNames.Auth)]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;
        public AuthController(AuthService service)
        {
            _service = service;
        }
        /// <summary>
        /// 验证登录名及登录密码及返回token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ResultDto<string> Validate([FromForm]LoginDto login)
        {
            var userId = _service.LoginValidate(login);
            return JwtHelper.CreateToken(userId);
        }
        /// <summary>
        /// 根据token获取用户信息及菜单数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultDto<UserInfoDto> GetUserInfo()
        {
            return _service.GetUserInfo();
        }
        [HttpGet]
        public ResultDto<String> NewGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}