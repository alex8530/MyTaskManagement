namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApproveHourByManagerToFinanicalTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Financialstatus", "ApproveHourByManager", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Financialstatus", "ApproveHourByManager");
        }
    }
}
