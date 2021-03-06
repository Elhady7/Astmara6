
using Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CollegeContext:DbContext
    {
        public CollegeContext() : base("name=CollegeContext")
        {


        }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<StudentStatment> StudentStatments { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectTeacher> SubjectTeachers { get; set; }
        public virtual DbSet<SubjectTeacherLoad> SubjectTeacherLoads { get; set; }

        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WorkHour> WorkHours { get; set; }
        public virtual DbSet<ExcelLimitation> ExcelLimitations { get; set; }

    }
}
