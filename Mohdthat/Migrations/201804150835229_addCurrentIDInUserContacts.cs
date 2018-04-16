namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCurrentIDInUserContacts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User_Contacts", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.User_Contacts", new[] { "ApplicationUserID" });
            AddColumn("dbo.User_Contacts", "CurrnetUserID", c => c.String(maxLength: 128));
            AddColumn("dbo.User_Contacts", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.User_Contacts", "CurrnetUserID");
            CreateIndex("dbo.User_Contacts", "UserID");
            AddForeignKey("dbo.User_Contacts", "CurrnetUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.User_Contacts", "UserID", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.User_Contacts", "current_user");
            DropColumn("dbo.User_Contacts", "ApplicationUserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_Contacts", "ApplicationUserID", c => c.String(maxLength: 128));
            AddColumn("dbo.User_Contacts", "current_user", c => c.String());
            DropForeignKey("dbo.User_Contacts", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.User_Contacts", "CurrnetUserID", "dbo.AspNetUsers");
            DropIndex("dbo.User_Contacts", new[] { "UserID" });
            DropIndex("dbo.User_Contacts", new[] { "CurrnetUserID" });
            DropColumn("dbo.User_Contacts", "UserID");
            DropColumn("dbo.User_Contacts", "CurrnetUserID");
            CreateIndex("dbo.User_Contacts", "ApplicationUserID");
            AddForeignKey("dbo.User_Contacts", "ApplicationUserID", "dbo.AspNetUsers", "Id");
        }
    }
}
