namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfromtaskid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TTasks", "fromtaskid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TTasks", "fromtaskid");
        }
    }
}
