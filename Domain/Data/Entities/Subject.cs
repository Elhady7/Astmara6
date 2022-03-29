using Domain.Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Entities
{
    public class Subject:BaseEntity
    {
        [MaxLength(100)]
        public string Code { get; set; }
        public int? Academic { get; set; }
        public int? Virtual { get; set; }
        public int? Exprement { get; set; }
        public int? TotalHours { get; set; }
        public virtual ICollection<StudentStatment> StudentStatments { get; set; }
        public virtual ICollection<SubjectTeacherLoad> StudentStatmentLoads { get; set; }

        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
    }
}
