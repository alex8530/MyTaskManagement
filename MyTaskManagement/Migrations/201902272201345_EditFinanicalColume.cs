namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditFinanicalColume : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Financialstatus", "EstimatedHours", c => c.Long(nullable: false));
            AddColumn("dbo.Financialstatus", "EffortHours", c => c.Long(nullable: false));
            AlterColumn("dbo.Financialstatus", "Total", c => c.Long(nullable: false));
            DropColumn("dbo.Financialstatus", "W_Hours");
            DropColumn("dbo.Financialstatus", "OTHours");
            DropColumn("dbo.Financialstatus", "Wh_Rate");
            DropColumn("dbo.Financialstatus", "OTH_Rate");
            DropColumn("dbo.Financialstatus", "Bonus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Financialstatus", "Bonus", c => c.Int(nullable: false));
            AddColumn("dbo.Financialstatus", "OTH_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.Financialstatus", "Wh_Rate", c => c.Int(nullable: false));
            AddColumn("dbo.Financialstatus", "OTHours", c => c.Int(nullable: false));
            AddColumn("dbo.Financialstatus", "W_Hours", c => c.Int(nullable: false));
            AlterColumn("dbo.Financialstatus", "Total", c => c.Int(nullable: false));
            DropColumn("dbo.Financialstatus", "EffortHours");
            DropColumn("dbo.Financialstatus", "EstimatedHours");
        }
    }
}
