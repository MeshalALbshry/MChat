namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserContactsChangeUserAddName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_Contacts", "user_in_list", c => c.String());
            DropColumn("dbo.User_Contacts", "user_added");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_Contacts", "user_added", c => c.String());
            DropColumn("dbo.User_Contacts", "user_in_list");
        }
    }
}
