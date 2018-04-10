namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser_ContactsModle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User_Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        current_user = c.String(),
                        user_added = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User_Contacts");
        }
    }
}
