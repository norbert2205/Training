namespace Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class typeChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LoginResponses", "UserId", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoginResponses", "UserId", c => c.Int(nullable: false));
        }
    }
}
