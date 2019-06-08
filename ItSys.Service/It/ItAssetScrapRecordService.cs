using AutoMapper;
using ItSys.Common;
using ItSys.Dto;
using ItSys.Entity;
using ItSys.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItSys.Service
{
    public class ItAssetScrapRecordService
        : AuditService<ItAssetScrapRecord, ItAssetScrapRecordDto, ItAssetScrapRecordSaveDto, ItAssetScrapRecordQueryDto>
    {

        public ItAssetScrapRecordService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
            : base(dbContext, mapper, authContext)
        {
            enableCompanyFilter = queryParams => !queryParams.asset_id.HasValue;
            companyFilterProp = e => e.Asset.company_id;
            onWhere = (query, queryParams) =>
            {
                if (queryParams.asset_id.HasValue)
                {
                    query = query.Where(e => e.asset_id == queryParams.asset_id);
                }
                #region 资产编号
                if (!string.IsNullOrWhiteSpace(queryParams.asset_no))
                {
                    query = query.Where(e => e.Asset.no.Contains(queryParams.asset_no));
                }
                #endregion
                #region 资产型号
                if (!string.IsNullOrWhiteSpace(queryParams.asset_model))
                {
                    query = query.Where(e => e.Asset.model.Contains(queryParams.asset_model));
                }
                #endregion
                #region 报废原因
                if (!string.IsNullOrWhiteSpace(queryParams.reason))
                {
                    query = query.Where(e => e.reason.Contains(queryParams.reason));
                }
                #endregion
                #region 报废日期
                if (queryParams.scrap_date_begin.HasValue)
                {
                    query = query.Where(e => e.scrap_date >= queryParams.scrap_date_begin);
                }
                if (queryParams.scrap_date_end.HasValue)
                {
                    query = query.Where(e => e.scrap_date < queryParams.scrap_date_end.Value.AddDays(1));
                }
                #endregion
                return query;
            };
            onBeforeUpdate = (entity, dto, props) =>
            {
                props.Add(e => e.asset_id);
            };
            selectExpression = e => new ItAssetScrapRecordDto()
            {
                amount = e.amount,
                asset_model = e.Asset.model,
                asset_no = e.Asset.no,
                create_time = e.CreateTime,
                create_user_name = e.CreateUser.Name,
                id = e.Id,
                reason = e.reason,
                scrap_date = e.scrap_date,
                update_time = e.UpdateTime,
                asset_id = e.asset_id
            };
            orderProp = prop =>
            {
                switch (prop)
                {
                    case "scrap_date":
                        return e => e.scrap_date;
                    case "asset_no":
                        return e => e.Asset.no;
                    case "amount":
                        return e => e.amount;
                }
                return null;
            };
        }
    }
}
