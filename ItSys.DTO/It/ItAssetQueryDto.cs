using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetQueryDto : QueryDto
    {
        public string no { get; set; }
        public string model { get; set; }
        public string[] abnormal_status { get; set; }
        public string diy_no { get; set; }
        public int? type_id { get; set; }
        public bool hasSubType { get; set; }
        public string remarks { get; set; }
        public string source { get; set; }
        public string sn { get; set; }
        public string supplier_name { get; set; }
        public int[] company_ids { get; set; }
        public int? dep_id { get; set; }
        public bool hasSubDep { get; set; }
        public string employee_name { get; set; }
        public DateTime? inbound_date_begin { get; set; }
        public DateTime? inbound_date_end { get; set; }
        public decimal? price_begin { get; set; }
        public decimal? price_end { get; set; }

        public int? stock_warning_id { get; set; }
        public int[] not_ids { get; set; }
        public int[] ids { get; set; }
        public bool isNear { get; set; }
    }
}
