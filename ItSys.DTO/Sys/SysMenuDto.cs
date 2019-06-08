using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysMenuDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string path { get; set; }
        public int? parent_id { get; set; }
        public string parent_ids { get; set; }
        public int order { get; set; }
        public string api { get; set; }
        public string create_user_name { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
    }
}
