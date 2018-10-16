namespace Project_EDEO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Chronological : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diagnostic", "ChronologicalAge", c => c.Int(nullable: false));
            AddColumn("dbo.Diagnostic", "ModelEstimatedAge", c => c.Int(nullable: false));
            AddColumn("dbo.Diagnostic", "DoctorEstimatedAge", c => c.Int(nullable: false));
            AddColumn("dbo.Diagnostic", "UserID", c => c.String());
            DropColumn("dbo.Diagnostic", "EstimatedAge");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Diagnostic", "EstimatedAge", c => c.Int(nullable: false));
            DropColumn("dbo.Diagnostic", "UserID");
            DropColumn("dbo.Diagnostic", "DoctorEstimatedAge");
            DropColumn("dbo.Diagnostic", "ModelEstimatedAge");
            DropColumn("dbo.Diagnostic", "ChronologicalAge");
        }
    }
}
