namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrivateChatConversationDeleteUpdatedAt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PrivateChatConversations", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrivateChatConversations", "UpdatedAt", c => c.DateTime(nullable: false));
        }
    }
}
