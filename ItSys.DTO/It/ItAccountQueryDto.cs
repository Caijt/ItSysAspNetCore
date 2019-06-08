using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAccountQueryDto:QueryDto
    {
        public string name { get; set;}
        public string login_address { get; set; }
        public string account { get; set; }
        public string remarks { get; set; }
    }
}
