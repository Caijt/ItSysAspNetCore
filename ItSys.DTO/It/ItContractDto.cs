using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItContractDto : AuditDto
    {
        public string no { get; set; }
        public string name { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
        public int supplier_id { get; set; }

        public string supplier_name { get; set; }
        public decimal price { get; set; }
        public decimal pay_price_total { get; set; }
        public decimal unpay { get; set; }
        public decimal pay_progress{get;set;}
        public DateTime? last_pay_date { get; set; }
        public DateTime sign_date { get; set; }
        public DateTime begin_date { get; set; }
        public DateTime? end_date { get; set; }
        public string remarks { get; set; }
        public bool is_remind { get; set; }

        
        public string attach_guid { get; set; }
    }
}
