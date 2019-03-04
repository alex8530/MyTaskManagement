namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketToTaskTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TTasks", "Ticket", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TTasks", "Ticket");
        }
    }
}
