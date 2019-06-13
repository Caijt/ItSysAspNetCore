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
    public class ItContractService
        : AuditCompanyViewService<ItContract, ItContractView, ItContractDto, ItContractSaveDto, ItContractQueryDto>
    {
        public ItContractService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
            : base(dbContext, mapper, authContext)
        {
            onWhere = (query, queryParams) =>
            {
                #region 合同编号
                if (!string.IsNullOrWhiteSpace(queryParams.no))
                {
                    query = query.Where(e => e.no.Contains(queryParams.no));
                }
                #endregion
                #region 合同名称
                if (!string.IsNullOrWhiteSpace(queryParams.name))
                {
                    query = query.Where(e => e.name.Contains(queryParams.name));
                }
                #endregion
                #region 供应商名称
                if (!string.IsNullOrWhiteSpace(queryParams.supplier_name))
                {
                    query = query.Where(e => e.Supplier.name.Contains(queryParams.supplier_name));
                }
                #endregion
                #region 备注
                if (!string.IsNullOrWhiteSpace(queryParams.remarks))
                {
                    query = query.Where(e => e.remarks.Contains(queryParams.remarks));
                }
                #endregion
                #region 签订日期
                if (queryParams.sign_date_begin.HasValue)
                {
                    query = query.Where(e => e.sign_date >= queryParams.sign_date_begin);
                }
                if (queryParams.sign_date_end.HasValue)
                {
                    query = query.Where(e => e.sign_date < queryParams.sign_date_end.Value.AddDays(1));
                }
                #endregion
                #region 生效日期
                if (queryParams.begin_date_begin.HasValue)
                {
                    query = query.Where(e => e.begin_date >= queryParams.begin_date_begin);
                }
                if (queryParams.begin_date_end.HasValue)
                {
                    query = query.Where(e => e.begin_date < queryParams.begin_date_end.Value.AddDays(1));
                }
                #endregion
                #region 失效日期
                if (queryParams.end_date_begin.HasValue)
                {
                    query = query.Where(e => e.end_date >= queryParams.end_date_begin);
                }
                if (queryParams.end_date_end.HasValue)
                {
                    query = query.Where(e => e.end_date < queryParams.end_date_end.Value.AddDays(1));
                }
                #endregion
                #region 所属公司
                if (queryParams.company_ids != null && queryParams.company_ids.Length > 0)
                {
                    query = query.Where(e => queryParams.company_ids.Contains(e.company_id));
                }
                #endregion
                #region 待付款
                if (queryParams.toPay)
                {
                    query = query.Where(e => e.price > e.pay_price_total);
                }
                #endregion
                #region 近期合同
                if (queryParams.isNear)
                {
                    query = query.Where(e => e.CreateTime >= DateTime.Now.AddDays(-10));
                }
                #endregion
                #region 即将过期合同
                if (queryParams.toExpire)
                {
                    query = query.Where(e => e.end_date <= DateTime.Now.AddMonths(1));
                }
                #endregion
                return query;
            };
            selectExpression = e => new ItContractDto()
            {
                attach_guid = e.attach_guid,
                begin_date = e.begin_date,
                company_id = e.company_id,
                company_name = e.Company.Name,
                create_time = e.CreateTime,
                create_user_name = e.CreateUser.Name,
                end_date = e.end_date,
                id = e.Id,
                is_remind = e.is_remind,
                last_pay_date = e.last_pay_date,
                name = e.name,
                no = e.no,
                price = e.price,
                remarks = e.remarks,
                sign_date = e.sign_date,
                supplier_id = e.supplier_id,
                supplier_name = e.Supplier.name,
                update_time = e.UpdateTime,
                pay_price_total = e.pay_price_total,
                unpay = e.unpay,
                pay_progress = e.pay_progress
            };
            orderProp = prop =>
            {
                switch (prop)
                {
                    case "price":
                        return e => e.price;
                    case "pay_price_total":
                        return e => e.pay_price_total;
                    case "unpay":
                        return e => e.unpay;
                    case "last_pay_date":
                        return e => e.last_pay_date;
                    case "sign_date":
                        return e => e.sign_date;
                    case "begin_date":
                        return e => e.begin_date;
                    case "end_date":
                        return e => e.end_date;
                    case "pay_progress":
                        return e => e.pay_progress;
                }
                return null;
            };
            summaryExpression = g => new
            {
                price = g.Sum(e => e.price),
                pay_price_total = g.Sum(e => e.pay_price_total),
                unpay = g.Sum(e => e.unpay),
            };
        }
        public List<Dictionary<string, object>> GetTimeStatistic(TimeStatisticQueryDto timeStatisticQueryDto, ItContractQueryDto queryDto)
        {
            Expression<Func<ItContractView, dynamic>> groupExp = null;
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
                        unit = e.sign_date.Year.ToString()
                    };
                    break;
                case "day":
                    format = "yyyy-M-d";
                    dateFunc = (datetime, i) => datetime.AddDays(i);
                    beginDateFunc = datetime => datetime.Date;
                    endDateFunc = datetime => datetime.AddDays(1).Date;
                    groupExp = e => new
                    {
                        unit = e.sign_date.Year.ToString() + "-" + e.sign_date.Month + "-" + e.sign_date.Day
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
                        unit = e.sign_date.Year.ToString() + "-" + e.sign_date.Month.ToString()
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
            var data = query.Where(e => e.sign_date >= beginTime && e.sign_date < endTime)
                .GroupBy(groupExp)
                .Select(g => new
                {
                    Key = g.Key,
                    price = g.Sum(e => e.price),
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
