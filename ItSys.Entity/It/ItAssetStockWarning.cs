using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("it_asset_stock_warning")]
    public class ItAssetStockWarning : AuditCompanyEntity
    {
        public string name { get; set; }
        public int warning_value { get; set; }
        public string remarks { get; set; }
        public List<ItAsset> Assets { get; set; }

    }
}
