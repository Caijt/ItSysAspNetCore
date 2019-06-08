using AutoMapper;
using ItSys.Common;
using ItSys.Dto;
using ItSys.Entity;
using ItSys.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItSys.Service
{
    public class SysMenuService : AuditService<SysMenu, SysMenuDto, SysMenuSaveDto, SysMenuQueryDto>
    {
        public SysMenuService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) :
            base(dbContext, mapper, authContext)
        {
            onWhere = (query, queryParams) =>
            {
                if (queryParams.user_id != null)
                {
                    var roleStrIds = dbContext.SysUsers.Where(e => e.Id == queryParams.user_id).Select(e => e.role_ids).First();
                    if (string.IsNullOrWhiteSpace(roleStrIds))
                    {
                        query = query.Where(e => false);
                    }
                    else
                    {
                        var roleIds = roleStrIds.Split(",").Select(e => int.Parse(e));
                        if (!roleIds.Contains(-1))
                        {
                            var menuStrIds = dbContext.SysRoles.Where(e => roleIds.Contains(e.Id)).Select(e => e.MenuIds);
                            var menuIds = string.Join(",", menuStrIds).Split(",").Select(e => int.Parse(e));
                            var menuStrAllIds = dbSet.Where(e => menuIds.Contains(e.Id)).Distinct().Select(e => (e.ParentIds == null ? "" : e.ParentIds + ",") + e.Id.ToString());
                            var menuAllIds = string.Join(",", menuStrAllIds).Split(",").Distinct().Select(e => int.Parse(e));
                            query = query.Where(e => menuAllIds.Contains(e.Id));
                        }
                    }
                }
                if (queryParams.role_id != null)
                {
                    if (queryParams.role_id != -1)
                    {
                        var menuStrIds = dbContext.SysRoles.Where(e => e.Id == queryParams.role_id).Select(e => e.MenuIds).FirstOrDefault();
                        if (string.IsNullOrWhiteSpace(menuStrIds))
                        {
                            query = query.Where(e => false);
                        }
                        else
                        {
                            var menuIds = menuStrIds.Split(",").Select(e => int.Parse(e));
                            var menuStrAllIds = dbSet.Where(e => menuIds.Contains(e.Id)).Distinct().Select(e => (e.ParentIds == null ? "" : e.ParentIds + ",") + e.Id.ToString());
                            var menuAllIds = string.Join(",", menuStrAllIds).Split(",").Distinct().Select(e => int.Parse(e));
                            query = query.Where(e => menuAllIds.Contains(e.Id));
                        }
                    }
                }
                return query;
            };
            orderDefaultProp = e => e.Order;
            orderDescDefaultValue = false;
        }

        public List<SysMenuMiniDto> GetMiniList(SysMenuQueryDto queryParams)
        {
            return getList(queryParams, e => new SysMenuMiniDto
            {
                id = e.Id,
                parent_id = e.ParentId,
                path = e.Path,
                title = e.Title
            });
        }

        public bool HasPermission(string apiPath)
        {
            if (authContext.UserId == -1)
            {
                return true;
            }
            if (!dbSet.Any(e => ("\n" + e.Api + "\n").Contains(apiPath)))
            {
                return true;
            }
            var queryDto = new SysMenuQueryDto();
            queryDto.user_id = authContext.UserId;
            var ids = getList(queryDto, e => e.Id);
            apiPath = "\n" + apiPath + "\n";
            return dbSet.Any(e => ids.Contains(e.Id) && ("\n" + e.Api + "\n").Contains(apiPath));

        }
    }
}
