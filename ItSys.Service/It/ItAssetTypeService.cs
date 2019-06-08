using AutoMapper;
using ItSys.Common;
using ItSys.Dto;
using ItSys.Entity;
using ItSys.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Service
{
    public class ItAssetTypeService : AuditService<ItAssetType, ItAssetTypeDto, ItAssetTypeQueryDto>
    {
        public ItAssetTypeService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
            : base(dbContext, mapper, authContext)
        {
            orderDescDefaultValue = false;
            orderProp = prop => e => e.order;
            onAfterCreate = (entity, dto) =>
            {
                dbContext.Database.ExecuteSqlCommand($"call proc_sync_tree_parent_ids({"it_asset_type"},{entity.Id})");
            };
            onAfterUpdate = (entity, dto) =>
            {
                dbContext.Database.ExecuteSqlCommand($"call proc_sync_tree_parent_ids({"it_asset_type"},{entity.Id})");
            };
        }
    }
}
