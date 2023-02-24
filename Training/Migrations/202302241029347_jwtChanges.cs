namespace Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jwtChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("LoginResponses", "RefreshToken_Id", "RefreshTokens");
            DropIndex("LoginResponses", new[] { "RefreshToken_Id" });
            DropTable("RefreshTokens");
            DropTable("LoginResponses");
        }
        
        public override void Down()
        {
            CreateTable(
                "LoginResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(unicode: false),
                        Token = c.String(unicode: false),
                        AccessTokenExpiry = c.DateTime(nullable: false, precision: 0),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        ModifiedAt = c.DateTime(nullable: false, precision: 0),
                        RefreshToken_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "RefreshTokens",
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
            
            CreateIndex("LoginResponses", "RefreshToken_Id");
            AddForeignKey("LoginResponses", "RefreshToken_Id", "RefreshTokens", "Id");
        }
    }
}
