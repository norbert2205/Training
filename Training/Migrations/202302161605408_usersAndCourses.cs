namespace Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usersAndCourses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCourse",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CourseId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCourse", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.UserCourse", "UserId", "dbo.Users");
            DropIndex("dbo.UserCourse", new[] { "CourseId" });
            DropIndex("dbo.UserCourse", new[] { "UserId" });
            DropTable("dbo.UserCourse");
        }
    }
}
