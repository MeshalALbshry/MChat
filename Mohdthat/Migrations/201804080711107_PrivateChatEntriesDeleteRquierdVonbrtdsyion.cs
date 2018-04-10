namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrivateChatEntriesDeleteRquierdVonbrtdsyion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrivateChatEntries", "Conversation_Id", "dbo.PrivateChatConversations");
            DropIndex("dbo.PrivateChatEntries", new[] { "Conversation_Id" });
            AlterColumn("dbo.PrivateChatEntries", "Conversation_Id", c => c.Int());
            CreateIndex("dbo.PrivateChatEntries", "Conversation_Id");
            AddForeignKey("dbo.PrivateChatEntries", "Conversation_Id", "dbo.PrivateChatConversations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrivateChatEntries", "Conversation_Id", "dbo.PrivateChatConversations");
            DropIndex("dbo.PrivateChatEntries", new[] { "Conversation_Id" });
            AlterColumn("dbo.PrivateChatEntries", "Conversation_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.PrivateChatEntries", "Conversation_Id");
            AddForeignKey("dbo.PrivateChatEntries", "Conversation_Id", "dbo.PrivateChatConversations", "Id", cascadeDelete: true);
        }
    }
}
