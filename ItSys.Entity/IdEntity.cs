using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ItSys.Entity
{
    public abstract class IdEntity
    {
        [Column("id")]
        
        public virtual int Id { get; set; }
    }
}
