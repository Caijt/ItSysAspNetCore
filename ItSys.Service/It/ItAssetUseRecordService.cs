using ItSys.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using ItSys.Dto;
using ItSys.EntityFramework;
using AutoMapper;
using ItSys.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ItSys.Service
{
    public class ItAssetUseRecordService
        : AuditCompanyService<ItAssetUseRecord, ItAssetUseRecordDto, ItAssetUseRecordSaveDto, ItAssetUseRecordQueryDto>
    {
        public ItAssetUseRecordService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
            : base(dbContext, mapper, authContext)
        {
            onWhere = (query, queryParams) =>
            {
                query = query.Where(e => e.record_type == "USE");
                if (queryParams.ids != null && queryParams.ids.Length > 0)
                {
                    query = query.Where(e => queryParams.ids.Contains(e.Id));
                }
                #region 领用单编号
                if (!string.IsNullOrWhiteSpace(queryParams.no))
                {
                    query = query.Where(e => e.no.Contains(queryParams.no));
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
                    query = query.Where(e => e.Employee.name.Contains(queryParams.employee_name));
                } 
                #endregion
                #region 使用地点
                if (!string.IsNullOrWhiteSpace(queryParams.place))
                {
                    query = query.Where(e => e.place.Contains(queryParams.place));
                } 
                #endregion
                #region 领用备注
                if (!string.IsNullOrWhiteSpace(queryParams.remarks))
                {
                    query = query.Where(e => e.remarks.Contains(queryParams.remarks));
                } 
                #endregion
                #region 领用日期
                if (queryParams.use_date_begin.HasValue)
                {
                    query = query.Where(e => e.record_date >= queryParams.use_date_begin);
                }
                if (queryParams.use_date_end.HasValue)
                {
                    query = query.Where(e => e.record_date < queryParams.use_date_end.Value.AddDays(1));
                }
                #endregion
                return query;
            };
            onBeforeCreate = (entity, dto) =>
            {
                string noPrefix = "ITLY" + DateTime.Now.ToString("yyMM");
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
                entity.record_type = "USE";
                entity.no = noPrefix + i.ToString().PadLeft(4, '0');
                entity.asset_list.ForEach(e =>
                {
                    e.dep_id = entity.dep_id.Value;
                    e.employee_id = entity.employee_id.Value;
                });
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
            selectExpression = e => new ItAssetUseRecordDto()
            {
                id = e.Id,
                dep_name = e.Dep.name,
                employee_name = e.Employee.name,
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
                record_type = e.record_type,
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

        public List<ItAssetUseRecordPrintDto> GetPrintList(ItAssetUseRecordQueryDto dto)
        {
            return getList(dto, e => new ItAssetUseRecordPrintDto()
            {
                assets = e.asset_list.Select(a => new ItAssetUseRecordItemDto()
                {
                    id = a.Id,
                    asset_model = a.Asset.model,
                    asset_no = a.Asset.no,
                    amount = a.amount,
                    asset_type_name = a.Asset.Type.name
                }),
                company_name = e.Company.Name,
                create_user_name = e.CreateUser.Name,
                employee_name = e.Employee.name,
                dep_name = e.Dep.name,
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
