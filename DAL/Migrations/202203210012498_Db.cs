namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfBranch = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdBranch = c.Int(),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.IdBranch)
                .Index(t => t.IdBranch);
            
            CreateTable(
                "dbo.SubjectTeacherLoads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdBranch = c.Int(),
                        IdSubject = c.Int(),
                        IdTeacher = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.IdBranch)
                .ForeignKey("dbo.Subjects", t => t.IdSubject)
                .ForeignKey("dbo.Teachers", t => t.IdTeacher)
                .Index(t => t.IdBranch)
                .Index(t => t.IdSubject)
                .Index(t => t.IdTeacher);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 100),
                        Academic = c.Int(),
                        Virtual = c.Int(),
                        Exprement = c.Int(),
                        TotalHours = c.Int(),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentStatments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdLevel = c.Int(),
                        IdBranch = c.Int(),
                        IdSubject = c.Int(),
                        NumberOfStudent = c.Int(),
                        La2e7a = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.IdBranch)
                .ForeignKey("dbo.Levels", t => t.IdLevel)
                .ForeignKey("dbo.Subjects", t => t.IdSubject)
                .Index(t => t.IdLevel)
                .Index(t => t.IdBranch)
                .Index(t => t.IdSubject);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubjectTeachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdBranch = c.Int(),
                        IdSubject = c.Int(),
                        hoursAca = c.Int(),
                        hoursExp = c.Int(),
                        hoursVirt = c.Int(),
                        IdTeacher = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.IdBranch)
                .ForeignKey("dbo.Subjects", t => t.IdSubject)
                .ForeignKey("dbo.Teachers", t => t.hoursAca)
                .Index(t => t.IdBranch)
                .Index(t => t.IdSubject)
                .Index(t => t.hoursAca);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NickName = c.String(maxLength: 100),
                        IdWorkHours = c.Int(),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkHours", t => t.IdWorkHours)
                .Index(t => t.IdWorkHours);
            
            CreateTable(
                "dbo.WorkHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.String(maxLength: 100),
                        HoursOfQuorum = c.Int(),
                        AcademicOrVirtual = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExcelLimitations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        studentNumRange = c.String(),
                        subjectNameRange = c.String(),
                        subjectCodeRange = c.String(),
                        rosterRange = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Password = c.String(nullable: false, maxLength: 50),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectTeacherLoads", "IdTeacher", "dbo.Teachers");
            DropForeignKey("dbo.SubjectTeacherLoads", "IdSubject", "dbo.Subjects");
            DropForeignKey("dbo.SubjectTeachers", "hoursAca", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "IdWorkHours", "dbo.WorkHours");
            DropForeignKey("dbo.SubjectTeachers", "IdSubject", "dbo.Subjects");
            DropForeignKey("dbo.SubjectTeachers", "IdBranch", "dbo.Branches");
            DropForeignKey("dbo.StudentStatments", "IdSubject", "dbo.Subjects");
            DropForeignKey("dbo.StudentStatments", "IdLevel", "dbo.Levels");
            DropForeignKey("dbo.StudentStatments", "IdBranch", "dbo.Branches");
            DropForeignKey("dbo.SubjectTeacherLoads", "IdBranch", "dbo.Branches");
            DropForeignKey("dbo.Sections", "IdBranch", "dbo.Branches");
            DropIndex("dbo.Teachers", new[] { "IdWorkHours" });
            DropIndex("dbo.SubjectTeachers", new[] { "hoursAca" });
            DropIndex("dbo.SubjectTeachers", new[] { "IdSubject" });
            DropIndex("dbo.SubjectTeachers", new[] { "IdBranch" });
            DropIndex("dbo.StudentStatments", new[] { "IdSubject" });
            DropIndex("dbo.StudentStatments", new[] { "IdBranch" });
            DropIndex("dbo.StudentStatments", new[] { "IdLevel" });
            DropIndex("dbo.SubjectTeacherLoads", new[] { "IdTeacher" });
            DropIndex("dbo.SubjectTeacherLoads", new[] { "IdSubject" });
            DropIndex("dbo.SubjectTeacherLoads", new[] { "IdBranch" });
            DropIndex("dbo.Sections", new[] { "IdBranch" });
            DropTable("dbo.Users");
            DropTable("dbo.ExcelLimitations");
            DropTable("dbo.WorkHours");
            DropTable("dbo.Teachers");
            DropTable("dbo.SubjectTeachers");
            DropTable("dbo.Levels");
            DropTable("dbo.StudentStatments");
            DropTable("dbo.Subjects");
            DropTable("dbo.SubjectTeacherLoads");
            DropTable("dbo.Sections");
            DropTable("dbo.Branches");
        }
    }
}
