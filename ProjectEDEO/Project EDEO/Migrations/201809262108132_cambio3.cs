namespace Project_EDEO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambio3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpecificDiagnostic",
                c => new
                    {
                        SpecificDiagnosticID = c.Guid(nullable: false),
                        EstimatedAge = c.Int(nullable: false),
                        Image = c.String(),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        MedicalRecordID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.SpecificDiagnosticID)
                .ForeignKey("dbo.MedicalRecord", t => t.MedicalRecordID, cascadeDelete: true)
                .Index(t => t.MedicalRecordID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpecificDiagnostic", "MedicalRecordID", "dbo.MedicalRecord");
            DropIndex("dbo.SpecificDiagnostic", new[] { "MedicalRecordID" });
            DropTable("dbo.SpecificDiagnostic");
        }
    }
}
