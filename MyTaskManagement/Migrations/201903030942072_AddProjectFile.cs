namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectFile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyProjectFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        MyFileType = c.Int(nullable: false),
                        ProjectId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyProjectFiles", "ProjectId", "dbo.Projects");
            DropIndex("dbo.MyProjectFiles", new[] { "ProjectId" });
            DropTable("dbo.MyProjectFiles");
        }
    }
}
