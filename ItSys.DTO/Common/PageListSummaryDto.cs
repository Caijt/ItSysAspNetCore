using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class PageListSummaryDto<T> : PageListDto<T>
    {
        public dynamic Summary { get; set; }
    }
}
