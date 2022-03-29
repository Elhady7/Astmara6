using Domain.Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Entities
{
    public class StudentStatment :BaseIdEntity
    {
        [ForeignKey("Level")]
        public int? IdLevel { get; set; }
        [ForeignKey("Branch")]
        public int? IdBranch { get; set; }
        [ForeignKey("Subject")]
        public int? IdSubject { get; set; }
        public int? NumberOfStudent { get; set; }

        public string La2e7a { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Level Level { get; set; }
        public virtual Subject Subject { get; set; }

    }
}
