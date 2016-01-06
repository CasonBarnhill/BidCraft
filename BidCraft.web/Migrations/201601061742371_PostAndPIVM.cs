namespace BidCraft.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostAndPIVM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Project = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ProjectStartDate = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "ApplicationUser_Id");
            DropTable("dbo.Posts");
        }
    }
}
