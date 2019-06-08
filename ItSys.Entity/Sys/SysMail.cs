using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("sys_mail")]
    public class SysMail
    {
        [Key]
        public string id { get; set; }
        public string mail { get; set; }
        public string smtp_host { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public int? port { get; set; }
        public bool is_ssl { get; set; }
        public string sender_name { get; set; }
    }
}
