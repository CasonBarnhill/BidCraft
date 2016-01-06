namespace BidCraft.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postandbid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bids", "Amount", c => c.Int(nullable: false));
            DropColumn("dbo.Posts", "ProjectStartDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ProjectStartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Bids", "Amount");
        }
    }
}
