namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomToGroup1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "CreatedBy_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Rooms", "CreatedBy_Id");
            AddForeignKey("dbo.Rooms", "CreatedBy_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Rooms", new[] { "CreatedBy_Id" });
            DropColumn("dbo.Rooms", "CreatedBy_Id");
        }
    }
}
