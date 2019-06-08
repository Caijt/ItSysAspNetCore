using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ModifyPwdDto
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string newPassword2 { get; set; }
    }
}
