using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("it_asset_use_record")]
    public class ItAssetUseRecord : AuditCompanyEntity
    {
        public int input_status { get; set; }
        public string no { get; set; }
        public string record_type { get; set; }
        public DateTime record_date { get; set; }
        public string remarks { get; set; }
        public int? dep_id { get; set; }
        [ForeignKey("dep_id")]
        public HrDep Dep { get; set; }
        public int? employee_id { get; set; }
        [ForeignKey("employee_id")]
        public HrEmployee Employee { get; set; }
        public string place { get; set; }
        public DateTime? submit_time { get; set; }
        public string attach_guid { get; set; }
        public List<ItAssetUseRecordItem> asset_list { get; set; }
    }
}
