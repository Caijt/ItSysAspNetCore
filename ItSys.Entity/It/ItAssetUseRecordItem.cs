using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("it_asset_use_record_item")]
    public class ItAssetUseRecordItem : IdEntity
    {
        public int record_id { get; set; }
        [ForeignKey("record_id")]
        public ItAssetUseRecord Record { get; set; }

        public int asset_id { get; set; }
        [ForeignKey("asset_id")]
        public ItAsset Asset { get; set; }
        public int employee_id { get; set; }
        [ForeignKey("employee_id")]
        public HrEmployee Employee { get; set; }
        public int dep_id { get; set; }
        [ForeignKey("dep_id")]
        public HrDep Dep { get; set; }
        public int amount { get; set; }
        public int? use_item_id { get; set; }
    }
}
