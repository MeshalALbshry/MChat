namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRoomDeleteUpdateAt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User_Room", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.User_Room", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.User_Room", new[] { "Room_Id" });
            DropIndex("dbo.User_Room", new[] { "User_Id" });
            AlterColumn("dbo.User_Room", "Room_Id", c => c.Int());
            AlterColumn("dbo.User_Room", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.User_Room", "Room_Id");
            CreateIndex("dbo.User_Room", "User_Id");
            AddForeignKey("dbo.User_Room", "Room_Id", "dbo.Rooms", "Id");
            AddForeignKey("dbo.User_Room", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.User_Room", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_Room", "UpdatedAt", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.User_Room", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.User_Room", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.User_Room", new[] { "User_Id" });
            DropIndex("dbo.User_Room", new[] { "Room_Id" });
            AlterColumn("dbo.User_Room", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.User_Room", "Room_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.User_Room", "User_Id");
            CreateIndex("dbo.User_Room", "Room_Id");
            AddForeignKey("dbo.User_Room", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.User_Room", "Room_Id", "dbo.Rooms", "Id", cascadeDelete: true);
        }
    }
}
