using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ItSys.Entity;

namespace ItSys.Dto
{
    public class SysCompanySaveDto : IdEntityDto
    {
        [Required]
        public string name { get; set; }
        public string address { get; set; }
        public bool is_disabled { get; set; }
    }
}