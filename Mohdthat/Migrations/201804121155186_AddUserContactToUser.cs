namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserContactToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserContacts_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "UserContacts_Id");
            AddForeignKey("dbo.AspNetUsers", "UserContacts_Id", "dbo.User_Contacts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserContacts_Id", "dbo.User_Contacts");
            DropIndex("dbo.AspNetUsers", new[] { "UserContacts_Id" });
            DropColumn("dbo.AspNetUsers", "UserContacts_Id");
        }
    }
}
