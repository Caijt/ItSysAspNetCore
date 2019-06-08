using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetUseRecordDto : AuditDto
    {
        public int input_status { get; set; }
        public string no { get; set; }
        public string record_type { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
        public DateTime record_date { get; set; }
        public string remarks { get; set; }
        public int dep_id { get; set; }
        public string dep_name { get; set; }
        public int employee_id { get; set; }
        public string employee_name { get; set; }
        public int amount { get; set; }
        public string place { get; set; }
        public DateTime? submit_time { get; set; }
        public string attach_guid { get; set; }
       
    }
}
