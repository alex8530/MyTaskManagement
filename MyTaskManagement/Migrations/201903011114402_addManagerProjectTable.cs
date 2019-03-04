namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addManagerProjectTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectManagerTables",
                c => new
                    {
                        ProjectID = c.String(nullable: false, maxLength: 128),
                        ManagerID = c.String(),
                    })
                .PrimaryKey(t => t.ProjectID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProjectManagerTables");
        }
    }
}
