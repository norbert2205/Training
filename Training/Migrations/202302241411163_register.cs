namespace Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class register : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Login", c => c.String(unicode: false));
            AddColumn("dbo.Users", "Password", c => c.String(unicode: false));
            AddColumn("dbo.Users", "Email", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Login");
        }
    }
}
