namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowTaskToBeNullInTaskTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TTasks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TTasks", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.TTasks", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.TTasks", "ApplicationUser_Id");
            AddForeignKey("dbo.TTasks", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TTasks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TTasks", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.TTasks", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TTasks", "ApplicationUser_Id");
            AddForeignKey("dbo.TTasks", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
