namespace BidCraft.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteredPost1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Buyer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "Buyer_Id" });
            DropIndex("dbo.Posts", new[] { "Creator_Id" });
            AddColumn("dbo.Posts", "Buyer", c => c.String());
            AddColumn("dbo.Posts", "Creator", c => c.String());
            DropColumn("dbo.Posts", "Buyer_Id");
            DropColumn("dbo.Posts", "Creator_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Creator_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Posts", "Buyer_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Posts", "Creator");
            DropColumn("dbo.Posts", "Buyer");
            CreateIndex("dbo.Posts", "Creator_Id");
            CreateIndex("dbo.Posts", "Buyer_Id");
            AddForeignKey("dbo.Posts", "Creator_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "Buyer_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
