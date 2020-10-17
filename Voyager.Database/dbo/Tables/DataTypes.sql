﻿CREATE TABLE [dbo].[data_types]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[name] NVARCHAR(255) NOT NULL,
	[slug] NVARCHAR(255) NOT NULL,
	[display_name_singular] NVARCHAR(255) NOT NULL,
	[display_name_plural] NVARCHAR(255) NOT NULL,
	[icon] NVARCHAR(255) NULL DEFAULT NULL,
	[model_name] NVARCHAR(255) NULL DEFAULT NULL,
	[policy_name] NVARCHAR(255) NULL DEFAULT NULL,
	[controller] NVARCHAR(255) NULL DEFAULT NULL,
	[description] NVARCHAR(255) NULL DEFAULT NULL,
	[generate_permissions] TINYINT NOT NULL DEFAULT 0, 
	[server_side] TINYINT NOT NULL DEFAULT 0, 
	[details] TEXT NULL, 
	[created_at] DATETIME2 NULL DEFAULT NULL, 
	[updated_at] DATETIME2 NULL DEFAULT NULL 
)