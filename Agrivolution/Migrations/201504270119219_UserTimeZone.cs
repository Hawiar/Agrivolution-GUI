namespace Agrivolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTimeZone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TimeZone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TimeZone");
        }
    }
}
