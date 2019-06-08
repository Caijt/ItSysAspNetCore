using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    [Table("it_account")]
    public class ItAccount : AuditCompanyEntity
    {
        public string name { get; set; }
        public string login_address { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string remarks { get; set; }
        public string attach_guid { get; set; }

    }
}
