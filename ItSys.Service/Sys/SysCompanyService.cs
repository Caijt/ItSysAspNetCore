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
    public class SysCompanyService : AuditService
        <SysCompany,
        SysCompanyDto,
        SysCompanySaveDto,
        SysCompanyQueryDto>
    {
        public SysCompanyService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) : base(dbContext, mapper, authContext)
        {
            onWhere = (query, queryParams) =>
            {
                if (!string.IsNullOrWhiteSpace(queryParams.name))
                {
                    query = query.Where(e => e.Name.Contains(queryParams.name));
                }
                if (!string.IsNullOrWhiteSpace(queryParams.address))
                {
                    query = query.Where(e => e.Address.Contains(queryParams.address));
                }
                if (queryParams.inUserCompany)
                {
                    if (authContext.UserId != -1)
                    {
                        var companyStrIds = dbContext.SysUsers.Where(e => e.Id == authContext.UserId).Select(e => e.company_ids).FirstOrDefault();
                        if (string.IsNullOrWhiteSpace(companyStrIds))
                        {
                            query = query.Where(e => false);
                        }
                        else
                        {
                            var companyIds = companyStrIds.Split(",").Select(i => int.Parse(i));
                            query = query.Where(e => companyIds.Contains(e.Id));
                        }
                    }
                }
                return query;
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
