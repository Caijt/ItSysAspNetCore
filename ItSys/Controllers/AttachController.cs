using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ItSys.Dto;
using ItSys.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace ItSys.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class AttachController : ControllerBase
    {
        private readonly AttachService _service;
        private readonly IHostingEnvironment _env;
        private readonly string _uploadAttachPath;
        public AttachController(AttachService attachService, IHostingEnvironment env)
        {
            _service = attachService;
            _env = env;
            _uploadAttachPath = Path.Combine(_env.WebRootPath, "Uploads", "Attach");
        }
        [HttpGet]
        public ResultDto<List<AttachDto>> GetList([FromQuery]AttachQueryDto dto)
        {
            return _service.GetList(dto);
        }
        [HttpPost]
        public ResultDto<AttachDto> Upload([FromForm]IFormFile file, [FromForm]Guid attachGuid)
        {
            if (!Directory.Exists(_uploadAttachPath))
            {
                Directory.CreateDirectory(_uploadAttachPath);
            }
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_uploadAttachPath, fileName);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var dto = new AttachSaveDto()
            {
                save_name = fileName,
                size = Convert.ToInt32(file.Length),
                ext = Path.GetExtension(file.FileName),
                name = file.FileName,
                attach_guid = attachGuid
            };
            return _service.Create(dto);
        }
        [HttpPost]
        public IActionResult Download([FromForm]int id)
        {
            var entity = _service.GetEntity(id);
            string filePath = Path.Combine(_uploadAttachPath, entity.save_name);
            var provider = new FileExtensionContentTypeProvider();
            var memi =provider.Mappings[entity.ext];
            var stream = System.IO.File.OpenRead(filePath);
            return File(stream, memi, entity.name);

        }
        [HttpDelete]
        public ResultDto Delete([FromForm]int id)
        {
            _service.DeleteSoft(id);
            return ResultDto.Success();
        }

        [HttpDelete]
        public ResultDto DeleteTrue([FromForm]int id)
        {
            var dto = _service.GetEntity(id);
            string filePath = Path.Combine(_uploadAttachPath, dto.save_name);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            _service.Delete(id);
            return ResultDto.Success();
        }

    }
}