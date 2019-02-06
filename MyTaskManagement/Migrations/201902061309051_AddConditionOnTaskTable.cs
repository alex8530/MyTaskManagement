namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConditionOnTaskTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TTasks", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.TTasks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TTasks", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TTasks", new[] { "Project_Id" });
            AlterColumn("dbo.TTasks", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.TTasks", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TTasks", "Project_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TTasks", "ApplicationUser_Id");
            CreateIndex("dbo.TTasks", "Project_Id");
            AddForeignKey("dbo.TTasks", "Project_Id", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TTasks", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TTasks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TTasks", "Project_Id", "dbo.Projects");
            DropIndex("dbo.TTasks", new[] { "Project_Id" });
            DropIndex("dbo.TTasks", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.TTasks", "Project_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.TTasks", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.TTasks", "Name", c => c.String());
            CreateIndex("dbo.TTasks", "Project_Id");
            CreateIndex("dbo.TTasks", "ApplicationUser_Id");
            AddForeignKey("dbo.TTasks", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.TTasks", "Project_Id", "dbo.Projects", "Id");
        }
    }
}
