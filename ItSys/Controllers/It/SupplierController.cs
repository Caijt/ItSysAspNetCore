using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItSys.ApiGroup;
using ItSys.Dto;
using ItSys.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItSys.Controllers.It
{
    [Route("api/it/[controller]/[action]")]
    [ApiController]
    [ApiGroup(ApiGroupNames.It)]
    [Produces("application/json")]
    [Authorize("Permission")]
    public class SupplierController : ControllerBase
    {
        private readonly ItSupplierService _service;
        public SupplierController(ItSupplierService service)
        {
            _service = service;
        }
        [HttpGet]
        public ResultDto<PageListDto<ItSupplierDto>> GetPageList([FromQuery]ItSupplierQueryDto dto)
        {

            return _service.GetPageList(dto);
        }
        [HttpPost]
        public ResultDto<ItSupplierDto> Save([FromForm]ItSupplierSaveDto dto)
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
        public ResultDto<ItSupplierDto> GetDetails(int id)
        {
            return _service.Get(id);
        }
    }
}