namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendaces",
                c => new
                    {
                        GigId = c.Int(nullable: false),
                        AttendeeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GigId, t.AttendeeId })
                .ForeignKey("dbo.AspNetUsers", t => t.AttendeeId, cascadeDelete: true)
                .ForeignKey("dbo.Gigs", t => t.GigId)
                .Index(t => t.GigId)
                .Index(t => t.AttendeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendaces", "GigId", "dbo.Gigs");
            DropForeignKey("dbo.Attendaces", "AttendeeId", "dbo.AspNetUsers");
            DropIndex("dbo.Attendaces", new[] { "AttendeeId" });
            DropIndex("dbo.Attendaces", new[] { "GigId" });
            DropTable("dbo.Attendaces");
        }
    }
}
