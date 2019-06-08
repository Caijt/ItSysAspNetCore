using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetStockWarningSaveDto : IdEntityDto
    {
        public string name { get; set; }
        public int company_id { get; set; }
        public int warning_value { get; set; }
        public string remarks { get; set; }
    }
}
