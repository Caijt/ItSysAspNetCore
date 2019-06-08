using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetUseRecordQueryDto : QueryDto
    {
        public int[] ids { get; set; }
        public string no { get; set; }
        public int? dep_id { get; set; }
        public bool hasSubDep { get; set; }
        public string employee_name { get; set; }
        public string place { get; set; }
        public string remarks { get; set; }
        public DateTime? use_date_begin { get; set; }
        public DateTime? use_date_end { get; set; }
    }
}
