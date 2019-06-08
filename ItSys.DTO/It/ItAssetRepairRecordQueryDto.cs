using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetRepairRecordQueryDto : QueryDto
    {
        public int? asset_id { get; set; }
        public string asset_no { get; set; }
        public bool? is_finish { get; set; }
        public string asset_model { get; set; }
        public string supplier_name { get; set; }
        public string reason { get; set; }
        public string content { get; set; }
        public DateTime? repair_date_begin { get; set; }
        public DateTime? repair_date_end { get; set; }
        public DateTime? finish_date_begin { get; set; }
        public DateTime? finish_date_end { get; set; }

    }
}
