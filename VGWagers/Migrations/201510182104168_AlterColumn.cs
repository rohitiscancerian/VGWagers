namespace VGWagers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.vgw_country", "LASTUPDATEDBYUSERID", c => c.Int());
            AlterColumn("dbo.vgw_state", "LASTUPDATEDBYUSERID", c => c.Int());
            AlterColumn("dbo.vgw_timezone", "LASTUPDATEDBYUSERID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.vgw_timezone", "LASTUPDATEDBYUSERID", c => c.Int(nullable: false));
            AlterColumn("dbo.vgw_state", "LASTUPDATEDBYUSERID", c => c.Int(nullable: false));
            AlterColumn("dbo.vgw_country", "LASTUPDATEDBYUSERID", c => c.Int(nullable: false));
        }
    }
}
