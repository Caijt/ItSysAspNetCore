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
    [ApiGroup(ApiGroupNames.It)]
    [Produces("application/json")]
    public class AssetStockWarningController : ControllerBase
    {
        private readonly ItAssetStockWarningService _service;
        public AssetStockWarningController(ItAssetStockWarningService service)
        {
            _service = service;
        }

        [HttpGet]
        public ResultDto<PageListDto<ItAssetStockWarningDto>> GetPageList([FromQuery]ItAssetStockWarningQueryDto dto)
        {
            return _service.GetPageList(dto);
        }
        [HttpGet]
        public ResultDto<List<ItAssetStockWarningDto>> GetList([FromQuery]ItAssetStockWarningQueryDto dto)
        {
            return _service.GetList(dto);
        }
        [HttpPost]
        public ResultDto<ItAssetStockWarningDto> Save([FromForm]ItAssetStockWarningSaveDto dto)
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