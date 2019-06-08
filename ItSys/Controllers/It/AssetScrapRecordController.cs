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
    public class AssetScrapRecordController : ControllerBase
    {
        private readonly ItAssetScrapRecordService _service;
        public AssetScrapRecordController(ItAssetScrapRecordService service)
        {
            _service = service;
        }
        [HttpGet]
        public ResultDto<PageListDto<ItAssetScrapRecordDto>> GetPageList([FromQuery]ItAssetScrapRecordQueryDto dto)
        {
            return _service.GetPageList(dto);
        }
        [HttpGet]
        public ResultDto<List<ItAssetScrapRecordDto>> GetList([FromQuery]ItAssetScrapRecordQueryDto dto)
        {
            return _service.GetList(dto);
        }
        [HttpPost]
        public ResultDto<ItAssetScrapRecordDto> Save([FromForm]ItAssetScrapRecordSaveDto dto)
        {
            return _service.Save(dto);
        }
        [HttpDelete]
        public ResultDto Delete([FromForm]int id)
        {
            _service.Delete(id);
            return ResultDto.Success();
        }
    }
}