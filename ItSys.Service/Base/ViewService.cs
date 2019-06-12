using AutoMapper;
using ItSys.Common;
using ItSys.Dto;
using ItSys.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ItSys.Service
{
    /// <summary>
    /// 视图查询服务层
    /// </summary>
    /// <typeparam name="TViewEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TQueryDto"></typeparam>
    public class ViewService<TViewEntity, TDto, TQueryDto>
        where TViewEntity : class, new()
        where TQueryDto : IQueryDto
    {
        protected ItSysDbContext dbContext;
        protected IQueryable<TViewEntity> dbQuery;
        protected IMapper mapper;
        protected AuthContext authContext;
        /// <summary>
        /// 实体转化为Dto对象的表达式
        /// </summary>
        protected Expression<Func<TViewEntity, TDto>> selectExpression { get; set; }

        /// <summary>
        /// 是否开启所属公司过滤功能
        /// </summary>
        protected Func<TQueryDto, bool> enableCompanyFilter { get; set; }
        /// <summary>
        /// 指定所属公司id属性
        /// </summary>
        protected Expression<Func<TViewEntity, int>> companyFilterProp { get; set; }
        protected Expression<Func<IGrouping<int, TViewEntity>, object>> summaryExpression { get; set; }

        /// <summary>
        /// 构建Where查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected Func<IQueryable<TViewEntity>, TQueryDto, IQueryable<TViewEntity>> onWhere { get; set; }

        /// <summary>
        /// 构建Include关联属性数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected Func<IQueryable<TViewEntity>, IQueryable<TViewEntity>> onInclude { get; set; }

        /// <summary>
        /// 构建排序
        /// </summary>
        /// <returns></returns>
        protected Expression<Func<TViewEntity, dynamic>> orderDefaultProp { get; set; }
        protected Func<string, Expression<Func<TViewEntity, dynamic>>> orderProp { get; set; }

        protected bool orderDescDefaultValue { get; set; }

        public ViewService(ItSysDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.dbQuery = dbContext.Set<TViewEntity>().AsQueryable();
            selectExpression = e => mapper.Map<TDto>(e);
            enableCompanyFilter = e => false;
        }

        protected bool checkPropUnique(Expression<Func<TViewEntity, bool>> exp)
        {
            var query = dbQuery.Where(exp);
            return query.Count() <= 0;
        }

        protected IQueryable<T> buildPage<T>(IQueryable<T> query, TQueryDto queryParams)
        {
            if (queryParams.noPage == 1 || !queryParams.IsPaged())
            {
                return query;
            }
            return query.Skip((queryParams.currentPage - 1) * queryParams.pageSize).Take(queryParams.pageSize);
        }

        

        protected IQueryable<TViewEntity> buildQuery(TQueryDto queryParams)
        {
            var query = dbQuery.AsNoTracking();

            #region 加载导航属性
            query = buildInclude(query);
            if (onInclude != null)
            {
                query = onInclude(query);
            }
            #endregion

            #region 查询条件
            query = buildWhere(query, queryParams);
            if (onWhere != null)
            {
                query = onWhere(query, queryParams);
            }
            #endregion

            #region 排序
            bool orderDesc = queryParams.orderDesc.GetValueOrDefault(orderDescDefaultValue);
            if (orderDefaultProp != null)
            {
                query = orderDesc ? query.OrderByDescending(orderDefaultProp) : query.OrderBy(orderDefaultProp);
            }
            var exp = buildOrderProp(queryParams.orderProp) ??
                (orderProp != null ? orderProp(queryParams.orderProp) : null);
            if (exp != null)
            {
                query = orderDesc ? query.OrderByDescending(exp) : query.OrderBy(exp);
            }
            #endregion

            return query;
        }
        protected IQueryable<T> buildQuery<T>(TQueryDto queryParams, Expression<Func<TViewEntity, T>> selectExp)
        {
            return buildQuery(queryParams).Select(selectExp);
        }

        protected List<T> getPropList<T>(Expression<Func<TViewEntity, T>> propExp, Expression<Func<TViewEntity, bool>> whereExp)
        {
            return dbQuery.Where(whereExp).Select(propExp).Distinct().ToList();
        }

        protected T get<T>(Expression<Func<TViewEntity, bool>> whereExp, Expression<Func<TViewEntity, T>> selectExp)
        {
            var query = dbQuery.AsNoTracking().Where(whereExp);
            query = buildInclude(query);
            if (onInclude != null)
            {
                query = onInclude(query);
            }
            return query.Select(selectExp).FirstOrDefault();
        }

        /// <summary>
        /// 根据传入的表达式返回对应的实体类型列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryParams"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        protected List<T> getList<T>(TQueryDto queryParams, Expression<Func<TViewEntity, T>> exp)
        {
            var query = buildQuery(queryParams, exp);
            return query.ToList();
        }

        /// <summary>
        /// 根据传入的表达式返回对应的实体类型分页列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryParams"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        protected PageListDto<T> getPageList<T>(TQueryDto queryParams, Expression<Func<TViewEntity, T>> exp)
        {
            var query = buildQuery(queryParams, exp);
            var queryPaged = buildPage(query, queryParams);
            return new PageListDto<T>()
            {
                List = queryPaged.ToList(),
                Total = query.Count()
            };
        }

        protected PageListSummaryDto<T> getPageListWithSummary<T>(TQueryDto queryParams
            , Expression<Func<TViewEntity, T>> selectExp
            , Expression<Func<IGrouping<int, TViewEntity>, dynamic>> summaryExp)
        {
            var entityQuery = buildQuery(queryParams);
            var dtoQuery = entityQuery.Select(selectExp);
            var queryPaged = buildPage(dtoQuery, queryParams);
            return new PageListSummaryDto<T>()
            {
                List = queryPaged.ToList(),
                Total = dtoQuery.Count(),
                Summary = entityQuery.GroupBy(e => 1).Select(summaryExp).SingleOrDefault()
            };
        }
        protected virtual IQueryable<TViewEntity> buildInclude(IQueryable<TViewEntity> query)
        {
            return query;
        }

        protected virtual IQueryable<TViewEntity> buildWhere(IQueryable<TViewEntity> query, TQueryDto queryParams)
        {
            if (enableCompanyFilter(queryParams))
            {
                if (authContext == null)
                {
                    throw new Exception("要启动公司过滤功能，请authContext进行赋值！");
                }
                if (companyFilterProp == null)
                {
                    throw new Exception("要启动公司过滤功能，请指定实体的公司Id属性！");
                }
                if (authContext.UserId != -1)
                {
                    var companyStrIds = dbContext.SysUsers
                        .Where(e => e.Id == authContext.UserId)
                        .Select(e => e.company_ids)
                        .FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(companyStrIds))
                    {
                        query = query.Where(e => false);
                    }
                    else
                    {
                        var companyIds = companyStrIds.Split(",").Select(i => int.Parse(i)).ToList();
                        var constant = Expression.Constant(companyIds, companyIds.GetType());
                        var method = companyIds.GetType().GetMethod("Contains");
                        var body = Expression.Call(constant, method, companyFilterProp.Body);
                        var whereExp = Expression.Lambda<Func<TViewEntity, bool>>(body, companyFilterProp.Parameters);
                        query = query.Where(whereExp);
                    }
                }
            }
            return query;
        }

        protected virtual Expression<Func<TViewEntity, dynamic>> buildDefaultOrder()
        {
            return null;
        }

        protected virtual Expression<Func<TViewEntity, dynamic>> buildOrderProp(string prop)
        {
            return null;
        }

        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <param name="queryInput"></param>
        /// <returns></returns>
        public virtual TDto Get(Expression<Func<TViewEntity, bool>> whereExp)
        {
            return get(whereExp, selectExpression);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public virtual List<TDto> GetList(TQueryDto queryParams)
        {
            return getList(queryParams, selectExpression);
        }

        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public virtual PageListDto<TDto> GetPageList(TQueryDto queryParams)
        {
            return getPageList(queryParams, selectExpression);
        }

        public virtual PageListSummaryDto<TDto> GetPageListWithSummary(TQueryDto queryParams)
        {
            return getPageListWithSummary(queryParams, selectExpression, summaryExpression);
        }

    }
}
