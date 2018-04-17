namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserContactAddUserSelected : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_Contacts", "UserSelected", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User_Contacts", "UserSelected");
        }
    }
}
