using AutoMapper;
using ItSys.Common;
using ItSys.Dto;
using ItSys.Entity;
using ItSys.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ItSys.Service
{
    public class ItContractPayRecordService
        : AuditService<ItContractPayRecord, ItContractPayRecordDto, ItContractPayRecordSaveDto, ItContractPayRecordQueryDto>
    {
        public ItContractPayRecordService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
            : base(dbContext, mapper, authContext)
        {
            this.authContext = authContext;
            enableCompanyFilter = queryParams => !queryParams.contract_id.HasValue;
            companyFilterProp = e => e.Contract.company_id;
            onWhere = (query, queryParams) =>
            {
                #region 合同编号
                if (!string.IsNullOrWhiteSpace(queryParams.contract_no))
                {
                    query = query.Where(e => e.Contract.no.Contains(queryParams.contract_no));
                }
                #endregion
                #region 合同名称
                if (!string.IsNullOrWhiteSpace(queryParams.contract_name))
                {
                    query = query.Where(e => e.Contract.name.Contains(queryParams.contract_name));
                }
                #endregion
                #region 供应商名称
                if (!string.IsNullOrWhiteSpace(queryParams.supplier_name))
                {
                    query = query.Where(e => e.Contract.Supplier.name.Contains(queryParams.supplier_name));
                }
                #endregion
                #region 备注
                if (!string.IsNullOrWhiteSpace(queryParams.remarks))
                {
                    query = query.Where(e => e.remarks.Contains(queryParams.remarks));
                }
                #endregion
                #region 付款金额
                if (queryParams.pay_price_begin.HasValue)
                {
                    query = query.Where(e => e.pay_price >= queryParams.pay_price_begin);
                }
                if (queryParams.pay_price_end.HasValue)
                {
                    query = query.Where(e => e.pay_price <= queryParams.pay_price_end);
                }
                #endregion
                #region 付款日期
                if (queryParams.pay_date_begin.HasValue)
                {
                    query = query.Where(e => e.pay_date >= queryParams.pay_date_begin);
                }
                if (queryParams.pay_date_end.HasValue)
                {
                    query = query.Where(e => e.pay_date <= queryParams.pay_date_end);
                }
                #endregion
                #region 所属公司
                if (queryParams.company_ids != null && queryParams.company_ids.Length > 0)
                {
                    query = query.Where(e => queryParams.company_ids.Contains(e.Contract.company_id));
                }
                #endregion
                if (queryParams.contract_id.HasValue)
                {
                    query = query.Where(e => e.contract_id == queryParams.contract_id);
                }
                return query;
            };
            selectExpression = e => new ItContractPayRecordDto()
            {
                contract_id = e.contract_id,
                contract_name = e.Contract.name,
                contract_no = e.Contract.no,
                create_user_name = e.CreateUser.Name,
                create_time = e.CreateTime,
                id = e.Id,
                pay_date = e.pay_date,
                pay_price = e.pay_price,
                remarks = e.remarks,
                supplier_name = e.Contract.Supplier.name,
                update_time = e.UpdateTime
            };
            orderProp = prop =>
            {
                switch (prop)
                {
                    case "pay_date":
                        return e => e.pay_date;
                    case "pay_price":
                        return e => e.pay_price;
                }
                return null;
            };
        }
        public List<Dictionary<string, object>> GetTimeStatistic(TimeStatisticQueryDto timeStatisticQueryDto, ItContractPayRecordQueryDto queryDto)
        {
            Expression<Func<ItContractPayRecord, dynamic>> groupExp = null;
            Func<DateTime, int, DateTime> dateFunc = null;
            Func<DateTime, DateTime> beginDateFunc = null;
            Func<DateTime, DateTime> endDateFunc = null;
            string format = "";
            switch (timeStatisticQueryDto.unit)
            {
                case "year":
                    format = "yyyy";
                    dateFunc = (datetime, i) => datetime.Date.AddYears(i);
                    beginDateFunc = datetime => new DateTime(datetime.Year, 1, 1);
                    endDateFunc = datetime => new DateTime(datetime.Year + 1, 1, 1);
                    groupExp = e => new
                    {
                        unit = e.pay_date.Year.ToString()
                    };
                    break;
                case "day":
                    format = "yyyy-M-d";
                    dateFunc = (datetime, i) => datetime.AddDays(i);
                    beginDateFunc = datetime => datetime.Date;
                    endDateFunc = datetime => datetime.AddDays(1).Date;
                    groupExp = e => new
                    {
                        unit = e.pay_date.Year.ToString() + "-" + e.pay_date.Month + "-" + e.pay_date.Day
                    };
                    break;
                case "month":
                default:
                    format = "yyyy-M";
                    dateFunc = (datetime, i) => datetime.AddMonths(i);
                    beginDateFunc = datetime => new DateTime(datetime.Year, datetime.Month, 1);
                    endDateFunc = datetime =>
                    {
                        datetime = datetime.AddMonths(1);
                        return new DateTime(datetime.Year, datetime.Month, 1);
                    };
                    groupExp = e => new
                    {
                        unit = e.pay_date.Year.ToString() + "-" + e.pay_date.Month.ToString()
                    };

                    break;
            }
            if (!timeStatisticQueryDto.time_end.HasValue)
            {
                timeStatisticQueryDto.time_end = DateTime.Today;
            }
            if (!timeStatisticQueryDto.time_begin.HasValue)
            {
                timeStatisticQueryDto.time_begin = dateFunc(timeStatisticQueryDto.time_end.Value, -8);
            }
            DateTime beginTime = beginDateFunc(timeStatisticQueryDto.time_begin.Value);
            DateTime endTime = endDateFunc(timeStatisticQueryDto.time_end.Value);
            List<Dictionary<string, object>> unitDataList = new List<Dictionary<string, object>>();
            DateTime timeTemp = beginTime;
            while (timeTemp < endTime)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("unit", timeTemp.ToString(format));
                dict.Add("price", 0);
                dict.Add("amount", 0);
                unitDataList.Add(dict);
                timeTemp = dateFunc(timeTemp, 1);
            }
            var query = buildWhere(this.dbQuery, queryDto);
            if (onWhere != null)
            {
                query = onWhere(query, queryDto);
            }
            var data = query.Where(e => e.pay_date >= beginTime && e.pay_date < endTime)
                .GroupBy(groupExp)
                .Select(g => new
                {
                    Key = g.Key,
                    price = g.Sum(e => e.pay_price),
                    amount = g.Count()
                }).ToList();
            unitDataList.ForEach(i =>
            {
                data.ForEach(i2 =>
                {
                    if (string.Equals(i["unit"], i2.Key.unit))
                    {
                        i["price"] = i2.price;
                        i["amount"] = i2.amount;
                    }
                });
            });
            return unitDataList;
        }
    }
}
