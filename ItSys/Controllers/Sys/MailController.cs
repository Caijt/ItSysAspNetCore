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
    public class Mail : ControllerBase
    {
        private readonly SysMailTemplateService _templateService;
        private readonly SysMailService _mailService;
        public Mail(SysMailTemplateService service,SysMailService mailService)
        {
            _templateService = service;
            _mailService = mailService;
        }
        [HttpGet]
        public ResultDto<List<SysMailTemplateDto>> GetList([FromQuery]SysMailTemplateQueryDto dto)
        {
            return _templateService.GetList(dto);
        }
        [HttpPost]
        public ResultDto<SysMailTemplateDto> Save([FromForm]SysMailTemplateDto dto)
        {
            return _templateService.Update(dto);
        }

        [HttpPost]
        public ResultDto<SysMailDto> SaveMail([FromForm]SysMailDto dto)
        {
            return _mailService.Update(dto);
        }

        [HttpGet]
        public ResultDto<SysMailDto> GetMail([FromQuery]string id)
        {
            return _mailService.Get(id);
        }
    }
}