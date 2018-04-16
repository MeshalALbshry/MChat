namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUserContactToUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserContacts_Id", "dbo.User_Contacts");
            DropIndex("dbo.AspNetUsers", new[] { "UserContacts_Id" });
            DropColumn("dbo.AspNetUsers", "UserContacts_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserContacts_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "UserContacts_Id");
            AddForeignKey("dbo.AspNetUsers", "UserContacts_Id", "dbo.User_Contacts", "Id");
        }
    }
}
