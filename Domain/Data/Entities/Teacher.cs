using Domain.Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Entities
{
    public class Teacher: BaseEntity
    {

        [MaxLength(100)]
        public String NickName { get; set; }
        [ForeignKey("WorkHour")]
        public int? IdWorkHours { get; set; }
        public virtual WorkHour WorkHour { get; set; }
        public virtual ICollection<SubjectTeacherLoad> StudentStatmentLoads { get; set; }

        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
    }
}
