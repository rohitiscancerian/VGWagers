namespace VGWagers.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Data.Entity.Migrations;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class VGWagersDB : DbContext
    {
        // Your context has been configured to use a 'test' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'VGWagers.Models.test' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'test' 
        // connection string in the application configuration file.
        public VGWagersDB()
            : base("name=DefaultConnection")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<vgw_address> vgw_address { get; set; }
        public virtual DbSet<vgw_game> vgw_game { get; set; }
        public virtual DbSet<vgw_match> vgw_match { get; set; }
        public virtual DbSet<vgw_match_users_xref> vgw_match_users_xref { get; set; }
        public virtual DbSet<vgw_platform> vgw_platform { get; set; }
        public virtual DbSet<vgw_platform_game_xref> vgw_platform_game_xref { get; set; }
        //public virtual DbSet<vgw_role> vgw_role { get; set; }
        public virtual DbSet<vgw_tournament> vgw_tournament { get; set; }
        public virtual DbSet<vgw_tournament_format> vgw_tournament_format { get; set; }
        //public virtual DbSet<vgw_user> vgw_user { get; set; }
    }


    public partial class vgw_address
    {
        [Key]
        public int ADDRESSID { get; set; }
        public string ADDRESSLINE1 { get; set; }
        public string ADDRESSLINE2 { get; set; }
        public string ADDRESSLINE3 { get; set; }
        public string CITY { get; set; }
        public Nullable<int> STATEID { get; set; }
        public Nullable<int> COUNTRYID { get; set; }
        public string ZIPCODE { get; set; }
  //      [ForeignKey("Id")]
        public int USERID { get; set; }
    //    [ForeignKey("Id")]
        public int LASTUPDATEDBYUSERID { get; set; }
        public System.DateTime LASTUPDATEDDATE { get; set; }
    }

    public partial class vgw_game
    {
        [Key]
        public int GAMEID { get; set; }
        public string GAMENAME { get; set; }
        public byte[] GAMEIMAGE { get; set; }
      //  [ForeignKey("Id")]
        public int LASTUPDATEDBYUSERID { get; set; }
        public System.DateTime LASTUPDATEDDATE { get; set; }
    }

    public partial class vgw_match
    {
        //[ForeignKey("MATCHID")]
        [Key]
        [Column(Order = 1)] 
        public int MATCHID { get; set; }
        //[ForeignKey("GAMEID")]
        [Key]
        [Column(Order = 2)] 
        public int GAMEID { get; set; }
        //[ForeignKey("PLATFORMID")]
        [Key]
        [Column(Order = 3)] 
        public int PLATFORMID { get; set; }
        public System.DateTime STARTDATE { get; set; }
        //[ForeignKey("Id")]
        public int LASTUPDATEDBYUSERID { get; set; }
        public System.DateTime LASTUPDATEDDATE { get; set; }
        public Nullable<int> DIFFICULTYLEVELID { get; set; }
        public Nullable<int> QUARTERLENGTH { get; set; }
    }

    public partial class vgw_match_users_xref
    {
        //[ForeignKey("MATCHID")]
        [Key]
        [Column(Order = 1)] 
        public int MATCHID { get; set; }

        //[ForeignKey("Id")]
        [Key]
        [Column(Order = 2)] 
        public string USERID { get; set; }
        public Nullable<decimal> SCORE { get; set; }
        public Nullable<System.DateTime> STARTTIME { get; set; }
        public Nullable<System.DateTime> ENDTIME { get; set; }
        public Nullable<bool> ISWINNER { get; set; }
        public byte[] SCOREIMAGE { get; set; }
        //[ForeignKey("Id")]
        public Nullable<int> LASTUPDATEDBYUSERID { get; set; }
        public Nullable<System.DateTime> LASTUPDATEDDATE { get; set; }
        public string TEAMNAME { get; set; }
    }

    public partial class vgw_platform
    {   
        [Key]
        public int PLATFORMID { get; set; }
        public string PLATFORMNAME { get; set; }
        //[ForeignKey("Id")]
        public int LASTUPDATEDBYUSERID { get; set; }
        public System.DateTime LASTUPDATEDDATE { get; set; }
    }

    public partial class vgw_platform_game_xref
    {
        //[ForeignKey("PLATFORMID")]

        [Key]
        [Column(Order = 1)] 
        public int PLATFORMID { get; set; }
        //[ForeignKey("GAMEID")]
        [Key]
        [Column(Order = 2)] 
        public int GAMEID { get; set; }
        public Nullable<int> LASTUPDATEDUSERID { get; set; }
        public Nullable<System.DateTime> LASTUPDATEDDATE { get; set; }
    }

    public partial class vgw_tournament
    {
        [Key]
        public int TOURNAMENTID { get; set; }
        public string TITLE { get; set; }

        //[ForeignKey("PLATFORMID")]
        public int PLATFORMID { get; set; }
        //[ForeignKey("GAMEID")]
        public int GAMEID { get; set; }
       // [ForeignKey("FORMATID")]
        public int FORMATID { get; set; }

        public Nullable<decimal> ENTRYFEE { get; set; }
        public Nullable<System.DateTime> STARTDATE { get; set; }
        public Nullable<System.DateTime> ENDDATE { get; set; }
        public Nullable<System.DateTime> REGISTRATIONCLOSEDATE { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<decimal> PRIZEAMOUNT { get; set; }
        public Nullable<int> PLAYERCOUNT { get; set; }
       // [ForeignKey("Id")]
        public int LASTUPDATEDBYUSERID { get; set; }
        public Nullable<System.DateTime> LASTUPDATEDDATE { get; set; }
    }

    public partial class vgw_tournament_format
    {
        [Key]
        public int FORMATID { get; set; }
        public string FORMATNAME { get; set; }
        public int LASTUPDATEDBYUSERID { get; set; }
        public Nullable<System.DateTime> LASTUPDATEDDATE { get; set; }
    }
}