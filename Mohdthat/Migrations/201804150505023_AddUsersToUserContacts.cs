namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersToUserContacts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User_Contacts", "UserContacts_Id", "dbo.User_Contacts");
            DropIndex("dbo.User_Contacts", new[] { "UserContacts_Id" });
            AddColumn("dbo.User_Contacts", "ApplicationUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.User_Contacts", "ApplicationUserID");
            AddForeignKey("dbo.User_Contacts", "ApplicationUserID", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.User_Contacts", "user_in_list");
            DropColumn("dbo.User_Contacts", "UserContacts_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_Contacts", "UserContacts_Id", c => c.Int());
            AddColumn("dbo.User_Contacts", "user_in_list", c => c.String());
            DropForeignKey("dbo.User_Contacts", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.User_Contacts", new[] { "ApplicationUserID" });
            DropColumn("dbo.User_Contacts", "ApplicationUserID");
            CreateIndex("dbo.User_Contacts", "UserContacts_Id");
            AddForeignKey("dbo.User_Contacts", "UserContacts_Id", "dbo.User_Contacts", "Id");
        }
    }
}
