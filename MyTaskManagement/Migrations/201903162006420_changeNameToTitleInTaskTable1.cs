namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeNameToTitleInTaskTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TTasks", "Title", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.TTasks", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TTasks", "Name", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.TTasks", "Title");
        }
    }
}
