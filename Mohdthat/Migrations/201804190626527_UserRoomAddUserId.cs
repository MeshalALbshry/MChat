namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRoomAddUserId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.User_Room", name: "User_Id", newName: "UserID");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.User_Room", name: "UserID", newName: "User_Id");
        }
    }
}
