using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysMailTemplateDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string title_template { get; set; }
        public string content_template { get; set; }
        public bool is_disabled { get; set; }
        public string tips { get; set; }
    }
}
