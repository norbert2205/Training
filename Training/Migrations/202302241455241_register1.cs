namespace Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class register1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Username", c => c.String(unicode: false));
            DropColumn("dbo.Users", "Login");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Login", c => c.String(unicode: false));
            DropColumn("dbo.Users", "Username");
        }
    }
}
