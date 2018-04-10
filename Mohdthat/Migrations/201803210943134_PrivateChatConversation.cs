namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrivateChatConversation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrivateChatConversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SesstionID = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        UserID1_Id = c.String(nullable: false, maxLength: 128),
                        UserID2_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID1_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID2_Id)
                .Index(t => t.UserID1_Id)
                .Index(t => t.UserID2_Id);
            
            CreateTable(
                "dbo.User_Room",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Room_Id = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Room_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Room", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.User_Room", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.PrivateChatConversations", "UserID2_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateChatConversations", "UserID1_Id", "dbo.AspNetUsers");
            DropIndex("dbo.User_Room", new[] { "User_Id" });
            DropIndex("dbo.User_Room", new[] { "Room_Id" });
            DropIndex("dbo.PrivateChatConversations", new[] { "UserID2_Id" });
            DropIndex("dbo.PrivateChatConversations", new[] { "UserID1_Id" });
            DropTable("dbo.User_Room");
            DropTable("dbo.PrivateChatConversations");
        }
    }
}
