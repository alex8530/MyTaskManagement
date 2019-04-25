namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPaymentAndRemainToFinanical : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Financialstatus", new[] { "user__id" });
            AddColumn("dbo.Financialstatus", "Payment", c => c.Long(nullable: false));
            AddColumn("dbo.Financialstatus", "Remain", c => c.Long(nullable: false));
            CreateIndex("dbo.Financialstatus", "User__id");
            DropColumn("dbo.Financialstatus", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Financialstatus", "Total", c => c.Long(nullable: false));
            DropIndex("dbo.Financialstatus", new[] { "User__id" });
            DropColumn("dbo.Financialstatus", "Remain");
            DropColumn("dbo.Financialstatus", "Payment");
            CreateIndex("dbo.Financialstatus", "user__id");
        }
    }
}
