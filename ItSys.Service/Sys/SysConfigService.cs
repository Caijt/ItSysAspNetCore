using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using ItSys.Dto;
using ItSys.Entity;
using ItSys.EntityFramework;

namespace ItSys.Service
{
    public class SysConfigService : EntityService<SysConfig, SysConfigDto, SysConfigQueryDto>
    {

        public SysConfigService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            onWhere = (query, queryParams) =>
            {
                if (!string.IsNullOrWhiteSpace(queryParams.key))
                {
                    query = query.Where(e => e.Key.Contains(queryParams.key));
                }
                if (!string.IsNullOrWhiteSpace(queryParams.remarks))
                {
                    query = query.Where(e => e.Remarks.Contains(queryParams.remarks));
                }
                if (queryParams.keys != null && queryParams.keys.Length > 0)
                {
                    query = query.Where(e => queryParams.keys.Contains(e.Key));
                }

                return query;
            };
        }
        public Dictionary<string, string> GetValues(string[] keys)
        {
            var dto = new SysConfigQueryDto()
            {
                keys = keys
            };
            return getList(dto, e => new { e.Key, e.Value }).ToDictionary(e => e.Key, e => e.Value);
        }

        public string GetValue(string key)
        {
            return get(e => e.Key == key, e => e.Value);
        }
    }
}
