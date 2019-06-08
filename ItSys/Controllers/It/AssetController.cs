using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ItSys.Dto;
using ItSys.Service;
using ItSys.ApiGroup;
using ItSys.Helper;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace ItSys.Controllers.It
{
    [Route("api/it/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    [ApiGroup(ApiGroupNames.It)]
    public class AssetController : ControllerBase
    {
        private ItAssetService _service;
        public AssetController(ItAssetService service)
        {
            _service = service;


        }
        [HttpGet]
        public ResultDto<PageListDto<ItAssetDto>> GetPageList([FromQuery]ItAssetQueryDto dto)
        {
            return _service.GetPageList(dto);
        }
        [HttpGet]
        public ResultDto<PageListSummaryDto<ItAssetDto>> GetPageListWithSummary([FromQuery]ItAssetQueryDto dto)
        {
            return _service.GetPageListWithSummary(dto);
        }
        [HttpGet]
        public ResultDto<List<ItAssetDto>> GetList([FromQuery]ItAssetQueryDto dto)
        {
            return _service.GetList(dto);
        }
        [HttpGet]
        public ResultDto<ItAssetDto> GetDetails(int id)
        {
            return _service.Get(id);
        }
        [HttpGet]
        public ResultDto<List<dynamic>> GetPropList(string prop, string keyword)
        {
            return _service.GetPropList(prop, keyword);
        }
        [HttpPost]
        public ResultDto<ItAssetDto> Save([FromForm]ItAssetSaveDto dto)
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
            [FromQuery]TimeStatisticQueryDto timeStatisticQueryDto, [FromQuery]ItAssetQueryDto queryDto)
        {
            return _service.GetTimeStatistic(timeStatisticQueryDto, queryDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetPrintQrcode(int id)
        {
            string url = Request.Scheme + "://" + Request.Host.Value + "/mobile/#/it/asset/details/" + id;
            return File(QrcodeHelper.CreateQrcode(url, 3), "image/png");
        }
        [HttpGet("{id}")]
        public IActionResult getDetailsQrcode(int id)
        {
            string url = Request.Scheme + "://" + Request.Host.Value + "/mobile/#/it/asset/detailsNoAuth/" + id;
            return File(QrcodeHelper.CreateQrcode(url), "image/png");
        }

        [HttpPost]
        public ResultDto<int> ImportExcel([FromForm]IFormFile file)
        {
            return _service.ImportExcel(file.OpenReadStream());
        }
        [HttpGet]
        public IActionResult ExportExcel([FromQuery]ItAssetQueryDto dto)
        {
            return File(
                _service.ExportExcel(dto),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "IT资产列表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
        }

        [HttpGet]
        public ResultDto<dynamic> Test([FromQuery]ItAssetQueryDto dto)
        {
            return _service.GetListSummary(dto);
        }
    }
}