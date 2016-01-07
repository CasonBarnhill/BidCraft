namespace BidCraft.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedPostedOn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "PostedOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "PostedOn", c => c.DateTime(nullable: false));
        }
    }
}
