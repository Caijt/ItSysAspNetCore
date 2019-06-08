using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ItSys.Dto
{
    public class SysUserSaveDto : IdEntityDto
    {
        public string login_name { get; set; }
        public string name { get; set; }
        public string pwd { get; set; }
        public bool change_pwd { get; set; }
        public string qywx_user { get; set; }
        public string role_ids { get; set; }
        public string company_ids { get; set; }
        public string factory_ids { get; set; }

    }
}
