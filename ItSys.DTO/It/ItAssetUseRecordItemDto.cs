using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetUseRecordItemDto : IdEntityDto
    {
        public string record_type { get; set; }
        public string record_no { get; set; }
        public DateTime record_date { get; set; }
        public int record_id { get; set; }
        public int asset_id { get; set; }
        public string asset_no { get; set; }
        public string asset_model { get; set; }
        public string asset_type_name { get; set; }
        public string dep_name { get; set; }
        public string employee_name { get; set; }
        public int amount { get; set; }
        public string remarks { get; set; }
        public string place { get; set; }
        public DateTime inbound_date { get; set; }
        public string create_user_name { get; set; }
        public DateTime? submit_time { get; set; }

    }
}
