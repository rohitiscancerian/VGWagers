namespace VGWagers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddrTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.vgw_address",
                c => new
                    {
                        ADDRESSID = c.Int(nullable: false, identity: true),
                        ADDRESSLINE1 = c.String(),
                        ADDRESSLINE2 = c.String(),
                        ADDRESSLINE3 = c.String(),
                        CITY = c.String(),
                        STATEID = c.Int(),
                        COUNTRYID = c.Int(),
                        ZIPCODE = c.String(),
                        USERID = c.Int(nullable: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ADDRESSID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.vgw_address");
        }
    }
}
