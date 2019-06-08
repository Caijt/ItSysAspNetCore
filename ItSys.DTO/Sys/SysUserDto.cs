using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysUserDto
    {
        public int id { get; set; }
        public string login_name { get; set; }
        public string name { get; set; }
        public string qywx_user { get; set; }
        public string company_names { get; set; }
        public string company_ids { get; set; }

        public string role_names { get; set; }
        public string role_ids { get; set; }
        public DateTime? last_login_time { get; set; }
        public DateTime create_time { get; set; }
        public string create_user_name { get; set; }
        public DateTime update_time { get; set; }
    }
}
