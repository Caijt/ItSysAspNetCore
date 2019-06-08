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
    public class HrEmployeeService : AuditService<HrEmployee, HrEmployeeDto, HrEmployeeSaveDto, HrEmployeeQueryDto>
    {
        public HrEmployeeService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) : base(dbContext, mapper, authContext)
        {
            onInclude = query => query.Include(e => e.Dep);
            onWhere = (query, queryParams) =>
            {
                if (!string.IsNullOrWhiteSpace(queryParams.name))
                {
                    query = query.Where(e => e.name.Contains(queryParams.name));
                }
                if (!string.IsNullOrWhiteSpace(queryParams.no))
                {
                    query = query.Where(e => e.no.Contains(queryParams.no));
                }
                if (!string.IsNullOrWhiteSpace(queryParams.mail))
                {
                    query = query.Where(e => e.mail.Contains(queryParams.mail));
                }
                if (queryParams.dep_id.HasValue)
                {
                    if (queryParams.hasSubDep)
                    {
                        query = query.Where(e => (e.Dep.parent_ids + "," + e.dep_id + ",").Contains(queryParams.dep_id.ToString()));
                    }
                    else
                    {
                        query = query.Where(e => e.dep_id == queryParams.dep_id);
                    }
                }

                query = query.Where(e => e.is_disabled == queryParams.is_disabled);
                return query;
            };
        }
    }
}
