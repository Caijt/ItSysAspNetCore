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
    public class AccountController : ControllerBase
    {
        private readonly ItAccountService _service;
        public AccountController(ItAccountService service)
        {
            _service = service;
        }
        [HttpGet]
        public ResultDto<PageListDto<ItAccountDto>> GetPageList([FromQuery]ItAccountQueryDto dto)
        {
            return _service.GetPageList(dto);
        }
        [HttpPost]
        public ResultDto<ItAccountDto> Save([FromForm]ItAccountSaveDto dto)
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