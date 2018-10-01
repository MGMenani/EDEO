namespace Project_EDEO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diagnostic",
                c => new
                    {
                        DiagnosticID = c.Guid(nullable: false),
                        EstimatedAge = c.Int(nullable: false),
                        Image = c.String(),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        MedicalRecordID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.DiagnosticID)
                .ForeignKey("dbo.MedicalRecord", t => t.MedicalRecordID, cascadeDelete: true)
                .Index(t => t.MedicalRecordID);
            
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
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Name = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UsersClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UsersLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UsersRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersRole", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UsersLogin", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UsersClaim", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UsersRole", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.MedicalRecord", "UserID", "dbo.Users");
            DropForeignKey("dbo.Diagnostic", "MedicalRecordID", "dbo.MedicalRecord");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.UsersRole", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UsersRole", new[] { "RoleId" });
            DropIndex("dbo.UsersLogin", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UsersClaim", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.MedicalRecord", new[] { "UserID" });
            DropIndex("dbo.Diagnostic", new[] { "MedicalRecordID" });
            DropTable("dbo.Roles");
            DropTable("dbo.UsersRole");
            DropTable("dbo.UsersLogin");
            DropTable("dbo.UsersClaim");
            DropTable("dbo.Users");
            DropTable("dbo.MedicalRecord");
            DropTable("dbo.Diagnostic");
        }
    }
}
