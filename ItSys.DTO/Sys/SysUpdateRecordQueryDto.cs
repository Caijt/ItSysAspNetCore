using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class SysUpdateRecordQueryDto:QueryDto
    {
        public string keyword { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }
}
