using ItSys.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using ItSys.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using System.Linq.Expressions;
using ItSys.Entity;
using ItSys.Common;

namespace ItSys.Service
{
    public class SysRoleService : AuditService<SysRole, SysRoleDto, SysRoleSaveDto, SysRoleQueryDto>
    {
        public SysRoleService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) : base(dbContext, mapper, authContext)
        {
            onWhere = (query, queryInput) =>
            {
                if (!string.IsNullOrWhiteSpace(queryInput.name))
                {
                    query = query.Where(e => e.Name.Contains(queryInput.name));
                }
                if (!string.IsNullOrWhiteSpace(queryInput.remarks))
                {
                    query = query.Where(e => e.Remarks.Contains(queryInput.remarks));
                }
                return query;
            };
            selectExpression = e => new SysRoleDto
            {
                id = e.Id,
                create_time = e.CreateTime,
                create_user_name = e.CreateUser.Name,
                menu_ids = e.MenuIds,
                name = e.Name,
                remarks = e.Remarks,
                update_time = e.UpdateTime
            };
        }

        /// <summary>
        /// 检查名称唯一性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckNameUnique(string name, int id = 0)
        {
            return checkPropUnique(e => e.Name == name, id);
        }
    }
}
