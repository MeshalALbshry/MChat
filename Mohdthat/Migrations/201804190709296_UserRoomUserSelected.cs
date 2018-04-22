namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRoomUserSelected : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_Room", "UserSelected", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User_Room", "UserSelected");
        }
    }
}
