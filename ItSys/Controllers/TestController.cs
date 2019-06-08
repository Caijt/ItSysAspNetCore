using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ItSys.ApiGroup;
using ItSys.EntityFramework;

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
        public TestController(ItSysDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Test()
        {
            //var a = from r in _dbContext.SysRoles
            //        let b = r.MenuIds.()
            //        from c in b
            //        select c;
            //return Ok(a.ToList());
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