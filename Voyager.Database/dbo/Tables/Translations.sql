CREATE TABLE [dbo].[translations]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[table_name] NVARCHAR(255) NOT NULL, 
	[column_name] NVARCHAR(255) NOT NULL,
	[foreign_key] INT NOT NULL,
	[locale] NVARCHAR(255) NOT NULL,
	[value] TEXT NOT NULL,
	[created_at] DATETIME2 NULL DEFAULT NULL,
	[updated_at] DATETIME2 NULL DEFAULT NULL,

)
