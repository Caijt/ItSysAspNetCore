using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("view_it_contract")]
    public class ItContractView : AuditCompanyEntity
    {
        public string no { get; set; }
        public string name { get; set; }
        public int supplier_id { get; set; }
        [ForeignKey("supplier_id")]
        public ItSupplier Supplier { get; set; }
        public decimal price { get; set; }
        public DateTime sign_date { get; set; }
        public DateTime begin_date { get; set; }
        public DateTime? end_date { get; set; }
        public string remarks { get; set; }
        public bool is_remind { get; set; }
        public string attach_guid { get; set; }
        public decimal pay_price_total { get; set; }
        public decimal unpay { get; set; }
        public DateTime? last_pay_date { get; set; }

        public decimal pay_progress { get; set; }

    }
}
