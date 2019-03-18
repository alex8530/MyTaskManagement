namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetodoubleinFinancal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Financialstatus", "EstimatedHours", c => c.Double(nullable: false));
            AlterColumn("dbo.Financialstatus", "EffortHours", c => c.Double(nullable: false));
            AlterColumn("dbo.Financialstatus", "Payment", c => c.Double(nullable: false));
            AlterColumn("dbo.Financialstatus", "Remain", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Financialstatus", "Remain", c => c.Long(nullable: false));
            AlterColumn("dbo.Financialstatus", "Payment", c => c.Long(nullable: false));
            AlterColumn("dbo.Financialstatus", "EffortHours", c => c.Long(nullable: false));
            AlterColumn("dbo.Financialstatus", "EstimatedHours", c => c.Long(nullable: false));
        }
    }
}
