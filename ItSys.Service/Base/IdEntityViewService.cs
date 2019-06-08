using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using ItSys.Dto;
using ItSys.Entity;
using ItSys.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ItSys.Service
{
    public abstract class IdEntityViewService<
        TEntity,
        TViewEntity,
        TDto,
        TCreateDto,
        TUpdateDto,
        TQueryDto> :
        EntityViewService<TEntity, TViewEntity, TDto, TCreateDto, TUpdateDto, TQueryDto>
        where TEntity : IdEntity, new()
        where TViewEntity : IdEntity, new()
        where TUpdateDto : IdEntityDto
        where TQueryDto : IQueryDto
    {

        public IdEntityViewService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            orderDescDefaultValue = true;
            orderDefaultProp = e => e.Id;
        }

        protected bool checkPropUnique(Expression<Func<TEntity, bool>> exp, int id = 0)
        {
            var query = dbSet.Where(exp);
            if (id != 0)
            {
                query = query.Where(e => e.Id != id);
            }
            return query.Count() <= 0;
        }

        protected T get<T>(int id, Expression<Func<TViewEntity, T>> exp)
        {
            return get(e => e.Id == id, exp);
        }

        protected override Expression<Func<TViewEntity, dynamic>> buildOrderProp(string prop)
        {
            return e => e.Id;
        }

        public virtual void Delete(int id)
        {
            var entity = new TEntity();
            entity.Id = id;
            Delete(entity);
        }

        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <param name="queryInput"></param>
        /// <returns></returns>
        public virtual TDto Get(int id)
        {
            return get(id, selectExpression);
        }

    }

    /// <summary>
    /// 简化为只需传入4个泛型，可用于创建跟更新的Dto一样的
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TSaveDto"></typeparam>
    /// <typeparam name="TQueryDto"></typeparam>
    public abstract class IdEntityViewService<TEntity, TViewEntity, TDto, TSaveDto, TQueryDto> : IdEntityViewService<TEntity, TViewEntity, TDto, TSaveDto, TSaveDto, TQueryDto>
        where TEntity : IdEntity, new()
        where TViewEntity : IdEntity, new()
        where TSaveDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public IdEntityViewService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
        public virtual TDto Save(TSaveDto saveDto)
        {
            if (saveDto.id == 0)
            {
                return Create(saveDto);
            }
            else
            {
                return Update(saveDto);
            }
        }

        public virtual TDto SaveWithTransaction(TSaveDto saveDto)
        {
            if (saveDto.id == 0)
            {
                return CreateWithTransaction(saveDto);
            }
            else
            {
                return UpdateWithTransaction(saveDto);
            }
        }
    }

    /// <summary>
    /// 简化为只需传入3个泛型，可用于显示、创建、更新的Dto都一样的
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TQueryDto"></typeparam>
    public abstract class IdEntityViewService<TEntity, TViewEntity, TDto, TQueryDto> : IdEntityViewService<TEntity, TViewEntity, TDto, TDto, TQueryDto>
        where TEntity : IdEntity, new()
        where TViewEntity : IdEntity, new()
        where TDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public IdEntityViewService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    }
}
