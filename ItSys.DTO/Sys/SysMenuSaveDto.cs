using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysMenuSaveDto : IdEntityDto
    {
        public string title { get; set; }
        public string path { get; set; }
        public int? parent_id { get; set; }
        public int order { get; set; }
        public string api { get; set; }
    }
}
