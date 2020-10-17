CREATE TABLE [dbo].[settings]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[key] NVARCHAR(255) NOT NULL, 
	[display_name] nvarchar(255) NOT NULL,
	[value] TEXT NULL, 
	[details] TEXT NULL,
	[type] NVARCHAR(255) NOT NULL,
	[order] INT NOT NULL DEFAULT 1, 
	[group] NVARCHAR(255) NULL,
)
