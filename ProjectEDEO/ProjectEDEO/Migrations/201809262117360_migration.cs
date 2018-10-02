namespace Project_EDEO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SpecificDiagnostic", "MedicalRecordID", "dbo.MedicalRecord");
            DropIndex("dbo.SpecificDiagnostic", new[] { "MedicalRecordID" });
            DropTable("dbo.SpecificDiagnostic");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.SpecificDiagnosticID);
            
            CreateIndex("dbo.SpecificDiagnostic", "MedicalRecordID");
            AddForeignKey("dbo.SpecificDiagnostic", "MedicalRecordID", "dbo.MedicalRecord", "MedicalRecordID", cascadeDelete: true);
        }
    }
}
