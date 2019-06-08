using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using ItSys.Dto;
using ItSys.Entity;
using ItSys.EntityFramework;
using Microsoft.EntityFrameworkCore;
using ItSys.Common;

namespace ItSys.Service
{
    public abstract class AuditViewService
        <TEntity, TViewEntity, TDto, TCreateDto, TUpdateDto, TQueryDto> :
        IdEntityViewService<TEntity, TViewEntity, TDto, TCreateDto, TUpdateDto, TQueryDto>
        where TEntity : AuditEntity, new()
        where TViewEntity : AuditEntity, new()
        where TUpdateDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public AuditViewService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) : base(dbContext, mapper)
        {
            this.authContext = authContext;
        }

        protected override void beforeCreate(TEntity entity, TCreateDto createDto)
        {
            base.beforeCreate(entity, createDto);
            var now = DateTime.Now;
            entity.CreateTime = now;
            entity.UpdateTime = now;
            entity.CreateUserId = authContext.UserId;
            entity.UpdateUserId = authContext.UserId;
        }

        protected override void beforeUpdate(TEntity entity, TUpdateDto updateDto, List<Expression<Func<TEntity, object>>> excludeProps)
        {
            entity.UpdateTime = DateTime.Now;
            entity.UpdateUserId = authContext.UserId;
            excludeProps.Add(e => e.CreateTime);
            excludeProps.Add(e => e.CreateUserId);
        }

        protected override IQueryable<TViewEntity> buildInclude(IQueryable<TViewEntity> query)
        {
            return query.Include(e => e.CreateUser);
        }

        protected override Expression<Func<TViewEntity, dynamic>> buildOrderProp(string prop)
        {
            ;
            if (prop == "create_time")
            {
                return e => e.CreateTime;
            }
            if (prop == "update_time")
            {
                return e => e.UpdateTime;
            }
            return null;
        }

    }

    /// <summary>
    /// 简化为只需传入4个泛型，可用于创建跟更新的Dto一样的
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TSaveDto"></typeparam>
    /// <typeparam name="TQueryDto"></typeparam>
    public abstract class AuditViewService<TEntity, TViewEntity, TDto, TSaveDto, TQueryDto> :
        AuditViewService<TEntity, TViewEntity, TDto, TSaveDto, TSaveDto, TQueryDto>
        where TEntity : AuditEntity, new()
        where TViewEntity : AuditEntity, new()
        where TSaveDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public AuditViewService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) : base(dbContext, mapper, authContext) { }
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

    public abstract class AuditViewService<TEntity, TViewEntity, TDto, TQueryDto> :
        AuditViewService<TEntity, TViewEntity, TDto, TDto, TQueryDto>
        where TEntity : AuditEntity, new()
        where TViewEntity : AuditEntity, new()
        where TDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public AuditViewService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) : base(dbContext, mapper, authContext) { }
    }
}
