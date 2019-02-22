namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTwoColumnToFinancial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Financialstatus", "pro__id", c => c.String());
            AddColumn("dbo.Financialstatus", "task__id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Financialstatus", "task__id");
            DropColumn("dbo.Financialstatus", "pro__id");
        }
    }
}
