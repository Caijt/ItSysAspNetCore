using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysRoleSaveDto : IdEntityDto
    {
        public string name { get; set; }
        public string remarks { get; set; }
        public string menu_ids { get; set; }
    }
}
