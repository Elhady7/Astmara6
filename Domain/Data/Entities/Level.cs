using Domain.Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Entities
{
    public class Level : BaseEntity
    {
        public virtual ICollection<StudentStatment> StudentStatments { get; set; }

    }
}
