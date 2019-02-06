namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        AdditionInformation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        DeadTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.TTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Priority = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        DeadTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        WorkingHours = c.Int(nullable: false),
                        OverTime = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Project_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.Financialstatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        W_Hours = c.Int(nullable: false),
                        OTHours = c.Long(nullable: false),
                        Wh_Rate = c.Int(nullable: false),
                        OTH_Rate = c.Long(nullable: false),
                        Total = c.Int(nullable: false),
                        Bonus = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TTasks", t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.Id)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ApplicationUserProjects",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Project_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Project_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Project_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TTasks", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.TTasks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Financialstatus", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Financialstatus", "Id", "dbo.TTasks");
            DropForeignKey("dbo.ApplicationUserProjects", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.ApplicationUserProjects", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "Client_Id", "dbo.Clients");
            DropIndex("dbo.ApplicationUserProjects", new[] { "Project_Id" });
            DropIndex("dbo.ApplicationUserProjects", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Financialstatus", new[] { "UserId" });
            DropIndex("dbo.Financialstatus", new[] { "Id" });
            DropIndex("dbo.TTasks", new[] { "Project_Id" });
            DropIndex("dbo.TTasks", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Projects", new[] { "Client_Id" });
            DropTable("dbo.ApplicationUserProjects");
            DropTable("dbo.Financialstatus");
            DropTable("dbo.TTasks");
            DropTable("dbo.Projects");
            DropTable("dbo.Clients");
        }
    }
}
