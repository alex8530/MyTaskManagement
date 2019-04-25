namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addComeFromCloneandFromUserToTaskTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TTasks", "ComeFromClone", c => c.Boolean());
            AddColumn("dbo.TTasks", "FromUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TTasks", "FromUser");
            DropColumn("dbo.TTasks", "ComeFromClone");
        }
    }
}
