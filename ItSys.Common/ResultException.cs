using System;
using System.Collections.Generic;
using System.Text;
using ItSys.Dto;

namespace ItSys.Common
{
    public class ResultException:Exception
    {
        public ResultDto result { get; set; }
        public ResultException(ResultDto r)
        {
            result = r;
        }
    }
}
