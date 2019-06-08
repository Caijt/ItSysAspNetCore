using ItSys.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Repository.Sys
{
    public class SysUserRepository : BaseRepository<SysUser>
    {
        public SysUser GetById2(int id)
        {
            var b = GetById(id);
            string a = Test();
            return dbContext.Db.Queryable<SysUser>().InSingle(id);
        }

        public string Test()
        {
            return "hehe";
        }
    }
}
