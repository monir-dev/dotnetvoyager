﻿CREATE TABLE [dbo].[menus]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[name] NVARCHAR(255) NOT NULL,
	[created_at] DATETIME2 NULL DEFAULT NULL,
	[updated_at] DATETIME2 NULL DEFAULT NULL
)