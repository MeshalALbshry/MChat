namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreatedAtToPrivateChatEntries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrivateChatEntries", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrivateChatEntries", "CreatedAt");
        }
    }
}
