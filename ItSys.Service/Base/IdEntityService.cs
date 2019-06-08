using AutoMapper;
using ItSys.Dto;
using ItSys.Entity;
using ItSys.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItSys.Service
{
    public abstract class IdEntityService<TEntity, TDto, TCreateDto, TUpdateDto, TQueryDto>
        : IdEntityViewService<TEntity, TEntity, TDto, TCreateDto, TUpdateDto, TQueryDto>
        where TEntity : IdEntity, new()
        where TUpdateDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public IdEntityService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
    public abstract class IdEntityService<TEntity, TDto, TSaveDto, TQueryDto>
        : IdEntityService<TEntity, TDto, TSaveDto, TSaveDto, TQueryDto>
        where TEntity : IdEntity, new()
        where TSaveDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public IdEntityService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

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
    public abstract class IdEntityService<TEntity, TDto, TQueryDto>
        : IdEntityService<TEntity, TDto, TDto, TQueryDto>
        where TEntity : IdEntity, new()
        where TDto : IdEntityDto
        where TQueryDto : IQueryDto
    {
        public IdEntityService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
}
