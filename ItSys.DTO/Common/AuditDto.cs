using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class AuditDto : IdEntityDto
    {
        public string create_user_name { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
    }
}
