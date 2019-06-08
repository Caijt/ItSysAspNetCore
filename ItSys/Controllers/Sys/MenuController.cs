using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ItSys.Dto;
using ItSys.Service;
using ItSys.ApiGroup;

namespace ItSys.Controllers.Sys
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Route("api/sys/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    [ApiGroup(ApiGroupNames.Sys)]
    public class MenuController : ControllerBase
    {
        private readonly SysMenuService _service;
        public MenuController(SysMenuService service)
        {
            _service = service;
        }
        [HttpGet]
        public ResultDto<List<SysMenuDto>> GetList([FromQuery]SysMenuQueryDto queryInput)
        {
            return _service.GetList(queryInput);
        }


        /// <summary>
        /// 创建或更新角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultDto<SysMenuDto> Save([FromForm]SysMenuSaveDto input)
        {
            return _service.Save(input);
        }
        [HttpDelete]
        public ResultDto Delete([FromForm]int id)
        {
            _service.Delete(id);
            return ResultDto.Success();
        }
    }
}