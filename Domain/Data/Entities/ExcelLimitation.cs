using Domain.Data.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Entities
{
    public class ExcelLimitation:BaseIdEntity
    {
        public string studentNumRange { get; set; }
        public string subjectNameRange { get; set; }
        public string subjectCodeRange { get; set; }
        public string rosterRange { get; set; }
    }

}
