using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class UserInfoDto
    {
        public int userid { get; set; }
        public string username { get; set; }
        public List<SysMenuMiniDto> menuList { get; set; }
    }
}
