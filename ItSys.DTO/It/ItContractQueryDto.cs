using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItContractQueryDto:QueryDto
    {
        public bool toPay { get; set; }
        public string no { get; set; }
        public string name { get; set; }
        public string supplier_name { get; set; }
        public string remarks { get; set; }
        public decimal? price_begin { get; set; }
        public decimal? price_end { get; set; }
        public DateTime? sign_date_begin { get; set; }
        public DateTime? sign_date_end { get; set; }
        public DateTime? begin_date_begin { get; set; }
        public DateTime? begin_date_end { get; set; }
        public DateTime? end_date_begin { get; set; }
        public DateTime? end_date_end { get; set; }
        public int[] company_ids { get; set; }
        public bool isNear { get; set; }
        public bool toExpire { get; set; }
    }
}
