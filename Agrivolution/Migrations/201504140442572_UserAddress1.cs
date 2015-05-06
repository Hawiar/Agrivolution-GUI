namespace Agrivolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAddress1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address1", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address1");
        }
    }
}
