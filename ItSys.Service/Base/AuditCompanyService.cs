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
    public class AuditCompanyService<TEntity, TDto, TCreateDto, TUpdateDto, TQueryDto>
        : AuditCompanyViewService<TEntity, TEntity, TDto, TCreateDto, TUpdateDto, TQueryDto>
        where TEntity : AuditCompanyEntity, new()
        where TUpdateDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public AuditCompanyService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
            : base(dbContext, mapper, authContext)
        {
            this.dbQuery = dbSet.AsQueryable();
        }
    }
    public class AuditCompanyService<TEntity, TDto, TSaveDto, TQueryDto>
        : AuditCompanyService<TEntity, TDto, TSaveDto, TSaveDto, TQueryDto>
        where TEntity : AuditCompanyEntity, new()
        where TSaveDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public AuditCompanyService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
            : base(dbContext, mapper, authContext)
        { }
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
    public class AuditCompanyService<TEntity, TDto, TQueryDto>
        : AuditCompanyService<TEntity, TDto, TDto, TQueryDto>
        where TEntity : AuditCompanyEntity, new()
        where TDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public AuditCompanyService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext)
            : base(dbContext, mapper, authContext)
        { }
    }
}
