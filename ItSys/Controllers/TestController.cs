using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ItSys.ApiGroup;
using ItSys.EntityFramework;
using Microsoft.Extensions.Logging;

namespace ItSys.Controllers
{
    /// <summary>
    /// 测试类
    /// </summary>
    [Route("api/test/[controller]/[action]")]
    [ApiController]
    [ApiGroup(ApiGroupNames.Test)]
    public class TestController : ControllerBase
    {
        private const string Separator = ",";
        private readonly ItSysDbContext _dbContext;
        private readonly ILogger<TestController> _logger;
        public TestController(ItSysDbContext dbContext,ILogger<TestController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Test()
        {
            _logger.LogInformation("hehe");
            return Ok();

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetUser()
        {

            return Ok();
        }
    }
}