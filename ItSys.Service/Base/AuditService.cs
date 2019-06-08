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
    public class AuditService<TEntity, TDto, TCreateDto, TUpdateDto, TQueryDto>
        : AuditViewService<TEntity, TEntity, TDto, TCreateDto, TUpdateDto, TQueryDto>
        where TEntity : AuditEntity, new()
        where TUpdateDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public AuditService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) : base(dbContext, mapper, authContext)
        { }
    }
    public class AuditService<TEntity, TDto, TSaveDto, TQueryDto>
        : AuditViewService<TEntity, TEntity, TDto, TSaveDto, TSaveDto, TQueryDto>
        where TEntity : AuditEntity, new()
        where TSaveDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public AuditService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) : base(dbContext, mapper, authContext)
        {
            this.dbQuery = dbSet.AsQueryable();
        }
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
    public class AuditService<TEntity, TDto, TQueryDto>
        : AuditViewService<TEntity, TEntity, TDto, TDto, TQueryDto>
        where TEntity : AuditEntity, new()
        where TDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public AuditService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) : base(dbContext, mapper, authContext)
        {
            this.dbQuery = dbSet.AsQueryable();
        }
    }
}
