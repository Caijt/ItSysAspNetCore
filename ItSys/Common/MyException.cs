using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItSys.Common
{
    public class MyException:Exception
    {
        public int Code { get; set; }
        public MyException(int code)
        {
            Code = code;
        }
    }
}
