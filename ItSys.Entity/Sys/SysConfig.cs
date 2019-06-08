using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("sys_config")]
    public class SysConfig
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
        public string Remarks { get; set; }
    }
}
