namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MyFiles", newName: "MyUserFiles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MyUserFiles", newName: "MyFiles");
        }
    }
}
