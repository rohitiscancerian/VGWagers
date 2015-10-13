namespace VGWagers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAllTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.vgw_address",
                c => new
                    {
                        ADDRESSID = c.Int(nullable: false, identity: true),
                        ADDRESSLINE1 = c.String(unicode: false),
                        ADDRESSLINE2 = c.String(unicode: false),
                        ADDRESSLINE3 = c.String(unicode: false),
                        CITY = c.String(unicode: false),
                        STATEID = c.Int(),
                        COUNTRYID = c.Int(),
                        ZIPCODE = c.String(unicode: false),
                        USERID = c.Int(nullable: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ADDRESSID);
            
            CreateTable(
                "dbo.vgw_country",
                c => new
                    {
                        COUNTRYID = c.Int(nullable: false, identity: true),
                        COUNTRYNAME = c.String(unicode: false),
                        LASTUPDATEDDATE = c.DateTime(precision: 0),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COUNTRYID);
            
            CreateTable(
                "dbo.vgw_difficulty_level_enum",
                c => new
                    {
                        DIFFICULTYLEVELID = c.Int(nullable: false, identity: true),
                        DIFFICULTYLEVEL = c.String(unicode: false),
                        ISACTIVE = c.Boolean(nullable: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.DIFFICULTYLEVELID);
            
            CreateTable(
                "dbo.vgw_game",
                c => new
                    {
                        GAMEID = c.Int(nullable: false, identity: true),
                        GAMENAME = c.String(unicode: false),
                        GAMEIMAGE = c.Binary(),
                        GENREID = c.Int(nullable: false),
                        ISACTIVE = c.Boolean(nullable: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.GAMEID);
            
            CreateTable(
                "dbo.vgw_game_difficulty_level_xref",
                c => new
                    {
                        GAMEDIFFICULTYLEVELID = c.Int(nullable: false, identity: true),
                        GAMEID = c.Int(nullable: false),
                        DIFFICULTYLEVELID = c.Int(nullable: false),
                        SORTORDER = c.Int(nullable: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.GAMEDIFFICULTYLEVELID);
            
            CreateTable(
                "dbo.vgw_genre_enum",
                c => new
                    {
                        GENREID = c.Int(nullable: false, identity: true),
                        GENRE = c.String(unicode: false),
                        ISACTIVE = c.Boolean(nullable: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.GENREID);
            
            CreateTable(
                "dbo.vgw_match",
                c => new
                    {
                        MATCHID = c.Int(nullable: false, identity: true),
                        GAMEID = c.Int(nullable: false),
                        PLATFORMID = c.Int(nullable: false),
                        STARTDATE = c.DateTime(nullable: false, precision: 0),
                        ENDDATE = c.DateTime(precision: 0),
                        QUARTERLENGTH = c.Int(),
                        WINNERUSERID = c.Int(),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.MATCHID);
            
            CreateTable(
                "dbo.vgw_match_score",
                c => new
                    {
                        MATCHSCOREID = c.Int(nullable: false, identity: true),
                        MATCHID = c.Int(nullable: false),
                        USERID = c.Int(nullable: false),
                        SCORE = c.Decimal(precision: 18, scale: 2),
                        TIMETAKEN = c.Decimal(precision: 18, scale: 2),
                        SCOREIMAGE = c.Binary(),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.MATCHSCOREID);
            
            CreateTable(
                "dbo.vgw_match_users_xref",
                c => new
                    {
                        MATCHID = c.Int(nullable: false),
                        USERID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        SCORE = c.Decimal(precision: 18, scale: 2),
                        STARTTIME = c.DateTime(precision: 0),
                        ENDTIME = c.DateTime(precision: 0),
                        ISWINNER = c.Boolean(),
                        SCOREIMAGE = c.Binary(),
                        LASTUPDATEDBYUSERID = c.Int(),
                        LASTUPDATEDDATE = c.DateTime(precision: 0),
                        TEAMNAME = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.MATCHID, t.USERID });
            
            CreateTable(
                "dbo.vgw_payment",
                c => new
                    {
                        PAYMENTID = c.Int(nullable: false, identity: true),
                        PAYMENTDATE = c.DateTime(nullable: false, precision: 0),
                        USERID = c.Int(nullable: false),
                        AMOUNT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ISPAYOUT = c.Boolean(nullable: false),
                        PAYMENTMETHODID = c.Int(nullable: false),
                        PAYMENTCARDLASTFOURDIGITS = c.Int(nullable: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.PAYMENTID);
            
            CreateTable(
                "dbo.vgw_payment_method_enum",
                c => new
                    {
                        PAYMENTMETHODID = c.Int(nullable: false, identity: true),
                        PAYMENTMETHOD = c.String(unicode: false),
                        ISACTIVE = c.Boolean(nullable: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.PAYMENTMETHODID);
            
            CreateTable(
                "dbo.vgw_platform",
                c => new
                    {
                        PLATFORMID = c.Int(nullable: false, identity: true),
                        PLATFORMNAME = c.String(unicode: false),
                        ISACTIVE = c.Boolean(nullable: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.PLATFORMID);
            
            CreateTable(
                "dbo.vgw_platform_game_xref",
                c => new
                    {
                        PLATFORMGAMEID = c.Int(nullable: false, identity: true),
                        PLATFORMID = c.Int(nullable: false),
                        GAMEID = c.Int(nullable: false),
                        ISACTIVE = c.Boolean(nullable: false),
                        LASTUPDATEDUSERID = c.Int(),
                        LASTUPDATEDDATE = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.PLATFORMGAMEID)
                .ForeignKey("dbo.vgw_game", t => t.GAMEID, cascadeDelete: true)
                .ForeignKey("dbo.vgw_platform", t => t.PLATFORMID, cascadeDelete: true)
                .Index(t => t.PLATFORMID)
                .Index(t => t.GAMEID);
            
            CreateTable(
                "dbo.vgw_state",
                c => new
                    {
                        STATEID = c.Int(nullable: false, identity: true),
                        STATENAME = c.String(unicode: false),
                        LASTUPDATEDDATE = c.DateTime(precision: 0),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.STATEID);
            
            CreateTable(
                "dbo.vgw_timezone",
                c => new
                    {
                        TIMEZONEID = c.Int(nullable: false, identity: true),
                        USEDAYLIGHTTIME = c.Boolean(nullable: false),
                        GMTDIFFERENCE = c.Int(nullable: false),
                        TIMEZONENAME = c.String(unicode: false),
                        LASTUPDATEDDATE = c.DateTime(precision: 0),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TIMEZONEID);
            
            CreateTable(
                "dbo.vgw_tournament",
                c => new
                    {
                        TOURNAMENTID = c.Int(nullable: false, identity: true),
                        TITLE = c.String(unicode: false),
                        PLATFORMID = c.Int(nullable: false),
                        GAMEID = c.Int(nullable: false),
                        FORMATID = c.Int(nullable: false),
                        ENTRYFEE = c.Decimal(precision: 18, scale: 2),
                        STARTDATE = c.DateTime(precision: 0),
                        ENDDATE = c.DateTime(precision: 0),
                        REGISTRATIONCLOSEDATE = c.DateTime(precision: 0),
                        DESCRIPTION = c.String(unicode: false),
                        PRIZEAMOUNT = c.Decimal(precision: 18, scale: 2),
                        PLAYERCOUNT = c.Int(),
                        ROUNDS = c.Int(nullable: false),
                        TOTALBUYIN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EARNINGS = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MATCHCOUNT = c.Int(nullable: false),
                        DOUBLEELIMINATION = c.Boolean(nullable: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.TOURNAMENTID);
            
            CreateTable(
                "dbo.vgw_tournament_format",
                c => new
                    {
                        FORMATID = c.Int(nullable: false, identity: true),
                        FORMATNAME = c.String(unicode: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.FORMATID);
            
            CreateTable(
                "dbo.vgw_tournament_match_xref",
                c => new
                    {
                        TOURNAMENTMATCHID = c.Int(nullable: false, identity: true),
                        TOURNAMENTID = c.Int(nullable: false),
                        MATCHID = c.Int(nullable: false),
                        ROUNDNUMBER = c.Int(nullable: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.TOURNAMENTMATCHID);
            
            CreateTable(
                "dbo.vgw_tournament_schedule",
                c => new
                    {
                        TOURNAMENTSCHEDULEID = c.Int(nullable: false, identity: true),
                        TOURNAMENTID = c.Int(nullable: false),
                        ROUNDNUMBER = c.Int(nullable: false),
                        STARTDATE = c.DateTime(precision: 0),
                        ENDDATE = c.DateTime(precision: 0),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.TOURNAMENTSCHEDULEID);
            
            CreateTable(
                "dbo.vgw_tournament_user_xref",
                c => new
                    {
                        TOURNAMENTUSERID = c.Int(nullable: false, identity: true),
                        TOURNAMENTID = c.Int(nullable: false),
                        USERID = c.Int(nullable: false),
                        SIGNEDUPDATE = c.DateTime(precision: 0),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.TOURNAMENTUSERID);
            
            CreateTable(
                "dbo.vgw_wager",
                c => new
                    {
                        WAGERID = c.Int(nullable: false, identity: true),
                        GAMEID = c.Int(nullable: false),
                        PLATFORMID = c.Int(nullable: false),
                        DIFFICULTYLEVELID = c.Int(nullable: false),
                        INITIALWAGERAMOUNT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FINALWAGERAMOUNT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        POSTEDBYUSERID = c.Int(nullable: false),
                        ACCEPTEDBYUSERID = c.Int(),
                        POSTEDDATE = c.DateTime(nullable: false, precision: 0),
                        ACCEPTEDDATE = c.DateTime(precision: 0),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.WAGERID);
            
            CreateTable(
                "dbo.vgw_wager_match_xref",
                c => new
                    {
                        WAGERMATCHID = c.Int(nullable: false, identity: true),
                        WAGERID = c.Int(nullable: false),
                        MATCHID = c.Int(nullable: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.WAGERMATCHID);
            
            CreateTable(
                "dbo.vgw_wager_revision",
                c => new
                    {
                        WAGERREVISIONID = c.Int(nullable: false, identity: true),
                        WAGERID = c.Int(nullable: false),
                        PROPOSEDBYUSERID = c.Int(nullable: false),
                        PROPOSEDAMOUNT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PROPOSEDDATE = c.DateTime(nullable: false, precision: 0),
                        ISACCEPTED = c.Boolean(nullable: false),
                        LASTUPDATEDBYUSERID = c.Int(nullable: false),
                        LASTUPDATEDDATE = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.WAGERREVISIONID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.vgw_platform_game_xref", "PLATFORMID", "dbo.vgw_platform");
            DropForeignKey("dbo.vgw_platform_game_xref", "GAMEID", "dbo.vgw_game");
            DropIndex("dbo.vgw_platform_game_xref", new[] { "GAMEID" });
            DropIndex("dbo.vgw_platform_game_xref", new[] { "PLATFORMID" });
            DropTable("dbo.vgw_wager_revision");
            DropTable("dbo.vgw_wager_match_xref");
            DropTable("dbo.vgw_wager");
            DropTable("dbo.vgw_tournament_user_xref");
            DropTable("dbo.vgw_tournament_schedule");
            DropTable("dbo.vgw_tournament_match_xref");
            DropTable("dbo.vgw_tournament_format");
            DropTable("dbo.vgw_tournament");
            DropTable("dbo.vgw_timezone");
            DropTable("dbo.vgw_state");
            DropTable("dbo.vgw_platform_game_xref");
            DropTable("dbo.vgw_platform");
            DropTable("dbo.vgw_payment_method_enum");
            DropTable("dbo.vgw_payment");
            DropTable("dbo.vgw_match_users_xref");
            DropTable("dbo.vgw_match_score");
            DropTable("dbo.vgw_match");
            DropTable("dbo.vgw_genre_enum");
            DropTable("dbo.vgw_game_difficulty_level_xref");
            DropTable("dbo.vgw_game");
            DropTable("dbo.vgw_difficulty_level_enum");
            DropTable("dbo.vgw_country");
            DropTable("dbo.vgw_address");
        }
    }
}
