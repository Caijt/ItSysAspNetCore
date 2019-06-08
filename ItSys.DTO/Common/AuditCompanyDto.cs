using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class AuditCompanyDto :AuditDto
    {
        public int company_id { get; set; }
        public string company_name { get; set; }
    }
}
