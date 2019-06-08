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
    public class AssetUseRecordController : ControllerBase
    {
        private readonly ItAssetUseRecordService _service;
        public AssetUseRecordController(ItAssetUseRecordService service)
        {
            _service = service;
        }

        [HttpGet]
        public ResultDto<PageListDto<ItAssetUseRecordDto>> GetPageList([FromQuery]ItAssetUseRecordQueryDto dto)
        {
            return _service.GetPageList(dto);
        }

        [HttpPost]
        public ResultDto<ItAssetUseRecordDto> Save([FromForm]ItAssetUseRecordSaveDto dto)
        {
            return _service.SaveWithTransaction(dto);
        }

        [HttpGet]
        public ResultDto<ItAssetUseRecordDto> GetDetails(int id)
        {
            return _service.Get(id);
        }

        [HttpGet]
        public ResultDto<List<ItAssetUseRecordPrintDto>> GetPrintList([FromQuery]ItAssetUseRecordQueryDto dto)
        {
            return _service.GetPrintList(dto);
        }
        
    }
}