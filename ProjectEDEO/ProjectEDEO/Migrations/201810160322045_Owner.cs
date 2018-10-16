namespace Project_EDEO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Owner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Owner", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Owner");
        }
    }
}
