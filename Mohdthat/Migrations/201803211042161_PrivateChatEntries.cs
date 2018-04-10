namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrivateChatEntries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrivateChatEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        Conversation_Id = c.Int(nullable: false),
                        Sender_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PrivateChatConversations", t => t.Conversation_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Sender_Id)
                .Index(t => t.Conversation_Id)
                .Index(t => t.Sender_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrivateChatEntries", "Sender_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PrivateChatEntries", "Conversation_Id", "dbo.PrivateChatConversations");
            DropIndex("dbo.PrivateChatEntries", new[] { "Sender_Id" });
            DropIndex("dbo.PrivateChatEntries", new[] { "Conversation_Id" });
            DropTable("dbo.PrivateChatEntries");
        }
    }
}
