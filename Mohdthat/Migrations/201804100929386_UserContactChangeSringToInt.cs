namespace Mohdthat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserContactChangeSringToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User_Contacts", "user_added", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User_Contacts", "user_added", c => c.String());
        }
    }
}
