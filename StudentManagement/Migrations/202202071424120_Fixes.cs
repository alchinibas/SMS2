namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "FullName");
        }
    }
}
