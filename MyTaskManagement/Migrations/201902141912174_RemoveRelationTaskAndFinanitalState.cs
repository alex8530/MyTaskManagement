namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRelationTaskAndFinanitalState : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Financialstatus", "Id", "dbo.TTasks");
            DropIndex("dbo.Financialstatus", new[] { "Id" });
            DropPrimaryKey("dbo.Financialstatus");
            AlterColumn("dbo.Financialstatus", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Financialstatus", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Financialstatus");
            AlterColumn("dbo.Financialstatus", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Financialstatus", "Id");
            CreateIndex("dbo.Financialstatus", "Id");
            AddForeignKey("dbo.Financialstatus", "Id", "dbo.TTasks", "Id");
        }
    }
}
