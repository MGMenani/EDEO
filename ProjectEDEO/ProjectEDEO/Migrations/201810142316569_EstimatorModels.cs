namespace Project_EDEO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstimatorModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EstimatorModel",
                c => new
                    {
                        EstimatorModelID = c.Guid(nullable: false),
                        Name = c.String(),
                        Directory = c.String(),
                        PythonFile = c.String(),
                        Active = c.Boolean(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EstimatorModelID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EstimatorModel");
        }
    }
}
