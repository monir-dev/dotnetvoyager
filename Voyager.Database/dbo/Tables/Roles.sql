CREATE TABLE [dbo].[roles]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY,
	[name]             NVARCHAR (256) NOT NULL,
	[display_name] NVARCHAR (MAX) NULL
)
