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
    public class ItAssetReturnRecordService
        : AuditCompanyService<ItAssetUseRecord, ItAssetReturnRecordDto, ItAssetReturnRecordSaveDto, ItAssetReturnRecordQueryDto>
    {
        public ItAssetReturnRecordService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
             : base(dbContext, mapper, authContext)
        {
            onWhere = (query, queryParams) =>
            {
                query = query.Where(e => e.record_type == "RETURN");
                if (queryParams.ids != null && queryParams.ids.Length > 0)
                {
                    query = query.Where(e => queryParams.ids.Contains(e.Id));
                }
                #region 交还单编号
                if (!string.IsNullOrWhiteSpace(queryParams.no))
                {
                    query = query.Where(e => e.no.Contains(queryParams.no));
                } 
                #endregion
                #region 交还地点
                if (!string.IsNullOrWhiteSpace(queryParams.place))
                {
                    query = query.Where(e => e.place.Contains(queryParams.place));
                } 
                #endregion
                #region 交还备注
                if (!string.IsNullOrWhiteSpace(queryParams.remarks))
                {
                    query = query.Where(e => e.remarks.Contains(queryParams.remarks));
                } 
                #endregion
                #region 交还日期
                if (queryParams.return_date_begin.HasValue)
                {
                    query = query.Where(e => e.record_date >= queryParams.return_date_begin);
                }
                if (queryParams.return_date_end.HasValue)
                {
                    query = query.Where(e => e.record_date < queryParams.return_date_end.Value.AddDays(1));
                }
                #endregion
                return query;
            };
            onBeforeCreate = (entity, dto) =>
            {
                string noPrefix = "ITJH" + DateTime.Now.ToString("yyMM");
                var dateMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                int i = 1;
                string maxNo = dbSet.Where(e => e.no.StartsWith(noPrefix)).Max(e => e.no);
                if (!string.IsNullOrWhiteSpace(maxNo))
                {
                    string n = maxNo.Substring(8);
                    if (int.TryParse(n, out int a))
                    {
                        i = a + 1;
                    }
                }
                entity.record_type = "RETURN";
                entity.no = noPrefix + i.ToString().PadLeft(4, '0');
                if (dto.action == 1)
                {
                    entity.submit_time = DateTime.Now;
                }
            };
            onAfterCreate = (entity, dto) =>
            {
                if (dto.action == 1)
                {
                    dbContext.Database.ExecuteSqlCommand("call proc_it_asset_sync_used({0})", entity.Id);
                }
            };
            onBeforeUpdate = (entity, dto, props) =>
            {
                props.Add(e => e.input_status);
                props.Add(e => e.no);
                props.Add(e => e.record_type);
                props.Add(e => e.attach_guid);
                props.Add(e => e.employee_id);
                props.Add(e => e.dep_id);
                props.Add(e => e.submit_time);
            };
            selectExpression = e => new ItAssetReturnRecordDto()
            {
                id = e.Id,
                company_name = e.Company.Name,
                attach_guid = e.attach_guid,
                company_id = e.company_id,
                create_time = e.CreateTime,
                update_time = e.UpdateTime,
                create_user_name = e.CreateUser.Name,
                no = e.no,
                record_date = e.record_date,
                amount = e.asset_list.Sum(a => a.amount),
                input_status = e.input_status,
                place = e.place,
                remarks = e.remarks,
                submit_time = e.submit_time
            };
            orderProp = prop =>
            {
                switch (prop)
                {
                    case "amount":
                        return e => e.asset_list.Sum(a => a.amount);
                    case "submit_time":
                        return e => e.submit_time;
                }
                return null;
            };
        }
        public List<ItAssetReturnRecordPrintDto> GetPrintList(ItAssetReturnRecordQueryDto dto)
        {
            return getList(dto, e => new ItAssetReturnRecordPrintDto()
            {
                assets = e.asset_list.Select(a => new ItAssetUseRecordItemDto()
                {
                    id = a.Id,
                    asset_model = a.Asset.model,
                    asset_no = a.Asset.no,
                    amount = a.amount,
                    asset_type_name = a.Asset.Type.name,
                    employee_name = a.Employee.name,
                    dep_name = a.Dep.name
                }),
                company_name = e.Company.Name,
                create_user_name = e.CreateUser.Name,
                no = e.no,
                place = e.place,
                record_date = e.record_date,
                id = e.Id,
                submit_time = e.submit_time,
                remarks = e.remarks
            });
        }
    }
}
