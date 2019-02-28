namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addManagerProjectTable : DbMigration
    {
        public override void Up()
        { 
            CreateTable(
                "dbo.ManagerProjectsTables",
                c => new
                    {
                        ProjectId = c.String(nullable: false, maxLength: 128),
                        ManagerId = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
             
        }
        
        public override void Down()
        {
             DropTable("dbo.ManagerProjectsTables");
            
        }
    }
}
