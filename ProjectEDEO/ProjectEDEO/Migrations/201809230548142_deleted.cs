namespace Project_EDEO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Diagnostic", "MedicalRecordID", "dbo.MedicalRecord");
            DropForeignKey("dbo.MedicalRecord", "UserID", "dbo.Users");
            DropIndex("dbo.Diagnostic", new[] { "MedicalRecordID" });
            DropIndex("dbo.MedicalRecord", new[] { "UserID" });
            DropTable("dbo.MedicalRecord");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MedicalRecord",
                c => new
                    {
                        MedicalRecordID = c.Guid(nullable: false),
                        Name = c.String(),
                        LastName = c.String(),
                        BornDate = c.DateTime(nullable: false, storeType: "date"),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MedicalRecordID);
            
            CreateIndex("dbo.MedicalRecord", "UserID");
            CreateIndex("dbo.Diagnostic", "MedicalRecordID");
            AddForeignKey("dbo.MedicalRecord", "UserID", "dbo.Users", "Id");
            AddForeignKey("dbo.Diagnostic", "MedicalRecordID", "dbo.MedicalRecord", "MedicalRecordID", cascadeDelete: true);
        }
    }
}
