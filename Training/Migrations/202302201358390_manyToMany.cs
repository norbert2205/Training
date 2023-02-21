namespace Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manyToMany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAssignment",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        AssignmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.AssignmentId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Assignments", t => t.AssignmentId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AssignmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAssignment", "AssignmentId", "dbo.Assignments");
            DropForeignKey("dbo.UserAssignment", "UserId", "dbo.Users");
            DropIndex("dbo.UserAssignment", new[] { "AssignmentId" });
            DropIndex("dbo.UserAssignment", new[] { "UserId" });
            DropTable("dbo.UserAssignment");
        }
    }
}
