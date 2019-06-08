using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItSys.ApiGroup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ItSys.Service;
using ItSys.Dto;
using System.Text.RegularExpressions;
using ItSys.Entity;
using AutoMapper;

namespace ItSys.Controllers.Sys
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/sys/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    [ApiGroup(ApiGroupNames.Sys)]
    public class UserController : ControllerBase
    {
        private SysUserService _service;

        public UserController(SysUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public ResultDto<SysUserDto> Save([FromForm]SysUserSaveDto saveDto)
        {
            return _service.Save(saveDto);
        }

        [HttpGet]
        public ResultDto<PageListDto<SysUserDto>> GetPageList([FromQuery]SysUserQueryDto queryParams)
        {
            return _service.GetPageList(queryParams);
        }

        [HttpDelete]
        public ResultDto Delete([FromForm]int id)
        {
            _service.Delete(id);
            return ResultDto.Success();
        }

        [HttpGet]
        public ResultDto<bool> CheckLoginNameUnique(string name, int id = 0)
        {
            return _service.CheckLoginNameUnique(name, id);
        }
    }
}