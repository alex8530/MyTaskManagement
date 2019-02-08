namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientForigenKeyToProjectTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Projects", name: "Client_Id", newName: "ClientId");
            RenameIndex(table: "dbo.Projects", name: "IX_Client_Id", newName: "IX_ClientId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Projects", name: "IX_ClientId", newName: "IX_Client_Id");
            RenameColumn(table: "dbo.Projects", name: "ClientId", newName: "Client_Id");
        }
    }
}
