namespace BidCraft.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alteredPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Buyer_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Posts", "Creator_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "Buyer_Id");
            CreateIndex("dbo.Posts", "Creator_Id");
            AddForeignKey("dbo.Posts", "Buyer_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "Creator_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "Buyer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "Creator_Id" });
            DropIndex("dbo.Posts", new[] { "Buyer_Id" });
            DropColumn("dbo.Posts", "Creator_Id");
            DropColumn("dbo.Posts", "Buyer_Id");
        }
    }
}
