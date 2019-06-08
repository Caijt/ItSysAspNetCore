using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItSys.Permission
{
    public class RouteConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var c in application.Controllers)
            {
                if (!c.Filters.Any(e => e is AuthorizeFilter))
                {
                    //这是没有写特性的，就用全局的
                    c.Filters.Add(new AuthorizeFilter("Permission"));
                }
                else {
                    //string a = "";
                    //这是有写特性的
                }
            }
        }
    }
}
