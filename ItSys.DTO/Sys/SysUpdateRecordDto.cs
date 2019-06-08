using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysUpdateRecordDto : AuditDto
    {
        public string title { get; set; }
        public string content { get; set; }
        public DateTime update_date { get; set; }
        public Guid attach_guid { get; set; }
        
    }
}
