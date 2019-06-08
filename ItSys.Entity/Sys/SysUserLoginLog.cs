using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("sys_user_login_log")]
    public class SysUserLoginLog : IdEntity
    {
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("login_time")]
        public DateTime LoginTime { get; set; }
        public string Ip { get; set; }
    }
}
