using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("attach")]
    public class Attach : IdEntity
    {
        public string name { get; set; }
        public int size { get; set; }
        public string ext { get; set; }
        public string save_name { get; set; }
        public string table_name { get; set; }
        public Guid attach_guid { get; set; }
        public DateTime upload_time { get; set; }
        public int upload_user_id { get; set; }
        [ForeignKey("upload_user_id")]
        public SysUser UploadUser { get; set; }
        public DateTime? del_time { get; set; }
        public int? del_user_id { get; set; }
    }
}
