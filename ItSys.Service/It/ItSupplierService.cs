using AutoMapper;
using ItSys.Common;
using ItSys.Dto;
using ItSys.Entity;
using ItSys.EntityFramework;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ItSys.Service
{
    public class ItSupplierService : AuditCompanyService<ItSupplier, ItSupplierDto, ItSupplierSaveDto, ItSupplierQueryDto>
    {

        public ItSupplierService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
            : base(dbContext, mapper, authContext)
        {
            onWhere = (query, queryParams) =>
            {
                #region 名称
                if (!string.IsNullOrWhiteSpace(queryParams.name))
                {
                    query = query.Where(e => e.name.Contains(queryParams.name));
                }
                #endregion
                #region 全称
                if (!string.IsNullOrWhiteSpace(queryParams.full_name))
                {
                    query = query.Where(e => e.full_name.Contains(queryParams.full_name));
                }
                #endregion
                #region 地址
                if (!string.IsNullOrWhiteSpace(queryParams.address))
                {
                    query = query.Where(e => e.address.Contains(queryParams.address));
                }
                #endregion
                #region 联系人
                if (!string.IsNullOrWhiteSpace(queryParams.contacts))
                {
                    query = query.Where(e => e.contacts.Contains(queryParams.contacts));
                }
                #endregion
                #region 银行
                if (!string.IsNullOrWhiteSpace(queryParams.bank))
                {
                    query = query.Where(e => e.bank.Contains(queryParams.bank));
                }
                #endregion
                #region 备注
                if (!string.IsNullOrWhiteSpace(queryParams.remarks))
                {
                    query = query.Where(e => e.remarks.Contains(queryParams.remarks));
                }
                #endregion
                #region 合作类型
                if (queryParams.supplierType != null && queryParams.supplierType.Length > 0)
                {
                    var where = PredicateBuilder.New<ItSupplier>(false);
                    foreach (var item in queryParams.supplierType)
                    {
                        where.Or(e => ("," + e.supplier_type + ",").Contains(item));
                    }
                    query = query.Where(where);
                }
                if (!string.IsNullOrWhiteSpace(queryParams.supplier_type))
                {
                    query = query.Where(e => ("," + e.supplier_type + ",").Contains(queryParams.supplier_type));
                } 
                #endregion
                return query;
            };
            onInclude = query => query.Include(e => e.Company);
            onBeforeUpdate = (entity, dto, props) =>
            {
                props.Add(e => e.attach_guid);
            };
            orderProp = prop =>
            {
                switch (prop)
                {
                    case "address":
                        return e => e.address;
                    case "contacts":
                        return e => e.contacts;
                    case "bank":
                        return e => e.bank;
                    case "remarks":
                        return e => e.remarks;
                    case "supplier_type":
                        return e => e.supplier_type;
                }
                return null;
            };
        }
    }
}
