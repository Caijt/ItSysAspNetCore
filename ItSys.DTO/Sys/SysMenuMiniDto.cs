using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysMenuMiniDto
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string path { get; set; }
        public int? parent_id { get; set; }
    }
}
