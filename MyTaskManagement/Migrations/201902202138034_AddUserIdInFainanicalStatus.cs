namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdInFainanicalStatus : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Financialstatus", name: "User_Id", newName: "user__id");
            RenameIndex(table: "dbo.Financialstatus", name: "IX_User_Id", newName: "IX_user__id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Financialstatus", name: "IX_user__id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Financialstatus", name: "user__id", newName: "User_Id");
        }
    }
}
