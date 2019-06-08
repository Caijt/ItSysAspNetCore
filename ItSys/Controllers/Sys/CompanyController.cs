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
    /// 公司管理
    /// </summary>
    [Route("api/sys/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    [ApiGroup(ApiGroupNames.Sys)]
    public class CompanyController : ControllerBase
    {
        private readonly SysCompanyService _service;
        public CompanyController(SysCompanyService service)
        {
            _service = service;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="queryInput"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultDto<List<SysCompanyDto>> GetList([FromQuery]SysCompanyQueryDto queryInput)
        {
            return _service.GetList(queryInput);
        }

        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <param name="queryInput"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultDto<PageListDto<SysCompanyDto>> GetPageList([FromQuery]SysCompanyQueryDto queryInput)
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
        /// 创建或更新公司信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultDto<SysCompanyDto> Save([FromForm]SysCompanySaveDto input)
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