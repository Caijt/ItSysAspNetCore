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
    public class AssetReturnRecordController : ControllerBase
    {
        private readonly ItAssetReturnRecordService _service;
        public AssetReturnRecordController(ItAssetReturnRecordService service)
        {
            _service = service;
        }
        [HttpGet]
        public ResultDto<PageListDto<ItAssetReturnRecordDto>> GetPageList([FromQuery]ItAssetReturnRecordQueryDto dto)
        {
            return _service.GetPageList(dto);
        }
        [HttpPost]
        public ResultDto<ItAssetReturnRecordDto> Save([FromForm]ItAssetReturnRecordSaveDto dto)
        {
            return _service.Save(dto);
        }
        [HttpGet]
        public ResultDto<ItAssetReturnRecordDto> GetDetails(int id)
        {
            return _service.Get(id);
        }
        [HttpGet]
        public ResultDto<List<ItAssetReturnRecordPrintDto>> GetPrintList([FromQuery]ItAssetReturnRecordQueryDto dto)
        {
            return _service.GetPrintList(dto);
        }
    }
}