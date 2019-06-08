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
    public abstract class EntityViewService<
        TEntity,
        TViewEntity,
        TDto,
        TCreateDto,
        TUpdateDto,
        TQueryDto> : ViewService<TViewEntity, TDto, TQueryDto>
        where TEntity : class, new()
        where TViewEntity : class, new()
        where TQueryDto : IQueryDto
    {
        protected DbSet<TEntity> dbSet;

        /// <summary>
        /// 构建创建实体
        /// </summary>
        protected Action<TEntity, TCreateDto> onBeforeCreate { get; set; }
        protected Action<TEntity, TCreateDto> onAfterCreate { get; set; }

        /// <summary>
        /// 更新实体之前操作
        /// </summary>
        protected Action<TEntity, TUpdateDto, List<Expression<Func<TEntity, object>>>> onBeforeUpdate { get; set; }
        /// <summary>
        /// 更新实体后操作
        /// </summary>
        protected Action<TEntity, TUpdateDto> onAfterUpdate { get; set; }

        public EntityViewService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            this.dbSet = dbContext.Set<TEntity>();
        }

        protected void createEntity(TEntity entity)
        {
            dbSet.Add(entity);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="excludeOrIncludePropExp">不更新属性或者只更新的属性</param>
        /// <param name="isExclude">true代表excludeOrIncludeProps是不更新的属性，false为只更新的属性</param>
        protected void updateEntity(TEntity entity,
            Action<List<Expression<Func<TEntity, object>>>> excludeOrIncludePropExp = null, bool isExclude = true)
        {
            if (isExclude)
            {
                dbSet.Update(entity);
            }
            if (excludeOrIncludePropExp != null)
            {
                var props = new List<Expression<Func<TEntity, object>>>();
                excludeOrIncludePropExp(props);
                foreach (var prop in props)
                {
                    var propEntry = dbContext.Entry(entity).Property(prop);
                    if (isExclude)
                    {
                        propEntry.IsModified = false;
                    }
                    else
                    {
                        propEntry.IsModified = true;
                    }
                }
            }
            dbContext.SaveChanges();
        }

        protected virtual void beforeCreate(TEntity entiry, TCreateDto createDto)
        {

        }

        protected virtual void beforeUpdate(TEntity entiry, TUpdateDto updateDto, List<Expression<Func<TEntity, object>>> excludeProps)
        {

        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <returns></returns>
        public virtual TDto Create(TCreateDto createDto)
        {
            var entity = mapper.Map<TEntity>(createDto);

            beforeCreate(entity, createDto);
            if (onBeforeCreate != null)
            {
                onBeforeCreate(entity, createDto);
            }
            createEntity(entity);
            if (onAfterCreate != null)
            {
                onAfterCreate(entity, createDto);
            }
            return mapper.Map<TDto>(entity);
        }

        public virtual TDto CreateWithTransaction(TCreateDto createDto)
        {
            TDto dto;
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dto = Create(createDto);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

            }
            return dto;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        public virtual TDto Update(TUpdateDto updateDto)
        {
            var entity = mapper.Map<TEntity>(updateDto);
            var excludeProps = new List<Expression<Func<TEntity, object>>>();
            beforeUpdate(entity, updateDto, excludeProps);
            if (onBeforeUpdate != null)
            {
                onBeforeUpdate(entity, updateDto, excludeProps);
            }
            updateEntity(entity, props =>
            {
                props.AddRange(excludeProps);
            });
            if (onAfterUpdate != null)
            {
                onAfterUpdate(entity, updateDto);
            }
            return mapper.Map<TDto>(entity);
        }

        public virtual TDto UpdateWithTransaction(TUpdateDto updateDto)
        {
            TDto dto;
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dto = Update(updateDto);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return dto;
        }

        public virtual void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            dbContext.SaveChanges();
        }
    }
    public abstract class EntityViewService<TEntity, TViewEntity, TDto, TSaveDto, TQueryDto> : EntityViewService<TEntity, TViewEntity, TDto, TSaveDto, TSaveDto, TQueryDto>
       where TEntity : class, new()
       where TViewEntity : class, new()
       where TQueryDto : IQueryDto
    {
        public EntityViewService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    }
    public abstract class EntityViewService<TEntity, TViewEntity, TDto, TQueryDto> : EntityService<TEntity, TViewEntity, TDto, TDto, TQueryDto>
       where TEntity : class, new()
       where TViewEntity : class, new()
       where TQueryDto : IQueryDto
    {
        public EntityViewService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    }
    /// <summary>
    /// 简化为只需传入4个泛型，可用于创建跟更新的Dto一样的
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TSaveDto"></typeparam>
    /// <typeparam name="TQueryDto"></typeparam>
    
}
