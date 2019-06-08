using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetScrapRecordDto:AuditDto
    {
        public int asset_id { get; set; }
        public string asset_no { get; set; }
        public string asset_model { get; set; }
        public DateTime scrap_date { get; set; }
        public int amount { get; set; }
        public string reason { get; set; }
    }
}
