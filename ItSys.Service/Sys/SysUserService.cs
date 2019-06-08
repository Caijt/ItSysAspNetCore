using System;
using System.Collections.Generic;
using System.Text;
using ItSys.Entity;
using ItSys.Dto;
using AutoMapper;
using ItSys.EntityFramework;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ItSys.Common;

namespace ItSys.Service
{
    public class SysUserService : AuditService<SysUser, SysUserDto, SysUserSaveDto, SysUserQueryDto>
    {
        public SysUserService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) :
            base(dbContext, mapper, authContext)
        {
            onWhere = (query, queryParams) =>
            {
                if (!string.IsNullOrWhiteSpace(queryParams.login_name))
                {
                    query = query.Where(e => e.login_name.Contains(queryParams.login_name));
                }
                if (!string.IsNullOrWhiteSpace(queryParams.name))
                {
                    query = query.Where(e => e.Name.Contains(queryParams.name));
                }
                if (!string.IsNullOrWhiteSpace(queryParams.qywx_user))
                {
                    query = query.Where(e => e.qywx_user.Contains(queryParams.qywx_user));
                }
                return query;
            };
            onBeforeUpdate = (entity, dto, props) =>
            {
                if (!dto.change_pwd)
                {
                    props.Add(e => e.Pwd);
                }
            };
        }

        public bool CheckLoginNameUnique(string name, int id = 0)
        {
            return checkPropUnique(e => e.login_name == name, id);
        }

        public override PageListDto<SysUserDto> GetPageList(SysUserQueryDto queryParams)
        {
            var dto = base.GetPageList(queryParams);
            foreach (var user in dto.List)
            {
                if (user.company_ids == "*")
                {
                    user.company_names = "所有";
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(user.company_ids))
                    {
                        var companyIds = user.company_ids.Split(",").Select(i => int.Parse(i));
                        user.company_names = string.Join(",", dbContext.SysCompanys.Where(e => companyIds.Contains(e.Id)).Select(e => e.Name));
                    }
                }
                if (!string.IsNullOrWhiteSpace(user.role_ids))
                {
                    var roleIds = user.role_ids.Split(",").Select(i => int.Parse(i));
                    user.role_names = string.Join(",", dbContext.SysRoles.Where(e => roleIds.Contains(e.Id)).Select(e => e.Name));
                }
            }
            return dto;
        }
    }
}
