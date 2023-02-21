namespace Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schoolmap_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Description = c.String(nullable: false, unicode: false),
                        Logo = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Schools");
        }
    }
}
