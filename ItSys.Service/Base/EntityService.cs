using AutoMapper;
using ItSys.Dto;
using ItSys.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItSys.Service
{
    public abstract class EntityService<TEntity, TDto, TCreateDto, TUpdateDto, TQueryDto> : EntityViewService<TEntity, TEntity, TDto, TCreateDto, TUpdateDto, TQueryDto>
        where TEntity : class, new()
        where TQueryDto : IQueryDto
    {
        public EntityService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }

    public abstract class EntityService<TEntity, TDto, TSaveDto, TQueryDto> : EntityService<TEntity, TDto, TSaveDto, TSaveDto, TQueryDto>
        where TEntity : class, new()
        where TQueryDto : IQueryDto
    {
        public EntityService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    }
    public abstract class EntityService<TEntity, TDto, TQueryDto> : EntityService<TEntity, TDto, TDto, TQueryDto>
        where TEntity : class, new()
        where TQueryDto : IQueryDto
    {
        public EntityService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    }
}
