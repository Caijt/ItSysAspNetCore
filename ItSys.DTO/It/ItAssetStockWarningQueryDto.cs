using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetStockWarningQueryDto : QueryDto
    {
        public string name { get; set; }
        public bool? status { get; set; }
        public string remarks { get; set; }
    }
}
