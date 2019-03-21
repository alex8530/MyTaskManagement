namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsApproveByManagerToFinancailTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Financialstatus", "IsApproveByManager", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Financialstatus", "IsApproveByManager");
        }
    }
}
