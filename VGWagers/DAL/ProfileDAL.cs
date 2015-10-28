using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VGWagers.Models;

namespace VGWagers.DAL
{
    public class ProfileDAL
    {
         VGWagersDB dbCon;

        public ProfileDAL() 
        {
            dbCon = new VGWagersDB();
        }

        public IList<Country> GetAllCountries()
        {
            return dbCon.vgw_country.Select(d => new Country
            {
               CountryId = d.COUNTRYID ,
               CountryName = d.COUNTRYNAME 

            }).ToList();

            
        }

        public IList<State> GetAllStates()
        {
            return dbCon.vgw_state.Select(d => new State
            {
                StateId = d.STATEID,
                StateName = d.STATENAME

            }).ToList();
        }

        public IList<VGWTimeZone> GetAllTimezones()
        {
            return dbCon.vgw_timezone.Select(d => new VGWTimeZone
            {
              TimeZoneId = d.TIMEZONEID,
              TimeZoneName = d.TIMEZONENAME
            }).ToList();
        }

        public State GetStateByStateId(int id)
        {
            return dbCon.vgw_state.Where(s => s.STATEID == id).Select(sv => new State
            {
                StateId = sv.STATEID,
                StateName = sv.STATENAME
            }).FirstOrDefault();
            
        }
        public Country GetCountryByCountryId(int id)
        {
            return dbCon.vgw_country.Where(c => c.COUNTRYID == id).Select(cv => new Country
            {
                CountryId = cv.COUNTRYID,
                CountryName = cv.COUNTRYNAME

            }).FirstOrDefault();
        }
        public VGWTimeZone GetTimezoneByTimezoneId(int id)
        {
            return dbCon.vgw_timezone.Where(t => t.TIMEZONEID == id).Select(tv => new VGWTimeZone
            {
                TimeZoneId = tv.TIMEZONEID,
                TimeZoneName = tv.TIMEZONENAME
            }).FirstOrDefault();
        }

        public IQueryable<AccountActivityModel> GetAccountActivity(int userId)
        {
            return dbCon.vgw_payment.Where(t => t.USERID == userId).Select(pt => new AccountActivityModel
            {
                PaymentDate = pt.PAYMENTDATE,
                PaymentDesc = pt.PAYMENTDESCRIPTION,
                Credit = pt.AMOUNT > 0 ? pt.AMOUNT : (decimal?)null,
                Debit = pt.AMOUNT < 0 ? pt.AMOUNT : (decimal?)null,
                Balance = pt.BALANCE
            }).OrderBy(p => p.PaymentDate).AsQueryable();
        }
    }
}