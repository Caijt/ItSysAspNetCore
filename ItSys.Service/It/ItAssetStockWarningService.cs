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
    public class ItAssetStockWarningService
        : AuditCompanyService<ItAssetStockWarning, ItAssetStockWarningDto, ItAssetStockWarningSaveDto, ItAssetStockWarningQueryDto>
    {
        public ItAssetStockWarningService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
            : base(dbContext, mapper, authContext)
        {
            onWhere = (query, queryParams) =>
            {
                if (!string.IsNullOrWhiteSpace(queryParams.name))
                {
                    query = query.Where(e => e.name.Contains(queryParams.name));
                }
                if (queryParams.status.HasValue)
                {
                    if (queryParams.status.Value)
                    {
                        query = query.Where(e => e.Assets.Sum(a => (a.amount - a.scrap_amount - a.used)) <= e.warning_value);
                    }
                    else
                    {
                        query = query.Where(e => e.Assets.Sum(a => (a.amount - a.scrap_amount - a.used)) > e.warning_value);
                    }

                }
                if (!string.IsNullOrWhiteSpace(queryParams.remarks))
                {
                    query = query.Where(e => e.remarks.Contains(queryParams.remarks));
                }
                return query;
            };
            selectExpression = e => new ItAssetStockWarningDto()
            {
                id = e.Id,
                company_id = e.company_id,
                company_name = e.Company.Name,
                create_time = e.CreateTime,
                create_user_name = e.CreateUser.Name,
                name = e.name,
                remarks = e.remarks,
                warning_value = e.warning_value,
                update_time = e.UpdateTime,
                stock_amount = e.Assets.Sum(e1 => e1.amount - e1.scrap_amount - e1.used),
            };
            orderProp = prop =>
            {
                switch (prop)
                {
                    case "stock_amount":
                        return e => e.Assets.Sum(a => a.amount - a.scrap_amount - a.used);
                    case "warning_value":
                        return e => e.warning_value;
                    case "status":
                        return e => e.Assets.Sum(a => a.amount - a.scrap_amount - a.used) <= e.warning_value;
                }
                return null;
            };
        }
    }
}
