using System;
using System.Collections.Generic;
using System.Text;
using ItSys.Entity;
using ItSys.Dto;
using ItSys.EntityFramework;
using AutoMapper;
using System.Linq;
using ItSys.Common;

namespace ItSys.Service
{
    public class AuthService
    {
        private readonly ItSysDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly SysMenuService _menuService;
        private readonly SysUserLoginLogService _loginLogService;
        private readonly AuthContext _authContext;
        public AuthService(
            ItSysDbContext dbContext, 
            IMapper mapper, 
            AuthContext authContext, 
            SysMenuService menuService,SysUserLoginLogService loginLogService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _menuService = menuService;
            _loginLogService = loginLogService;
            _authContext = authContext;
        }
        public int LoginValidate(LoginDto input)
        {
            var user = _dbContext.SysUsers
                .Where(u => u.login_name == input.username && u.Pwd == input.password)
                .FirstOrDefault();
            if (user == null)
            {
                throw new ResultException(ResultDto.Error("登录用户或登录密码错误"));
            }
            _loginLogService.Create();
            return user.Id;
        }
        /// <summary>
        /// 获取用户信息（包括用户菜单）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfoDto GetUserInfo()
        {
            int userId = _authContext.UserId;
            var dto = new UserInfoDto();
            dto.userid = _authContext.UserId;
            var user = _dbContext.SysUsers.Where(u => u.Id == userId).Select(u => new { u.Name, u.role_ids }).First();
            if (user == null)
            {
                throw new ResultException(ResultDto.Error("登录超时", 40001));
            }
            dto.username = user.Name;
            dto.menuList = _menuService.GetMiniList(new SysMenuQueryDto { user_id = userId });
            return dto;
        }

        public void ModifyPwd(ModifyPwdDto input)
        {
            if (input.newPassword != input.newPassword2)
            {
                throw new ResultException(ResultDto.Error("两次新密码输入不一致！"));
            }
            var user = _dbContext.SysUsers.First(u => u.Id == _authContext.UserId);
            if (user.Pwd != input.oldPassword)
            {
                throw new ResultException(ResultDto.Error("原密码错误"));
            }
            user.Pwd = input.newPassword;
            _dbContext.SaveChanges();
        }
    }
}
