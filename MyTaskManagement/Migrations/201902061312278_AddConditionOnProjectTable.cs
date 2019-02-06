namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConditionOnProjectTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "Client_Id", "dbo.Clients");
            DropIndex("dbo.Projects", new[] { "Client_Id" });
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Projects", "Client_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "Client_Id");
            AddForeignKey("dbo.Projects", "Client_Id", "dbo.Clients", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Client_Id", "dbo.Clients");
            DropIndex("dbo.Projects", new[] { "Client_Id" });
            AlterColumn("dbo.Projects", "Client_Id", c => c.Int());
            AlterColumn("dbo.Projects", "Name", c => c.String());
            CreateIndex("dbo.Projects", "Client_Id");
            AddForeignKey("dbo.Projects", "Client_Id", "dbo.Clients", "Id");
        }
    }
}
