namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeOwnerToCreatorInTaskTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TTasks", "Creator", c => c.String());
            DropColumn("dbo.TTasks", "Owner");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TTasks", "Owner", c => c.String());
            DropColumn("dbo.TTasks", "Creator");
        }
    }
}
