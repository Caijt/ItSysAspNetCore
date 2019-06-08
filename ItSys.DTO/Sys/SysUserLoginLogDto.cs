using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysUserLoginLogDto:IdEntityDto
    {
        public DateTime login_time { get; set; }
        public string ip { get; set; }
    }
}
