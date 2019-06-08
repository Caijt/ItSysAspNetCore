using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysRoleDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string remarks { get; set; }
        public string menu_ids { get; set; }
        public string create_user_name { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
    }
}
