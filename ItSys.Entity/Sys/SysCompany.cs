using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("sys_company")]
    public class SysCompany : AuditEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        [Column("is_disabled")]
        public bool IsDisabled { get; set; }        

    }
}
