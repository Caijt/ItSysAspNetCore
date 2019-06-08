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
    public class NetworkController : ControllerBase
    {
        private readonly ItNetworkService _service;
        public NetworkController(ItNetworkService service)
        {
            _service = service;
        }
        [HttpGet]
        public ResultDto<List<ItNetworkDto>> GetList([FromQuery]ItNetworkQueryDto dto)
        {
            return _service.GetList(dto);
        }
        [HttpPost]
        public ResultDto<ItNetworkDto> Save([FromForm]ItNetworkSaveDto dto)
        {
            return _service.SaveWithTransaction(dto);
        }
        [HttpDelete]
        public ResultDto Delete([FromForm]int id)
        {
            _service.Delete(id);
            return ResultDto.Success();
        }
        [HttpGet]
        public ResultDto<List<string>> GetPropList(string prop, string keyword)
        {
            return _service.GetPropList(prop, keyword);
        }
    }
}