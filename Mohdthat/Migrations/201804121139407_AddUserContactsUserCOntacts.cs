namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserContactsUserCOntacts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_Contacts", "UserContacts_Id", c => c.Int());
            CreateIndex("dbo.User_Contacts", "UserContacts_Id");
            AddForeignKey("dbo.User_Contacts", "UserContacts_Id", "dbo.User_Contacts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Contacts", "UserContacts_Id", "dbo.User_Contacts");
            DropIndex("dbo.User_Contacts", new[] { "UserContacts_Id" });
            DropColumn("dbo.User_Contacts", "UserContacts_Id");
        }
    }
}
