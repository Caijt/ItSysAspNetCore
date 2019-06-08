using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Model
{
    public class SysUser : IdModel
    {
        public string Name { get; set; }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public UserState State { get; set; }
        public bool IsDisabled { get; set; }
    }
    public enum UserState
    {
        Logout,
        Reg,
        Login
    }
}
