namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrivateChatEntriesChangeSenderToString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrivateChatEntries", "Sender_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PrivateChatEntries", new[] { "Sender_Id" });
            AddColumn("dbo.PrivateChatEntries", "Sender", c => c.String());
            DropColumn("dbo.PrivateChatEntries", "Sender_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrivateChatEntries", "Sender_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.PrivateChatEntries", "Sender");
            CreateIndex("dbo.PrivateChatEntries", "Sender_Id");
            AddForeignKey("dbo.PrivateChatEntries", "Sender_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
