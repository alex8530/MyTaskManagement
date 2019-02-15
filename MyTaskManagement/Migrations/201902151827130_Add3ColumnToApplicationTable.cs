namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add3ColumnToApplicationTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "JopTitle", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "HourlyRate", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "O_T_H_Rate", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "O_T_H_Rate");
            DropColumn("dbo.AspNetUsers", "HourlyRate");
            DropColumn("dbo.AspNetUsers", "JopTitle");
        }
    }
}
