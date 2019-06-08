using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    public abstract class AuditEntity : IdEntity
    {
        [Column("create_time")]
        public DateTime CreateTime { get; set; }

        [Column("create_user_id")]
        public int CreateUserId { get; set; }

        public SysUser CreateUser { get; set; }
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }

        [Column("update_user_id")]
        public int UpdateUserId { get; set; }
    }
}
