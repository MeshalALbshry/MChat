namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRoomDeleteUpdateAt1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User_Room", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.User_Room", new[] { "Room_Id" });
            RenameColumn(table: "dbo.User_Room", name: "Room_Id", newName: "RoomID");
            AlterColumn("dbo.User_Room", "RoomID", c => c.Int(nullable: false));
            CreateIndex("dbo.User_Room", "RoomID");
            AddForeignKey("dbo.User_Room", "RoomID", "dbo.Rooms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Room", "RoomID", "dbo.Rooms");
            DropIndex("dbo.User_Room", new[] { "RoomID" });
            AlterColumn("dbo.User_Room", "RoomID", c => c.Int());
            RenameColumn(table: "dbo.User_Room", name: "RoomID", newName: "Room_Id");
            CreateIndex("dbo.User_Room", "Room_Id");
            AddForeignKey("dbo.User_Room", "Room_Id", "dbo.Rooms", "Id");
        }
    }
}
