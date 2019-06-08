using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItSys.ApiGroup;
using ItSys.Dto;
using ItSys.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItSys.Controllers.It
{
    [Route("api/it/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    [ApiGroup(ApiGroupNames.It)]
    public class AssetUseStatusController : ControllerBase
    {
        private readonly ItAssetUseStatusService _service;
        public AssetUseStatusController(ItAssetUseStatusService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult test()
        {
            return Ok(_service.test());
        }
        [HttpGet]
        public ResultDto<PageListDto<ItAssetUseStatusDto>> GetPageList([FromQuery]ItAssetUseStatusQueryDto dto)
        {
            return _service.GetPageList(dto);
        }
        [HttpGet]
        public ResultDto<List<ItAssetUseStatusDto>> GetList([FromQuery]ItAssetUseStatusQueryDto dto)
        {
            return _service.GetList(dto);
        }
    }
}