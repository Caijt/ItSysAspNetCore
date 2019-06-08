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
    public class RoleController : ControllerBase
    {
        private readonly SysRoleService _service;
        public RoleController(SysRoleService service)
        {
            _service = service;
        }
        [HttpGet]
        public ResultDto<List<SysRoleDto>> GetList([FromQuery]SysRoleQueryDto queryInput)
        {
            return _service.GetList(queryInput);
        }
        [HttpGet]
        public ResultDto<PageListDto<SysRoleDto>> GetPageList([FromQuery]SysRoleQueryDto queryInput)
        {
            return _service.GetPageList(queryInput);
        }
        /// <summary>
        /// 检测名称唯一性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>大于0就是名称有重复</returns>
        [HttpGet]
        public ResultDto<bool> CheckNameUnique(string name, int id = 0)
        {
            return _service.CheckNameUnique(name, id);
        }

        /// <summary>
        /// 创建或更新角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultDto<SysRoleDto> Save([FromForm]SysRoleSaveDto input)
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