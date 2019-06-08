using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysUserQueryDto :QueryDto
    {
        public string login_name { get; set; }
        public string name { get; set; }
        public string qywx_user { get; set; }
    }
}
