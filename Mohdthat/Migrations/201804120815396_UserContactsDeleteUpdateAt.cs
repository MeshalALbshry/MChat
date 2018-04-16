namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserContactsDeleteUpdateAt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User_Contacts", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_Contacts", "UpdatedAt", c => c.DateTime(nullable: false));
        }
    }
}
