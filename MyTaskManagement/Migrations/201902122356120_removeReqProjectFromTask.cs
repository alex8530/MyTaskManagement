namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeReqProjectFromTask : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TTasks", "ProjectId", "dbo.Projects");
            DropIndex("dbo.TTasks", new[] { "ProjectId" });
            AlterColumn("dbo.TTasks", "ProjectId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TTasks", "ProjectId");
            AddForeignKey("dbo.TTasks", "ProjectId", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TTasks", "ProjectId", "dbo.Projects");
            DropIndex("dbo.TTasks", new[] { "ProjectId" });
            AlterColumn("dbo.TTasks", "ProjectId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TTasks", "ProjectId");
            AddForeignKey("dbo.TTasks", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
    }
}
