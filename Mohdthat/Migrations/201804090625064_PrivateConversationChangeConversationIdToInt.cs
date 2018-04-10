namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrivateConversationChangeConversationIdToInt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrivateChatEntries", "Conversation_Id", "dbo.PrivateChatConversations");
            DropIndex("dbo.PrivateChatEntries", new[] { "Conversation_Id" });
            AddColumn("dbo.PrivateChatEntries", "ConversationID", c => c.Int(nullable: false));
            DropColumn("dbo.PrivateChatEntries", "Conversation_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrivateChatEntries", "Conversation_Id", c => c.Int());
            DropColumn("dbo.PrivateChatEntries", "ConversationID");
            CreateIndex("dbo.PrivateChatEntries", "Conversation_Id");
            AddForeignKey("dbo.PrivateChatEntries", "Conversation_Id", "dbo.PrivateChatConversations", "Id");
        }
    }
}
