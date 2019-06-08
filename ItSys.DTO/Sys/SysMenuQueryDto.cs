using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysMenuQueryDto : QueryDto
    {
        public string role_ids { get; set; }
        public int? user_id { get; set; }

        public int? role_id { get; set; }
    }
}
