namespace VGWagers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gameEntityChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.vgw_game", "CANBEPLAYEDBYTEAM", c => c.Boolean(nullable: false));
            AddColumn("dbo.vgw_game", "SORTORDER", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.vgw_game", "SORTORDER");
            DropColumn("dbo.vgw_game", "CANBEPLAYEDBYTEAM");
        }
    }
}
