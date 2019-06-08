using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetReturnRecordQueryDto : QueryDto
    {
        public int[] ids { get; set; }
        public string no { get; set; }
        public string place { get; set; }
        public string remarks { get; set; }
        public DateTime? return_date_begin { get; set; }
        public DateTime? return_date_end { get; set; }
    }
}
