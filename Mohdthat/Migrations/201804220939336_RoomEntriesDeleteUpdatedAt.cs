namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomEntriesDeleteUpdatedAt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RoomEntries", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoomEntries", "UpdatedAt", c => c.DateTime(nullable: false));
        }
    }
}
