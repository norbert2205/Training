namespace Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refreshToken : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(unicode: false),
                        TokenType = c.String(unicode: false),
                        Expiration = c.DateTime(nullable: false, precision: 0),
                        User = c.String(unicode: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        ModifiedAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoginResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Token = c.String(unicode: false),
                        AccessTokenExpiry = c.DateTime(nullable: false, precision: 0),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        ModifiedAt = c.DateTime(nullable: false, precision: 0),
                        RefreshToken_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RefreshTokens", t => t.RefreshToken_Id)
                .Index(t => t.RefreshToken_Id);
            
            AddColumn("dbo.Courses", "CreatedAt", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Courses", "ModifiedAt", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Users", "ModifiedAt", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Assignments", "CreatedAt", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Assignments", "ModifiedAt", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Schools", "CreatedAt", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Schools", "ModifiedAt", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginResponses", "RefreshToken_Id", "dbo.RefreshTokens");
            DropIndex("dbo.LoginResponses", new[] { "RefreshToken_Id" });
            DropColumn("dbo.Schools", "ModifiedAt");
            DropColumn("dbo.Schools", "CreatedAt");
            DropColumn("dbo.Assignments", "ModifiedAt");
            DropColumn("dbo.Assignments", "CreatedAt");
            DropColumn("dbo.Users", "ModifiedAt");
            DropColumn("dbo.Users", "CreatedAt");
            DropColumn("dbo.Courses", "ModifiedAt");
            DropColumn("dbo.Courses", "CreatedAt");
            DropTable("dbo.LoginResponses");
            DropTable("dbo.RefreshTokens");
        }
    }
}
