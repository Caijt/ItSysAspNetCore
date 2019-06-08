using AutoMapper;
using ItSys.Common;
using ItSys.Dto;
using ItSys.Entity;
using ItSys.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ItSys.Service
{
    public class ItNetworkService : AuditCompanyService<ItNetwork, ItNetworkDto, ItNetworkSaveDto, ItNetworkQueryDto>
    {
        public ItNetworkService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
            : base(dbContext, mapper, authContext)
        {
            onInclude = query => query.Include(e => e.Company);
            orderDescDefaultValue = false;
            orderProp = prop => e => e.order;
            onAfterCreate = (entity, dto) =>
            {
                dbContext.Database.ExecuteSqlCommand($"call proc_sync_tree_parent_ids({"it_network"},{entity.Id})");
            };
            onAfterUpdate = (entity, dto) =>
            {
                dbContext.Database.ExecuteSqlCommand($"call proc_sync_tree_parent_ids({"it_network"},{entity.Id})");
                dbContext.Database.ExecuteSqlCommand($"update it_network set company_id={entity.company_id} where FIND_IN_SET({entity.Id},parent_ids)");
            };
        }

        public List<string> GetPropList(string prop, string keyword)
        {
            Expression<Func<ItNetwork, string>> propExp = null;
            Expression<Func<ItNetwork, bool>> whereExp = null;
            if (prop == "type")
            {
                propExp = e => e.type;
                whereExp = e => string.IsNullOrWhiteSpace(keyword) || e.type.Contains(keyword);
            }
            return getPropList(propExp, whereExp);
        }
    }
}
