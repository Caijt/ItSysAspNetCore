using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("it_asset_type")]
    public class ItAssetType : AuditEntity
    {
        public string name { get; set; }
        public int order { get; set; }
        public int? parent_id { get; set; }
        public string parent_ids { get; set; }
        public string remarks { get; set; }

    }
}
