using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItSupplierDto:AuditDto
    {
        public string name { get; set; }
        public string full_name { get; set; }
        public string address { get; set; }
        public string contacts { get; set; }
        public string bank { get; set; }
        public string supplier_type { get; set; }
        public string remarks { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
        public Guid attach_guid { get; set; }
    }
}
