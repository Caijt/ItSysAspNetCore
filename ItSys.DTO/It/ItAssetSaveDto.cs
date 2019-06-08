using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetSaveDto : IdEntityDto
    {
        public int input_status { get; set; }
        public int company_id { get; set; }

        public string company_name { get; set; }
        public int? supplier_id { get; set; }
        public string supplier_name { get; set; }
        public int type_id { get; set; }
        public string type_name { get; set; }
        public string no { get; set; }
        public string model { get; set; }
        public string diy_no { get; set; }
        public decimal price { get; set; }
        public string sn { get; set; }
        public string source { get; set; }
        public int amount { get; set; }
        public int used { get; set; }
        public int scrap_amount { get; set; }
        public DateTime inbound_date { get; set; }
        public string remarks { get; set; }
        public int use_dep_id { get; set; }
        public int use_employee_id { get; set; }
        public bool is_repair { get; set; }
        public int? stock_warning_id { get; set; }
        public string attach_guid { get; set; }
    }
}
