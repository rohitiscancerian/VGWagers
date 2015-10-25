namespace VGWagers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.vgw_payment", "PAYMENTDESCRIPTION", c => c.String(unicode: false));
            AddColumn("dbo.vgw_payment", "BALANCE", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.vgw_payment", "BALANCE");
            DropColumn("dbo.vgw_payment", "PAYMENTDESCRIPTION");
        }
    }
}
