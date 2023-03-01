namespace Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assignment_rework : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("UserAssignment", "UserId", "Users");
            DropForeignKey("UserAssignment", "AssignmentId", "Assignments");
            DropIndex("UserAssignment", new[] { "UserId" });
            DropIndex("UserAssignment", new[] { "AssignmentId" });
            AddColumn("Assignments", "User_Id", c => c.Int());
            CreateIndex("Assignments", "User_Id");
            AddForeignKey("Assignments", "User_Id", "Users", "Id");
            DropTable("UserAssignment");
        }
        
        public override void Down()
        {
            CreateTable(
                "UserAssignment",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        AssignmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.AssignmentId });
            
            DropForeignKey("Assignments", "User_Id", "Users");
            DropIndex("Assignments", new[] { "User_Id" });
            DropColumn("Assignments", "User_Id");
            CreateIndex("UserAssignment", "AssignmentId");
            CreateIndex("UserAssignment", "UserId");
            AddForeignKey("UserAssignment", "AssignmentId", "Assignments", "Id", cascadeDelete: true);
            AddForeignKey("UserAssignment", "UserId", "Users", "Id", cascadeDelete: true);
        }
    }
}
