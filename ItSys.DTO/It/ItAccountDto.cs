using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAccountDto:AuditDto
    {
        public string name { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
        public string login_address { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string remarks { get; set; }
        public string attach_guid { get; set; }
    }
}
