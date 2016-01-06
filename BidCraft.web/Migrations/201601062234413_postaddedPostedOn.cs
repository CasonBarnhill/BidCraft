namespace BidCraft.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postaddedPostedOn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PostedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "PostedOn");
        }
    }
}
