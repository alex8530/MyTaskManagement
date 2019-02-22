namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdInFainanicalStatus1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Financialstatus");
            AlterColumn("dbo.Financialstatus", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Financialstatus", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Financialstatus");
            AlterColumn("dbo.Financialstatus", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Financialstatus", "Id");
        }
    }
}
