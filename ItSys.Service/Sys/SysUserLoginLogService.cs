using System;
using System.Collections.Generic;
using System.Text;
using ItSys.Entity;
using ItSys.EntityFramework;
using ItSys.Dto;
using AutoMapper;
using System.Linq;
using ItSys.Common;

namespace ItSys.Service
{
    public class SysUserLoginLogService : IdEntityService<SysUserLoginLog, SysUserLoginLogDto, SysUserLoginLogQueryDto>
    {
        public SysUserLoginLogService(ItSysDbContext dbContext, IMapper mapper, AuthContext authContext) : base(dbContext, mapper)
        {
            onBeforeCreate = (entity, dto) =>
            {
                entity.UserId = authContext.UserId;
                entity.Ip = authContext.LoginIP.ToString();
                entity.LoginTime = DateTime.Now;
            };
        }
        public SysUserLoginLogDto Create()
        {
            return base.Create(new SysUserLoginLogDto());
        }
    }
}
