namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMyFilesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        MyFileType = c.Int(nullable: false),
                        UserFileId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserFileId)
                .Index(t => t.UserFileId);
            
            DropColumn("dbo.AspNetUsers", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Image", c => c.Binary());
            DropForeignKey("dbo.MyFiles", "UserFileId", "dbo.AspNetUsers");
            DropIndex("dbo.MyFiles", new[] { "UserFileId" });
            DropTable("dbo.MyFiles");
        }
    }
}
