namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOwnerToTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TTasks", "Owner", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TTasks", "Owner");
        }
    }
}
