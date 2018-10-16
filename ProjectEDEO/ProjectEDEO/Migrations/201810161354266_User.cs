namespace Project_EDEO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diagnostic", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Diagnostic", "UserID");
            AddForeignKey("dbo.Diagnostic", "UserID", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Diagnostic", "UserID", "dbo.Users");
            DropIndex("dbo.Diagnostic", new[] { "UserID" });
            DropColumn("dbo.Diagnostic", "UserID");
        }
    }
}
