using ItSys.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItSys.Dto;
using ItSys.Entity;
using AutoMapper;
using ItSys.Common;

namespace ItSys.Service
{
    public class ItAssetUseStatusService : ViewService<ItAssetUseStatusView, ItAssetUseStatusDto, ItAssetUseStatusQueryDto>
    {
        public ItAssetUseStatusService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) : base(dbContext, mapper)
        {
            this.authContext = authContext;
            enableCompanyFilter = queryParams => !queryParams.asset_id.HasValue;
            companyFilterProp = e => e.company_id;
            onWhere = (query, queryParams) =>
            {
                if (queryParams.not_item_ids != null && queryParams.not_item_ids.Count() > 0)
                {
                    query = query.Where(e => !queryParams.not_item_ids.Contains(e.record_item_id));
                }
                if (queryParams.asset_id.HasValue)
                {
                    query = query.Where(e => e.asset_id == queryParams.asset_id.Value);
                }
                #region 资产编号
                if (!string.IsNullOrWhiteSpace(queryParams.asset_no))
                {
                    query = query.Where(e => e.asset_no.Contains(queryParams.asset_no));
                }
                #endregion
                #region 资产型号
                if (!string.IsNullOrWhiteSpace(queryParams.asset_model))
                {
                    query = query.Where(e => e.asset_model.Contains(queryParams.asset_model));
                }
                #endregion
                #region 资产标识号
                if (!string.IsNullOrWhiteSpace(queryParams.asset_diy_no))
                {
                    query = query.Where(e => e.asset_diy_no.Contains(queryParams.asset_diy_no));
                }
                #endregion
                #region 领用部门
                if (queryParams.dep_id.HasValue)
                {
                    query = query.Where(e => e.dep_id == queryParams.dep_id);
                }
                #endregion
                #region 领用员工
                if (!string.IsNullOrWhiteSpace(queryParams.employee_name))
                {
                    query = query.Where(e => e.employee_name.Contains(queryParams.employee_name));
                }
                #endregion
                #region 使用地点
                if (!string.IsNullOrWhiteSpace(queryParams.place))
                {
                    query = query.Where(e => e.use_place.Contains(queryParams.place));
                }
                #endregion
                #region 领用备注
                if (!string.IsNullOrWhiteSpace(queryParams.remarks))
                {
                    query = query.Where(e => e.use_remarks.Contains(queryParams.remarks));
                }
                #endregion
                #region 领用日期
                if (queryParams.use_date_begin.HasValue)
                {
                    query = query.Where(e => e.use_date >= queryParams.use_date_begin);
                }
                if (queryParams.use_date_end.HasValue)
                {
                    query = query.Where(e => e.use_date < queryParams.use_date_end.Value.AddDays(1));
                }
                #endregion
                return query.Where(e => e.use_amount > 0);
            };
            orderDescDefaultValue = true;
            orderDefaultProp = e => e.record_item_id;
            orderProp = prop =>
            {
                switch (prop)
                {
                    case "use_date":
                        return e => e.use_date;
                    case "dep_name":
                        return e => e.dep_name;
                    case "employee_name":
                        return e => e.employee_name;
                    case "use_amount":
                        return e => e.use_amount;
                }
                return e => e.use_date;
            };
        }
        public dynamic test()
        {


            var a = dbContext
                .ItAssetUseRecordItems
                .Where(e => e.use_item_id != null)
                .GroupBy(e => e.use_item_id)
                .Select((e, e2) => new ItAssetUseRecordItem
                {
                    use_item_id = e.Key.Value,
                    amount = e.Sum(e1 => e1.amount)
                });
            var a2 = dbContext
                .ItAssetUseRecordItems
                .Join(
                    a,
                    e => e.use_item_id,
                    b => b.use_item_id,
                    (e, b) => new
                    {
                        item_id = e.Id,
                        e.employee_id,
                        e.dep_id,
                        amount = e.amount - b.amount
                    }
                );
            var a3 = a2.Where(e => e.amount > 0).ToList();
            return a3;
        }
    }
}
