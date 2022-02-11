namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        DepartmentHeadId = c.Guid(),
                        RecordStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.DepartmentHeadId)
                .Index(t => t.DepartmentHeadId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(),
                        LastName = c.String(nullable: false),
                        MajorSubjectId = c.Guid(nullable: false),
                        DesignationId = c.Guid(nullable: false),
                        Startyear = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Mobile = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: true)
                .ForeignKey("dbo.MajorSubjects", t => t.MajorSubjectId, cascadeDelete: true)
                .Index(t => t.MajorSubjectId)
                .Index(t => t.DesignationId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        RecordStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MajorSubjects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        RecordStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(),
                        LastName = c.String(nullable: false),
                        FullName = c.String(nullable: false),
                        Email = c.String(),
                        Address = c.String(),
                        Mobile = c.String(nullable: false),
                        EnrolledYear = c.String(nullable: false),
                        DepartmentId = c.Guid(nullable: false),
                        GuardianFirstName = c.String(),
                        GuardianMiddleName = c.String(),
                        GuardianLastName = c.String(),
                        GuardianFullName = c.String(),
                        GuardianContact = c.String(),
                        RecordStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        RecordStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(),
                        LastName = c.String(nullable: false),
                        FullName = c.String(nullable: false),
                        Address = c.String(),
                        Mobile = c.String(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RecordStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.Students", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "DepartmentHeadId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "MajorSubjectId", "dbo.MajorSubjects");
            DropForeignKey("dbo.Teachers", "DesignationId", "dbo.Designations");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Students", new[] { "DepartmentId" });
            DropIndex("dbo.Teachers", new[] { "DesignationId" });
            DropIndex("dbo.Teachers", new[] { "MajorSubjectId" });
            DropIndex("dbo.Departments", new[] { "DepartmentHeadId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Students");
            DropTable("dbo.MajorSubjects");
            DropTable("dbo.Designations");
            DropTable("dbo.Teachers");
            DropTable("dbo.Departments");
        }
    }
}
