namespace VGWagers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gameChanges : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "vgw_platform", newName: "vgw_platform_enum");
            RenameTable(name: "vgw_platform_game_xref", newName: "vgw_game_platform_xref");
            CreateIndex("vgw_game_difficulty_level_xref", "DIFFICULTYLEVELID");
            CreateIndex("vgw_game_difficulty_level_xref", "GAMEID");
            AddForeignKey("vgw_game_difficulty_level_xref", "DIFFICULTYLEVELID", "vgw_difficulty_level_enum", "DIFFICULTYLEVELID", cascadeDelete: true);
            AddForeignKey("vgw_game_difficulty_level_xref", "GAMEID", "vgw_game", "GAMEID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("vgw_game_difficulty_level_xref", "GAMEID", "vgw_game");
            DropForeignKey("vgw_game_difficulty_level_xref", "DIFFICULTYLEVELID", "vgw_difficulty_level_enum");
            DropIndex("vgw_game_difficulty_level_xref", new[] { "GAMEID" });
            DropIndex("vgw_game_difficulty_level_xref", new[] { "DIFFICULTYLEVELID" });
            RenameTable(name: "vgw_game_platform_xref", newName: "vgw_platform_game_xref");
            RenameTable(name: "vgw_platform_enum", newName: "vgw_platform");
        }
    }
}
