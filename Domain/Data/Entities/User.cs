using Domain.Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Entities
{
    public class User:BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public String Password { get; set; }
    }
}
