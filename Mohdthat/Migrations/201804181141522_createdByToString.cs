namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdByToString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rooms", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Rooms", new[] { "CreatedBy_Id" });
            AddColumn("dbo.Rooms", "CreatedBy", c => c.String());
            DropColumn("dbo.Rooms", "CreatedBy_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "CreatedBy_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Rooms", "CreatedBy");
            CreateIndex("dbo.Rooms", "CreatedBy_Id");
            AddForeignKey("dbo.Rooms", "CreatedBy_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
