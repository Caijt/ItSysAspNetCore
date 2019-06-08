using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("it_contract_pay_record")]
    public class ItContractPayRecord : AuditEntity
    {
        [ForeignKey("contract_id")]
        public ItContract Contract { get; set; }
        public int contract_id { get; set; }
        public DateTime pay_date { get; set; }
        public decimal pay_price { get; set; }
        public string remarks { get; set; }
    }
}
