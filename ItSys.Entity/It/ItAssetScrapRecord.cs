using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("it_asset_scrap_record")]
    public class ItAssetScrapRecord : AuditEntity
    {
        public int asset_id { get; set; }
        [ForeignKey("asset_id")]
        public ItAsset Asset { get; set; }
        public DateTime scrap_date { get; set; }
        public int amount { get; set; }
        public string reason { get; set; }
    }
}
