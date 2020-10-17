CREATE TABLE [dbo].[failed_jobs]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[uuid] NVARCHAR(255) NOT NULL,
	[connection] text NOT NULL,
	[queue] text NOT NULL,
	[payload] NVARCHAR(MAX) NOT NULL,
	[exception] NVARCHAR(MAX) NOT NULL,
	[failed_at] TIMESTAMP NOT NULL,
)
