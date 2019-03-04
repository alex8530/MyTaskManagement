namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeEndTimeFromTask : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TTasks", "DeadTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TTasks", "DeadTime", c => c.DateTime(nullable: false));
        }
    }
}
