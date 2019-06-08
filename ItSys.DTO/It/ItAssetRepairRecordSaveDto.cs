using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetRepairRecordSaveDto : IdEntityDto
    {
        public int asset_id { get; set; }
        public int? supplier_id { get; set; }
        public string reason { get; set; }
        public string content { get; set; }
        public DateTime repair_date { get; set; }
        public DateTime? finish_date { get; set; }
        public bool is_finish { get; set; }
        public decimal price { get; set; }
    }
}
