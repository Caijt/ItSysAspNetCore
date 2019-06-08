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
    public class ItAssetUseRecordItemService
        : IdEntityService<ItAssetUseRecordItem, ItAssetUseRecordItemDto, ItAssetUseRecordItemQueryDto>
    {
        public ItAssetUseRecordItemService(ItSysDbContext dbContext, IMapper mapper,AuthContext authContext)
            : base(dbContext, mapper)
        {
            this.authContext = authContext;
            enableCompanyFilter = queryParams => !queryParams.asset_id.HasValue && !queryParams.record_id.HasValue;
            companyFilterProp = e => e.Record.company_id;
            onWhere = (query, queryParams) =>
            {
                #region 记录ID
                if (queryParams.record_id.HasValue)
                {
                    query = query.Where(e => e.record_id == queryParams.record_id.Value);
                }
                #endregion
                #region 资产ID
                if (queryParams.asset_id.HasValue)
                {
                    query = query.Where(e => e.asset_id == queryParams.asset_id.Value);
                }
                #endregion
                #region 记录编号
                if (!string.IsNullOrWhiteSpace(queryParams.record_no))
                {
                    query = query.Where(e => e.Record.no.Contains(queryParams.record_no));
                }
                #endregion
                #region 记录类型
                if (!string.IsNullOrWhiteSpace(queryParams.record_type))
                {
                    query = query.Where(e => e.Record.record_type == queryParams.record_type);
                }
                #endregion
                #region 资产编号
                if (!string.IsNullOrWhiteSpace(queryParams.asset_no))
                {
                    query = query.Where(e => e.Asset.no.Contains(queryParams.asset_no));
                }
                #endregion
                #region 部门
                if (queryParams.dep_id.HasValue)
                {
                    query = query.Where(e => e.dep_id == queryParams.dep_id);
                }
                #endregion
                #region 员工
                if (!string.IsNullOrWhiteSpace(queryParams.employee_name))
                {
                    query = query.Where(e => e.Employee.name.Contains(queryParams.employee_name));
                }
                #endregion
                #region 地点
                if (!string.IsNullOrWhiteSpace(queryParams.place))
                {
                    query = query.Where(e => e.Record.place.Contains(queryParams.place));
                }
                #endregion
                #region 备注
                if (!string.IsNullOrWhiteSpace(queryParams.remarks))
                {
                    query = query.Where(e => e.Record.remarks.Contains(queryParams.remarks));
                }
                #endregion
                #region 记录日期
                if (queryParams.record_date_begin.HasValue)
                {
                    query = query.Where(e => e.Record.record_date >= queryParams.record_date_begin);
                }
                if (queryParams.record_date_end.HasValue)
                {
                    query = query.Where(e => e.Record.record_date < queryParams.record_date_end.Value.AddDays(1));
                }
                #endregion
                #region 近期记录
                if (queryParams.isNear)
                {
                    query = query.Where(e => e.Record.submit_time >= DateTime.Now.AddDays(-10));
                }
                #endregion
                return query;
            };
            selectExpression = e => new ItAssetUseRecordItemDto()
            {
                amount = e.amount,
                asset_model = e.Asset.model,
                asset_no = e.Asset.no,
                create_user_name = e.Record.CreateUser.Name,
                dep_name = e.Dep.name,
                employee_name = e.Employee.name,
                id = e.Id,
                place = e.Record.place,
                record_date = e.Record.record_date,
                record_no = e.Record.no,
                record_id = e.record_id,
                remarks = e.Record.remarks,
                asset_type_name = e.Asset.Type.name,
                inbound_date = e.Asset.inbound_date,
                record_type = e.Record.record_type,
                submit_time = e.Record.submit_time,
                asset_id = e.asset_id
            };
            orderProp = prop =>
            {
                switch (prop)
                {
                    case "record_date":
                        return e => e.Record.record_date;
                    case "amount":
                        return e => e.amount;
                    case "submit_time":
                        return e => e.Record.submit_time;
                }
                return null;
            };
        }
    }
}
