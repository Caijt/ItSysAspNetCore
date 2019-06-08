using AutoMapper;
using ItSys.Dto;
using ItSys.Entity;
using ItSys.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ItSys.Common;

namespace ItSys.Service
{
    public class SysMailService : EntityService<SysMail, SysMailDto, SysMailQueryDto>
    {
        public SysMailService(ItSysDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            onBeforeUpdate = (entity, dto, props) =>
            {
                var mail = new MailHelper(
                    dto.smtp_host,
                    dto.mail,
                    dto.sender_name,
                    dto.user_name,
                    dto.password,
                    dto.is_ssl,
                    dto.port.GetValueOrDefault(0));
                string message = mail.Send(new string[] { dto.mail }, "测试邮件", "这是由IT部门信息管理系统发送的一封测试邮件");
                if (!string.IsNullOrWhiteSpace(message))
                {
                    var r = new ResultDto<string>
                    {
                        Message = "邮箱参数有误，无法保存！",
                        Data = message,
                        Code = -1
                    };
                    throw new ResultException(r);
                }

            };
        }
        public SysMailDto Get(string id)
        {
            return get(e => e.id == id, selectExpression);
        }
    }
}
