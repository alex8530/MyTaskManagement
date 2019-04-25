namespace MyTaskManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAmountofmoneyToPaymentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "AmountOfMoney", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "AmountOfMoney");
        }
    }
}
