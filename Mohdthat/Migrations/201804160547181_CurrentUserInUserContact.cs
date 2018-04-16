namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CurrentUserInUserContact : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User_Contacts", "CurrnetUserID", "dbo.AspNetUsers");
            DropIndex("dbo.User_Contacts", new[] { "CurrnetUserID" });
            AddColumn("dbo.User_Contacts", "CurrnetUser", c => c.String());
            DropColumn("dbo.User_Contacts", "CurrnetUserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_Contacts", "CurrnetUserID", c => c.String(maxLength: 128));
            DropColumn("dbo.User_Contacts", "CurrnetUser");
            CreateIndex("dbo.User_Contacts", "CurrnetUserID");
            AddForeignKey("dbo.User_Contacts", "CurrnetUserID", "dbo.AspNetUsers", "Id");
        }
    }
}
