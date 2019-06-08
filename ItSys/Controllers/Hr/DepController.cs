using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItSys.ApiGroup;
using ItSys.Dto;
using ItSys.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItSys.Controllers.Hr
{
    [Route("api/hr/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    [ApiGroup(ApiGroupNames.Hr)]
    public class DepController : ControllerBase
    {
        private readonly HrDepService _service;
        public DepController(HrDepService service)
        {
            _service = service;
        }

        [HttpGet]
        public ResultDto<List<HrDepDto>> GetList([FromQuery]HrDepQueryDto dto)
        {
            return _service.GetList(dto);
        }
        [HttpPost]
        public ResultDto<HrDepDto> Save([FromForm]HrDepDto dto)
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