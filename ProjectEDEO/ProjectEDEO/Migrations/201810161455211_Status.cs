namespace Project_EDEO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diagnostic", "ChronologicalAge", c => c.Int(nullable: false));
            DropColumn("dbo.Diagnostic", "ChronologicalEstimatedAge");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Diagnostic", "ChronologicalEstimatedAge", c => c.Int(nullable: false));
            DropColumn("dbo.Diagnostic", "ChronologicalAge");
        }
    }
}
