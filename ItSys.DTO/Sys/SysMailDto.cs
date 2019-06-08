using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysMailDto
    {
        public string id { get; set; }
        public string mail { get; set; }
        public string smtp_host { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public int? port { get; set; }
        public bool is_ssl { get; set; }
        public string sender_name { get; set; }
    }
}
