using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItContractSaveDto : IdEntityDto
    {
        public string no { get; set; }
        public string name { get; set; }
        public int company_id { get; set; }

        public int supplier_id { get; set; }

        public decimal price { get; set; }
        public DateTime sign_date { get; set; }
        public DateTime begin_date { get; set; }
        public DateTime? end_date { get; set; }
        public string remarks { get; set; }
        public bool is_remind { get; set; }
        public string attach_guid { get; set; }
    }
}
