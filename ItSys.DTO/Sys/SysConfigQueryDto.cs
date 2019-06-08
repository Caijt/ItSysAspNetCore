using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysConfigQueryDto : QueryDto
    {
        public string key { get; set; }
        public string remarks { get; set; }
        public string[] keys { get; set; }
    }
}
