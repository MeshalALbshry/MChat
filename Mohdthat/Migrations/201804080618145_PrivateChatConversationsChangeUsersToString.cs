namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrivateChatConversationsChangeUsersToString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrivateChatConversations", "UserID1_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateChatConversations", "UserID2_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PrivateChatConversations", new[] { "UserID1_Id" });
            DropIndex("dbo.PrivateChatConversations", new[] { "UserID2_Id" });
            AddColumn("dbo.PrivateChatConversations", "UserID1", c => c.String(nullable: false));
            AddColumn("dbo.PrivateChatConversations", "UserID2", c => c.String());
            DropColumn("dbo.PrivateChatConversations", "UserID1_Id");
            DropColumn("dbo.PrivateChatConversations", "UserID2_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrivateChatConversations", "UserID2_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.PrivateChatConversations", "UserID1_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.PrivateChatConversations", "UserID2");
            DropColumn("dbo.PrivateChatConversations", "UserID1");
            CreateIndex("dbo.PrivateChatConversations", "UserID2_Id");
            CreateIndex("dbo.PrivateChatConversations", "UserID1_Id");
            AddForeignKey("dbo.PrivateChatConversations", "UserID2_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PrivateChatConversations", "UserID1_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
