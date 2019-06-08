using AutoMapper;
using ItSys.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using ItSys.Dto;
using System.Linq;
using ItSys.Entity;
using ItSys.Common;

namespace ItSys.Service
{
    public class SysUpdateRecordService : 
        AuditService<SysUpdateRecord, SysUpdateRecordDto, SysUpdateRecordSaveDto, SysUpdateRecordQueryDto>
    {
        public SysUpdateRecordService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) :
            base(dbContext, mapper, authContext)
        {
            onWhere = (query, queryParams) =>
            {
                if (!string.IsNullOrWhiteSpace(queryParams.keyword))
                {
                    query = query.Where(e => e.Title.Contains(queryParams.keyword) || e.Content.Contains(queryParams.keyword));
                }
                if (!string.IsNullOrWhiteSpace(queryParams.title))
                {
                    query = query.Where(e => e.Title.Contains(queryParams.title));
                }
                if (!string.IsNullOrWhiteSpace(queryParams.content))
                {
                    query = query.Where(e => e.Content.Contains(queryParams.content));
                }
                return query;
            };
        }
    }
}
