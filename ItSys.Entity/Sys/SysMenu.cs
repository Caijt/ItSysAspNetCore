using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("sys_menu")]
    public class SysMenu : AuditEntity
    {
        public string Title { get; set; }
        public string Path { get; set; }
        [Column("parent_id")]
        public int? ParentId { get; set; }
        [Column("parent_ids")]
        public string ParentIds { get; set; }
        public int Order { get; set; }
        public string Api { get; set; }
    }
}
