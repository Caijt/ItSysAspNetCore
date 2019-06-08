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
    public class AssetUseRecordItemController : ControllerBase
    {
        private readonly ItAssetUseRecordItemService _service;
        public AssetUseRecordItemController(ItAssetUseRecordItemService service)
        {
            _service = service;
        }
        [HttpGet]
        public ResultDto<PageListDto<ItAssetUseRecordItemDto>> GetPageList([FromQuery]ItAssetUseRecordItemQueryDto dto)
        {
            return _service.GetPageList(dto);
        }
        [HttpGet]
        public ResultDto<List<ItAssetUseRecordItemDto>> GetList([FromQuery]ItAssetUseRecordItemQueryDto dto)
        {
            return _service.GetList(dto);
        }
    }
}