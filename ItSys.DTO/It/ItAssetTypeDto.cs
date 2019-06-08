using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetTypeDto : IdEntityDto
    {
        public string name { get; set; }
        public int order { get; set; }
        public int? parent_id { get; set; }
        public string parent_ids { get; set; }
    }
}
