using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("sys_update_record")]
    public class SysUpdateRecord : AuditEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime update_date { get; set; }
        public Guid attach_guid { get; set; }
    }
}
