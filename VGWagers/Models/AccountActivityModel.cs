using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VGWagers.Models
{
    public class AccountActivityModel
    {
        public DateTime PaymentDate { get; set; }
        public string PaymentDesc { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Balance { get; set; }
    }
}