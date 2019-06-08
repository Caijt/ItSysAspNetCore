using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItSys.ApiGroup;
using ItSys.Dto;
using ItSys.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItSys.Controllers.Sys
{
    [Route("api/sys/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    [ApiGroup(ApiGroupNames.Sys)]
    public class UpdateRecordController : ControllerBase
    {
        private readonly SysUpdateRecordService _service;
        public UpdateRecordController(SysUpdateRecordService service)
        {
            _service = service;
        }

        [HttpGet]
        public ResultDto<PageListDto<SysUpdateRecordDto>> GetPageList([FromQuery]SysUpdateRecordQueryDto dto)
        {
            return _service.GetPageList(dto);
        }

        [HttpPost]
        public ResultDto<SysUpdateRecordDto> Save([FromForm]SysUpdateRecordSaveDto dto)
        {
            return _service.Save(dto);
        }

        [HttpDelete]
        public ResultDto Delete([FromForm]int id)
        {
            _service.Delete(id);
            return ResultDto.Success();
        }

        [HttpPost]
        public ResultDto UploadAttach([FromForm]IFormFile file)
        {
            //file.CopyTo()
            var files = Request.Form.Files;
            return ResultDto.Success();
        }
    }
}