using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class TimeStatisticQueryDto
    {
        public string unit { get; set; }
        public DateTime? time_begin { get; set; }
        public DateTime? time_end { get; set; }
    }
}
