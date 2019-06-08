using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("sys_mail_template")]
    public class SysMailTemplate
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        public string title_template { get; set; }
        public string content_template { get; set; }
        public bool is_disabled { get; set; }
        public string tips { get; set; }

    }
}
