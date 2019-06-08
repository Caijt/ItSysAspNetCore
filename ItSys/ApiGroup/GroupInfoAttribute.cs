using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItSys.ApiGroup
{
    /// <summary>
    /// 系统模块枚举注释
    /// </summary>
    public class GroupInfoAttribute : Attribute
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
    }
}
