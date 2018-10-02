namespace Project_EDEO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelUpdated : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.MedicalRecordID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateIndex("dbo.Diagnostic", "MedicalRecordID");
            AddForeignKey("dbo.Diagnostic", "MedicalRecordID", "dbo.MedicalRecord", "MedicalRecordID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalRecord", "UserID", "dbo.Users");
            DropForeignKey("dbo.Diagnostic", "MedicalRecordID", "dbo.MedicalRecord");
            DropIndex("dbo.MedicalRecord", new[] { "UserID" });
            DropIndex("dbo.Diagnostic", new[] { "MedicalRecordID" });
            DropTable("dbo.MedicalRecord");
        }
    }
}
