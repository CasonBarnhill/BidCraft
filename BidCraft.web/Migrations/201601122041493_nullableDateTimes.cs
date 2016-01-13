namespace BidCraft.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableDateTimes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bids", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Bids", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Posts", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Posts", "ModifiedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Posts", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bids", "ModifiedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bids", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
