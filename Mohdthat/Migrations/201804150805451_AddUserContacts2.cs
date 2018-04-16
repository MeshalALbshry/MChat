namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserContacts2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserContactID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserContactID");
        }
    }
}
