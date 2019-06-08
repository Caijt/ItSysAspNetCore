using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetReturnRecordItemSaveDto : IdEntityDto
    {
        public int asset_id { get; set; }
        public int amount { get; set; }
        public int dep_id { get; set; }
        public int employee_id { get; set; }
        public int use_item_id { get; set; }
    }
}
