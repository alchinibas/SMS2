namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addrecordstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "RecordStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "RecordStatus");
        }
    }
}
