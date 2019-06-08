using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ItSys.Dto;
using ItSys.Service;

namespace ItSys.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class IndexController : ControllerBase
    {
        private readonly SysUpdateRecordService _updateRecordService;
        private readonly SysUserLoginLogService _loginLogService;
        private readonly AuthService _authService;
        public IndexController(
            SysUpdateRecordService updateRecordService,
            SysUserLoginLogService loginLogService,
            AuthService authService)
        {
            _updateRecordService = updateRecordService;
            _loginLogService = loginLogService;
            _authService = authService;
        }
        /// <summary>
        /// 获取更新列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultDto<PageListDto<SysUpdateRecordDto>> GetUpdateRecordList([FromQuery]SysUpdateRecordQueryDto dto)
        {
            return _updateRecordService.GetPageList(dto);
        }
        /// <summary>
        /// 获取用户登录日志列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultDto<PageListDto<SysUserLoginLogDto>> GetLoginLogList([FromQuery]SysUserLoginLogQueryDto dto)
        {

            return _loginLogService.GetPageList(dto);
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultDto ModifyPwd(ModifyPwdDto input)
        {
            _authService.ModifyPwd(input);
            return ResultDto.Success();
        }
    }
}