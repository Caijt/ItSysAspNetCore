using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItContractPayRecordQueryDto : QueryDto
    {
        public string contract_no { get; set; }
        public string contract_name { get; set; }
        public string supplier_name { get; set; }
        public string remarks { get; set; }
        public decimal? pay_price_begin { get; set; }
        public decimal? pay_price_end { get; set; }
        public DateTime? pay_date_begin { get; set; }
        public DateTime? pay_date_end { get; set; }
        public int? contract_id { get; set; }
        public int[] company_ids { get; set; }
    }
}
