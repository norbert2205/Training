namespace Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usertypechanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Type");
        }
    }
}
