namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDateToFinancial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Financialstatus", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Financialstatus", "Date");
        }
    }
}
