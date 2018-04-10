namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomEntries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Room_Id = c.Int(),
                        Sender_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Sender_Id, cascadeDelete: true)
                .Index(t => t.Room_Id)
                .Index(t => t.Sender_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomEntries", "Sender_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.RoomEntries", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.RoomEntries", new[] { "Sender_Id" });
            DropIndex("dbo.RoomEntries", new[] { "Room_Id" });
            DropTable("dbo.RoomEntries");
        }
    }
}
