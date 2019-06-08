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
    public class AssetTypeController : ControllerBase
    {
        private readonly ItAssetTypeService _service;
        public AssetTypeController(ItAssetTypeService service)
        {
            _service = service;
        }
        [HttpGet]
        public ResultDto<List<ItAssetTypeDto>> GetList([FromQuery]ItAssetTypeQueryDto dto)
        {
            return _service.GetList(dto);
        }
        [HttpPost]
        public ResultDto<ItAssetTypeDto> Save([FromForm]ItAssetTypeDto dto)
        {
            return _service.SaveWithTransaction(dto);
        }
        [HttpDelete]
        public ResultDto Delete([FromForm]int id)
        {
             _service.Delete(id);
            return ResultDto.Success();
        }
    }
}