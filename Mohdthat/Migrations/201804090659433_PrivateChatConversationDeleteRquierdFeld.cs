namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrivateChatConversationDeleteRquierdFeld : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PrivateChatConversations", "SesstionID", c => c.String());
            AlterColumn("dbo.PrivateChatConversations", "UserID1", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PrivateChatConversations", "UserID1", c => c.String(nullable: false));
            AlterColumn("dbo.PrivateChatConversations", "SesstionID", c => c.String(nullable: false));
        }
    }
}
