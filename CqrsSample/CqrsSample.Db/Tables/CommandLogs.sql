CREATE TABLE [dbo].[CommandLogs]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IssuedAt] DATETIME NOT NULL, 
    [Type] NVARCHAR(256) NOT NULL, 
    [Body] NVARCHAR(MAX) NOT NULL
)

GO

CREATE INDEX [IX_CommandLogs_IssuedAt] ON [dbo].[CommandLogs] ([IssuedAt])
