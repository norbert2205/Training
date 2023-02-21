namespace Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mappings_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Question = c.String(unicode: false),
                        Answer = c.String(unicode: false),
                        CorrectAnswer = c.String(unicode: false),
                        Grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Schools", "Name", c => c.String(unicode: false));
            AlterColumn("dbo.Schools", "Description", c => c.String(unicode: false));
            AlterColumn("dbo.Schools", "Logo", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Schools", "Logo", c => c.Binary(nullable: false));
            AlterColumn("dbo.Schools", "Description", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Schools", "Name", c => c.String(nullable: false, unicode: false));
            DropTable("dbo.Assignments");
            DropTable("dbo.Users");
            DropTable("dbo.Courses");
        }
    }
}
