using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetScrapRecordQueryDto : QueryDto
    {
        public int? asset_id { get; set; }
        public string asset_no { get; set; }
        public string asset_model { get; set; }
        public string reason { get; set; }
        public DateTime? scrap_date_begin { get; set; }
        public DateTime? scrap_date_end { get; set; }
    }
}
