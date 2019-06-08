using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetUseRecordItemSaveDto : IdEntityDto
    {
        public int asset_id { get; set; }
        public int amount { get; set; }       
    }
}
