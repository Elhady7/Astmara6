using Domain.Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Entities
{
    public class WorkHour:BaseIdEntity
    {
        [MaxLength(100)]
        public string Rank { get; set; }

        public int? HoursOfQuorum { get; set; }

        public bool? AcademicOrVirtual { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
