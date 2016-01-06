namespace BidCraft.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaterialsforpostFinishforBid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bids", "ProjectFinishByDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "AreMaterialsIncluded", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "AreMaterialsIncluded");
            DropColumn("dbo.Bids", "ProjectFinishByDate");
        }
    }
}
