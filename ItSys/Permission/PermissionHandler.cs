using ItSys.Dto;
using ItSys.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItSys.Permission
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var filterContext = context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;
            if (!context.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new JsonResult(ResultDto.Error("检测不到登录信息！",40001));
            }
            else
            {
                var menuService = filterContext.HttpContext.RequestServices.GetRequiredService<SysMenuService>();
                if (!menuService.HasPermission(filterContext.HttpContext.Request.Path))
                {
                    filterContext.Result = new JsonResult(ResultDto.Error("没有权限访问！"));
                }
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

    }
}
