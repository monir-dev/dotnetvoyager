
IF NOT EXISTS (SELECT 1 FROM [dbo].[AspNetUsers])
    BEGIN
        INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[UserName]
           ,[NormalizedUserName]
           ,[Email]
           ,[NormalizedEmail]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[ConcurrencyStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnd]
           ,[LockoutEnabled]
           ,[AccessFailedCount])
     VALUES
           ('65113800-a6b5-4f91-bb12-ad398a6b9b48'
           ,'admin@gmail.com'
           ,'ADMIN@GMAIL.COM'
           ,'admin@gmail.com'
           ,'ADMIN@GMAIL.COM'
           ,1
           ,'AQAAAAEAACcQAAAAELsZMljj50P95+Kqk5Y58OY3SlAd6YFAxQL2sqWISJiVDGTNyuDxQgWxei1pCirUHA=='
           ,'FLDAS3OFYE6A2CYEGZQOULQYK5REB2Y2'
           ,'1c403edd-463b-4f48-a777-b74db6ce8e95'
           ,NULL
           ,0
           ,0
           ,NULL
           ,1
           ,0)
    END