using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ItSys.Dto;
using ItSys.Common;
using Newtonsoft.Json;
using ItSys.ApiGroup;
using ItSys.Service;
using Microsoft.AspNetCore.Authorization;

namespace ItSys.Controllers.Sys
{
    [Route("api/sys/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    [ApiGroup(ApiGroupNames.Sys)]
    public class ConfigController : ControllerBase
    {
        private readonly SysConfigService _service;

        public ConfigController(SysConfigService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取多个配置信息
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ResultDto<Dictionary<string, string>> GetValues([FromQuery] string[] keys)
        {
            return _service.GetValues(keys);
        }

        /// <summary>
        /// 获取单个配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultDto<string> GetValue(string key)
        {
            return _service.GetValue(key);
        }
        [HttpGet]
        public ResultDto<List<SysConfigDto>> GetList([FromQuery]SysConfigQueryDto queryParams)
        {
            return _service.GetList(queryParams);
        }
        [HttpPost]
        public ResultDto<SysConfigDto> Save([FromForm]SysConfigDto dto)
        {
            return _service.Update(dto);
        }
    }
}