using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetUseRecordItemQueryDto : QueryDto
    {
        public int? record_id { get; set; }
        public int? asset_id { get; set; }

        public string record_no { get; set; }
        public string record_type { get; set; }
        public string asset_no { get; set; }
        public string asset_model { get; set; }
        public int? dep_id { get; set; }
        public bool hasSubDep { get; set;}
        public string employee_name { get; set; }
        public string place { get; set; }
        public string remarks { get; set; }
        public DateTime? record_date_begin { get; set; }
        public DateTime? record_date_end { get; set; }
        public bool isNear { get; set; }

    }
}
