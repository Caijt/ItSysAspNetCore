using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class AttachDto : IdEntityDto
    {
        public string name { get; set; }
        public int size { get; set; }
        public DateTime upload_time { get; set; }
        public int upload_user_id { get; set; }
        public string upload_user_name { get; set; }
    }
}
