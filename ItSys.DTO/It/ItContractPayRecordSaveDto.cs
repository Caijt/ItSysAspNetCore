using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItContractPayRecordSaveDto : IdEntityDto
    {
        public DateTime pay_date { get; set; }
        public decimal pay_price { get; set; }
        public string remarks { get; set; }
        public int contract_id { get; set; }
    }
}
