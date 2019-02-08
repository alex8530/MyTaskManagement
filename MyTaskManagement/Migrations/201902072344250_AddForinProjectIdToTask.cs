namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForinProjectIdToTask : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TTasks", name: "Project_Id", newName: "ProjectId");
            RenameIndex(table: "dbo.TTasks", name: "IX_Project_Id", newName: "IX_ProjectId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TTasks", name: "IX_ProjectId", newName: "IX_Project_Id");
            RenameColumn(table: "dbo.TTasks", name: "ProjectId", newName: "Project_Id");
        }
    }
}
