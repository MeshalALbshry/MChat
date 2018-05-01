namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReadToPrivateChat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrivateChatEntries", "Read", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrivateChatEntries", "Read");
        }
    }
}
