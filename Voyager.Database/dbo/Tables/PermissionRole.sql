CREATE TABLE [dbo].[permission_role]
(
	[role_id] INT NOT NULL, 
	[permission_id] NVARCHAR(450) NOT NULL, 
	CONSTRAINT [PK_AspNetRole_Role] PRIMARY KEY ([role_id], [permission_id])
)
