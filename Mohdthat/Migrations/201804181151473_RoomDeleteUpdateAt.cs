namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomDeleteUpdateAt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rooms", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "UpdatedAt", c => c.DateTime(nullable: false));
        }
    }
}
