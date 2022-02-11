namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequriedinFullName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "FullName", c => c.String(nullable: false));
        }
    }
}
