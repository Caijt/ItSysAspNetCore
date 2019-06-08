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
    public class ItAssetRepairRecordService
        : AuditService<ItAssetRepairRecord, ItAssetRepairRecordDto, ItAssetRepairRecordSaveDto, ItAssetRepairRecordQueryDto>
    {
        public ItAssetRepairRecordService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
            : base(dbContext, mapper, authContext)
        {
            enableCompanyFilter = queryParams=>!queryParams.asset_id.HasValue;
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
                #region 维修状态
                if (queryParams.is_finish.HasValue)
                {
                    query = query.Where(e => e.is_finish == queryParams.is_finish);
                }
                #endregion
                #region 资产型号
                if (!string.IsNullOrWhiteSpace(queryParams.asset_model))
                {
                    query = query.Where(e => e.Asset.model.Contains(queryParams.asset_model));
                }
                #endregion
                #region 维修商名称
                if (!string.IsNullOrWhiteSpace(queryParams.supplier_name))
                {
                    query = query.Where(e => e.Supplier.name.Contains(queryParams.supplier_name));
                }
                #endregion
                #region 故障
                if (!string.IsNullOrWhiteSpace(queryParams.reason))
                {
                    query = query.Where(e => e.reason.Contains(queryParams.reason));
                }
                #endregion
                #region 维修内容
                if (!string.IsNullOrWhiteSpace(queryParams.content))
                {
                    query = query.Where(e => e.content.Contains(queryParams.content));
                }
                #endregion
                #region 维修日期
                if (queryParams.repair_date_begin.HasValue)
                {
                    query = query.Where(e => e.repair_date >= queryParams.repair_date_begin);
                }
                if (queryParams.repair_date_end.HasValue)
                {
                    query = query.Where(e => e.repair_date < queryParams.repair_date_end.Value.AddDays(1));
                }
                #endregion
                #region 完成日期
                if (queryParams.finish_date_begin.HasValue)
                {
                    query = query.Where(e => e.finish_date >= queryParams.finish_date_begin);
                }
                if (queryParams.finish_date_end.HasValue)
                {
                    query = query.Where(e => e.finish_date < queryParams.finish_date_end.Value.AddDays(1));
                }
                #endregion
                return query;
            };
            selectExpression = e => new ItAssetRepairRecordDto()
            {
                asset_id = e.asset_id,
                content = e.content,
                create_time = e.CreateTime,
                create_user_name = e.CreateUser.Name,
                finish_date = e.finish_date,
                id = e.Id,
                supplier_id = e.supplier_id,
                supplier_name = e.Supplier.name,
                asset_model = e.Asset.model,
                asset_no = e.Asset.no,
                is_finish = e.is_finish,
                price = e.price,
                reason = e.reason,
                repair_date = e.repair_date,
                update_time = e.UpdateTime

            };
            orderProp = prop =>
            {
                switch (prop)
                {
                    case "repair_date":
                        return e => e.repair_date;
                    case "asset_no":
                        return e => e.Asset.no;
                    case "finish_date":
                        return e => e.finish_date;
                    case "price":
                        return e => e.price;

                }
                return null;
            };
        }
    }
}
