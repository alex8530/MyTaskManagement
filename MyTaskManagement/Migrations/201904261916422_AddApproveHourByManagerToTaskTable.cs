namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApproveHourByManagerToTaskTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TTasks", "ApproveHourByManager", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TTasks", "ApproveHourByManager");
        }
    }
}
