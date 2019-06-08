using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetUseStatusDto
    {
        public string record_no { get; set; }
        public string asset_no { get; set; }
        public string asset_diy_no { get; set; }
        public string asset_model { get; set; }
        public string asset_type_name { get; set; }
        public string use_remarks { get; set; }
        public string employee_name { get; set; }
        public string dep_name { get; set; }
        public DateTime use_date { get; set; }
        public string use_place { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
        public int record_item_id { get; set; }
        public int employee_id { get; set; }
        public int dep_id { get; set; }
        public int record_id { get; set; }
        public int asset_id { get; set; }
        public int use_amount { get; set; }
    }
}
