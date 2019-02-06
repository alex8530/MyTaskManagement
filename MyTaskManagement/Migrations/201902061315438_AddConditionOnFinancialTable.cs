namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConditionOnFinancialTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Financialstatus", name: "UserId", newName: "User_Id");
            RenameIndex(table: "dbo.Financialstatus", name: "IX_UserId", newName: "IX_User_Id");
            AlterColumn("dbo.Financialstatus", "OTHours", c => c.Int(nullable: false));
            AlterColumn("dbo.Financialstatus", "OTH_Rate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Financialstatus", "OTH_Rate", c => c.Long(nullable: false));
            AlterColumn("dbo.Financialstatus", "OTHours", c => c.Long(nullable: false));
            RenameIndex(table: "dbo.Financialstatus", name: "IX_User_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.Financialstatus", name: "User_Id", newName: "UserId");
        }
    }
}
