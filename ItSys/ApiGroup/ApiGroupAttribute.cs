using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItSys.ApiGroup
{
    /// <summary>
    /// 系统分组特性
    /// </summary>
    public class ApiGroupAttribute : Attribute, IApiDescriptionGroupNameProvider
    {
        public ApiGroupAttribute(ApiGroupNames name)
        {
            GroupName = name.ToString();
        }
        public string GroupName { get; set; }
    }
}
