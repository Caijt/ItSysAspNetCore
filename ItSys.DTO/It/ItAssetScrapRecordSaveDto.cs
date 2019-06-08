using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetScrapRecordSaveDto : IdEntityDto
    {
        public int asset_id { get; set; }
        public DateTime scrap_date { get; set; }
        public int amount { get; set; }
        public string reason { get; set; }
    }
}
