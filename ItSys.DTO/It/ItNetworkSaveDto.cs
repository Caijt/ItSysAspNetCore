using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItNetworkSaveDto : IdEntityDto
    {
        public string type { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public string ip { get; set; }
        public int order { get; set; }
        public int? parent_id { get; set; }
        public string position { get; set; }
        public string admin_address { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string remarks { get; set; }
        public int company_id { get; set; }
    }
}
