using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItContractPayRecordDto : AuditDto
    {
        public string contract_no { get; set; }
        public string contract_name { get; set; }
        public string supplier_name { get; set; }
        public int contract_id { get; set; }
        public DateTime pay_date { get; set; }
        public decimal pay_price { get; set; }
        public string remarks { get; set; }

    }
}
