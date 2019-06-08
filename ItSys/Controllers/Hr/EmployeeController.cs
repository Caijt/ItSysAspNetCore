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
    public class EmployeeController : ControllerBase
    {
        private readonly HrEmployeeService _service;
        public EmployeeController(HrEmployeeService service)
        {
            _service = service;
        }
        [HttpGet]
        public ResultDto<PageListDto<HrEmployeeDto>> GetPageList([FromQuery]HrEmployeeQueryDto dto)
        {
            return _service.GetPageList(dto);
        }
        [HttpPost]
        public ResultDto<HrEmployeeDto> Save([FromForm]HrEmployeeSaveDto dto)
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