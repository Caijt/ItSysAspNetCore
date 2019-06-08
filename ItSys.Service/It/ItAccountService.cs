using AutoMapper;
using ItSys.Common;
using ItSys.Dto;
using ItSys.Entity;
using ItSys.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItSys.Service
{
    public class ItAccountService : AuditCompanyService<ItAccount, ItAccountDto, ItAccountSaveDto, ItAccountQueryDto>
    {
        public ItAccountService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
            : base(dbContext, mapper, authContext)
        {
            onInclude = query => query.Include(e => e.Company);
            onWhere = (query, queryParams) =>
            {
                if (!string.IsNullOrWhiteSpace(queryParams.name))
                {
                    query = query.Where(e => e.name.Contains(queryParams.name));
                }
                if (!string.IsNullOrWhiteSpace(queryParams.login_address))
                {
                    query = query.Where(e => e.login_address.Contains(queryParams.login_address));
                }
                if (!string.IsNullOrWhiteSpace(queryParams.account))
                {
                    query = query.Where(e => e.account.Contains(queryParams.account));
                }
                if (!string.IsNullOrWhiteSpace(queryParams.remarks))
                {
                    query = query.Where(e => e.remarks.Contains(queryParams.remarks));
                }
                return query;
            };
            orderProp = prop =>
            {
                return null;
            };
        }
    }
}
