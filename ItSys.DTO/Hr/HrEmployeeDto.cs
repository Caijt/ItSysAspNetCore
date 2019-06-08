using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class HrEmployeeDto : AuditDto
    {
        public string name { get; set; }
        public string no { get; set; }
        public bool sex { get; set; }
        public string mail { get; set; }
        public string remarks { get; set; }
        public bool is_disabled { get; set; }
        public int dep_id { get; set; }
        public string dep_name { get; set; }
    }
}
