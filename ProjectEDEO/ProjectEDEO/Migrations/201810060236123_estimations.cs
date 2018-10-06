namespace Project_EDEO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class estimations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estimation",
                c => new
                    {
                        EstimationID = c.Guid(nullable: false),
                        Gender = c.Int(nullable: false),
                        Image = c.String(),
                        EstimatedAge = c.Single(nullable: false),
                        IPAddress = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EstimationID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Estimation");
        }
    }
}
