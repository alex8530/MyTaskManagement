namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReviewerToTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TTasks", "ReviewerId", c => c.String());
            AddColumn("dbo.TTasks", "ReviewerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TTasks", "ReviewerName");
            DropColumn("dbo.TTasks", "ReviewerId");
        }
    }
}
