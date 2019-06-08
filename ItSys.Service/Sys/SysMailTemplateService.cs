using AutoMapper;
using ItSys.Dto;
using ItSys.Entity;
using ItSys.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItSys.Service
{
    public class SysMailTemplateService : EntityService<SysMailTemplate, SysMailTemplateDto, SysMailTemplateQueryDto>
    {
        public SysMailTemplateService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            onBeforeUpdate = (entity, dto, props) =>
            {
               props.Add(e => e.tips);
               props.Add(e => e.name);
            };
            onWhere = (query, queryParams) =>
            {
                if (!string.IsNullOrWhiteSpace(queryParams.name))
                {
                    query = query.Where(e => e.name.Contains(queryParams.name));
                }
                return query;
            };
        }
    }
}
