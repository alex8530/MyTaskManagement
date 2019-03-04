namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addChangeToTask : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TTasks", "RemainingHours");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TTasks", "RemainingHours", c => c.Long(nullable: false));
        }
    }
}
