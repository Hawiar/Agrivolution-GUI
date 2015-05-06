namespace Agrivolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserZip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Zip", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Zip");
        }
    }
}
