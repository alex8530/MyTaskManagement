namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdateColumsInTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TTasks", "EstimatedTime", c => c.Long(nullable: false));
            AddColumn("dbo.TTasks", "EffortHours", c => c.Long(nullable: false));
            AddColumn("dbo.TTasks", "RemainingHours", c => c.Long(nullable: false));
            AddColumn("dbo.TTasks", "Notes", c => c.String());
            DropColumn("dbo.TTasks", "WorkingHours");
            DropColumn("dbo.TTasks", "OverTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TTasks", "OverTime", c => c.Int(nullable: false));
            AddColumn("dbo.TTasks", "WorkingHours", c => c.Int(nullable: false));
            DropColumn("dbo.TTasks", "Notes");
            DropColumn("dbo.TTasks", "RemainingHours");
            DropColumn("dbo.TTasks", "EffortHours");
            DropColumn("dbo.TTasks", "EstimatedTime");
        }
    }
}
