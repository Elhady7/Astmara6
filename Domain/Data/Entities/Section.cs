using Domain.Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Entities
{
    public class Section: BaseEntity
    {
        [ForeignKey("Branch")]
        public int? IdBranch { get; set; }
        public virtual Branch Branch { get; set; }

    }
}
