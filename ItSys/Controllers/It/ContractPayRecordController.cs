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
    public class ContractPayRecordController : ControllerBase
    {
        private readonly ItContractPayRecordService _service;
        public ContractPayRecordController(ItContractPayRecordService service)
        {
            _service = service;
        }
        [HttpGet]
        public ResultDto<PageListDto<ItContractPayRecordDto>> GetPageList([FromQuery]ItContractPayRecordQueryDto dto)
        {
            return _service.GetPageList(dto);
        }
        [HttpPost]
        public ResultDto<ItContractPayRecordDto> Save([FromForm]ItContractPayRecordSaveDto dto)
        {
            return _service.Save(dto);
        }
        [HttpDelete]
        public ResultDto Delete([FromForm]int id)
        {
            _service.Delete(id);
            return ResultDto.Success();
        }
        [HttpGet]
        public ResultDto<List<Dictionary<string, object>>> GetTimeStatistic(
            [FromQuery]TimeStatisticQueryDto timeStatisticQueryDto, [FromQuery]ItContractPayRecordQueryDto queryDto)
        {
            return _service.GetTimeStatistic(timeStatisticQueryDto, queryDto);
        }
    }
}