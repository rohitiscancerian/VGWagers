using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VGWagers.Models.ValidationAttributes;

namespace VGWagers.Models
{
    public class ProfileViewModel
    {
        [DisplayName("Username")]
        public string USERNAME { get; set; }

        [DisplayName("Country")]
        public string COUNTRYNAME { get; set; }

        public int SELECTEDCOUNTRYID { get; set; }

        public IList<Country> COUNTRYLIST { get; set; }

        [DisplayName("State")]
        public string STATENAME { get; set; }

        public int SELECTEDSTATEID { get; set; }

        public IList<State> STATELIST { get; set; }
        
        [DisplayName("Time-zone")]
        public string TIMEZONENAME { get; set; }

        public int SELECTEDTIMEZONEID { get; set; }

        public IList<VGWTimeZone> TIMEZONELIST { get; set; }

        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [AgeRangeValidation(ErrorMessage = "Age must be 18 yrs or above.", MinAge = 18, MaxAge = 100)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DATEOFBIRTH { get; set; }

        [DisplayName("Twitch User ID")]
        public string TWITCHUSERID { get; set; }

        [DisplayName("Xbox ID")]
        public string XBOXID { get; set; }

        [DisplayName("PSN ID")]
        public string PSNID { get; set; }
    }
}