namespace BidCraft.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteredPost2andpvm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "PostedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "Url", c => c.String());
            AddColumn("dbo.Posts", "ImageUrl", c => c.String());
            AddColumn("dbo.Posts", "Text", c => c.String());
            AddColumn("dbo.Posts", "Bid", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "Buyer_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Posts", "Creator_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "Buyer_Id");
            CreateIndex("dbo.Posts", "Creator_Id");
            AddForeignKey("dbo.Posts", "Buyer_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "Creator_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Posts", "Project");
            DropColumn("dbo.Posts", "ProjectStartDate");
            DropColumn("dbo.Posts", "Buyer");
            DropColumn("dbo.Posts", "Creator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Creator", c => c.String());
            AddColumn("dbo.Posts", "Buyer", c => c.String());
            AddColumn("dbo.Posts", "ProjectStartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "Project", c => c.String());
            DropForeignKey("dbo.Posts", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "Buyer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "Creator_Id" });
            DropIndex("dbo.Posts", new[] { "Buyer_Id" });
            DropColumn("dbo.Posts", "Creator_Id");
            DropColumn("dbo.Posts", "Buyer_Id");
            DropColumn("dbo.Posts", "Bid");
            DropColumn("dbo.Posts", "Text");
            DropColumn("dbo.Posts", "ImageUrl");
            DropColumn("dbo.Posts", "Url");
            DropColumn("dbo.Posts", "PostedOn");
            DropColumn("dbo.Posts", "StartDate");
        }
    }
}
