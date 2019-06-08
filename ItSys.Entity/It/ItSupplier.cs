using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("it_supplier")]
    public class ItSupplier : AuditCompanyEntity
    {
        public string name { get; set; }
        public string full_name { get; set; }
        public string address { get; set; }
        public string contacts { get; set; }
        public string bank { get; set; }
        public string supplier_type { get; set; }
        public string remarks { get; set; }
        public Guid attach_guid { get; set; }
    }
}
