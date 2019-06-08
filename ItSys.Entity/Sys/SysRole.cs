using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("sys_role")]
    public class SysRole : AuditEntity
    {
        public string Name { get; set; }
        public string Remarks { get; set; }
        [Column("menu_ids")]
        public string MenuIds { get; set; }
    }
}
