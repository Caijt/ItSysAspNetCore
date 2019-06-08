using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("hr_employee")]
    public class HrEmployee:AuditEntity
    {
        public string name { get; set; }
        public string no { get; set; }
        public bool sex { get; set; }
        public int dep_id { get; set; }
        [ForeignKey("dep_id")]
        public HrDep Dep { get; set; }
        public string mail { get; set; }
        public string remarks { get; set; }
        public bool is_disabled { get; set; }
    }
}
