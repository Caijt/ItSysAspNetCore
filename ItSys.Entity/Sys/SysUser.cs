using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("sys_user")]
    public class SysUser : AuditEntity
    {
        public string login_name { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }

        public DateTime? last_login_time { get; set; }

        public string qywx_user { get; set; }

        public string role_ids { get; set; }

        public string company_ids { get; set; }

        public string factory_ids { get; set; }

    }
}
