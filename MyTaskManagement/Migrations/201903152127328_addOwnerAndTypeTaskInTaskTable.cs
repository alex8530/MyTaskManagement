namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOwnerAndTypeTaskInTaskTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TTasks", "TypeTask", c => c.Int(nullable: false));
            AddColumn("dbo.TTasks", "Owner", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TTasks", "Owner");
            DropColumn("dbo.TTasks", "TypeTask");
        }
    }
}
