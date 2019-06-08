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
    public class AttachService : IdEntityService<Attach, AttachDto, AttachSaveDto, AttachQueryDto>
    {
        private readonly AuthContext _authContext;
        public AttachService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) : base(dbContext, mapper)
        {
            _authContext = authContext;
            onBeforeCreate = (entity, dto) =>
            {
                entity.upload_user_id = authContext.UserId;
                entity.upload_time = DateTime.Now;
            };
            onWhere = (query, queryParams) =>
            {
                query = query.Where(e => e.del_time == null);
                if (queryParams.attach_guid != Guid.Empty)
                {
                    query = query.Where(e => e.attach_guid == queryParams.attach_guid);
                }
                return query;
            };
            orderDescDefaultValue = false;
        }

        public Attach GetEntity(int id)
        {
            return get(id, e => e);
        }

        public void DeleteSoft(int id)
        {
            var entity = new Attach()
            {
                Id = id,
                del_time = DateTime.Now,
                del_user_id = _authContext.UserId
            };
            updateEntity(entity, props =>
             {
                 props.Add(e => e.del_time);
                 props.Add(e => e.del_user_id);
             }, false);
        }
    }
}
