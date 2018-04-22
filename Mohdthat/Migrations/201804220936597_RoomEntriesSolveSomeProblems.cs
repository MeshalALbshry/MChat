namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomEntriesSolveSomeProblems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomEntries", "Sender_Id", "dbo.AspNetUsers");
            DropIndex("dbo.RoomEntries", new[] { "Sender_Id" });
            RenameColumn(table: "dbo.RoomEntries", name: "Sender_Id", newName: "SenderID");
            AlterColumn("dbo.RoomEntries", "Message", c => c.String());
            AlterColumn("dbo.RoomEntries", "SenderID", c => c.String(maxLength: 128));
            CreateIndex("dbo.RoomEntries", "SenderID");
            AddForeignKey("dbo.RoomEntries", "SenderID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomEntries", "SenderID", "dbo.AspNetUsers");
            DropIndex("dbo.RoomEntries", new[] { "SenderID" });
            AlterColumn("dbo.RoomEntries", "SenderID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.RoomEntries", "Message", c => c.String(nullable: false));
            RenameColumn(table: "dbo.RoomEntries", name: "SenderID", newName: "Sender_Id");
            CreateIndex("dbo.RoomEntries", "Sender_Id");
            AddForeignKey("dbo.RoomEntries", "Sender_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
