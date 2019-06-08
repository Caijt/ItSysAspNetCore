using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class AttachSaveDto : IdEntityDto
    {
        public string name { get; set; }
        public int size { get; set; }
        public string ext { get; set; }
        public string save_name { get; set; }
        public string table_name { get; set; }
        public Guid attach_guid { get; set; }
    }
}
