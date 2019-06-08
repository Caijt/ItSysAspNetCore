using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetStockWarningDto : AuditDto
    {
        public string name { get; set; }
        public string company_name { get; set; }
        public int company_id { get; set; }
        public int warning_value { get; set; }
        public string remarks { get; set; }
        public int stock_amount { get; set; }
    }
}
