INSERT INTO aspnetusers (Id, BirthDate, TermsAndConditionAccepted, MarketingMailersAccepted, Email, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName) 
values (1, STR_TO_DATE('1-01-1990', '%d-%m-%Y'), 1, 0, 'admin@consolecurrency.com', 0, 0, 0, 0, 0, 'SiteAdmin');

commit;