using Domain.Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Entities
{
    public class SubjectTeacherLoad: BaseIdEntity
    {
        [ForeignKey("Branch")]
        public int? IdBranch { get; set; }
        [ForeignKey("Subject")]
        public int? IdSubject { get; set; }
        [ForeignKey("Teacher")]

        public int? IdTeacher { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
