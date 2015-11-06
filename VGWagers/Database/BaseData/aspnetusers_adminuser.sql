INSERT INTO aspnetusers (Id, DateOfBirth, TermsAndConditionAccepted, MarketingMailersAccepted, Email, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName, CountryId, StateId, TimeZoneId) 
values (2, STR_TO_DATE('1-01-1990', '%d-%m-%Y'), 1, 0, 'admin@consolecurrency.com', 0, 0, 0, 0, 0, 'SiteAdmin', 1, 1, 1);

commit;