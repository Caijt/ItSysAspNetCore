using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetReturnRecordSaveDto : IdEntityDto
    {
        public int action { get; set; }
        public int company_id { get; set;}
        public DateTime record_date { get; set; }
        public string place { get; set; }
        public string remarks { get; set; }
        public string attach_guid { get; set; }
        public List<ItAssetReturnRecordItemSaveDto> asset_list { get; set; }
    }
}
