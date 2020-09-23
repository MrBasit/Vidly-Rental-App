namespace vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DrivingLicenseAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DrivingLicense", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DrivingLicense");
        }
    }
}
