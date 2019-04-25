namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsApproveByManagerToTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TTasks", "IsApproveByManager", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TTasks", "IsApproveByManager");
        }
    }
}
