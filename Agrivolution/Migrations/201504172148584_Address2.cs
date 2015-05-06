namespace Agrivolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address2");
        }
    }
}
