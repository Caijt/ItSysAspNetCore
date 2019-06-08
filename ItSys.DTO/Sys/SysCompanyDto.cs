using System;
using System.Collections.Generic;
using System.Text;
using ItSys.Entity;

namespace ItSys.Dto
{
    public class SysCompanyDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public bool is_disabled { get; set; }
        public string create_user_name { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
    }
}
