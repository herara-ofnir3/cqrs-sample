CREATE TABLE [dbo].[Posts]
(
	[Id] INT IDENTITY NOT NULL PRIMARY KEY, 
	[Version] TIMESTAMP NOT NULL,
    [Title] NVARCHAR(255) NOT NULL, 
    [Body] NVARCHAR(MAX) NOT NULL, 
    [PostedAt] DATETIME NOT NULL, 
    [UpdatedAt] DATETIME NULL, 
    [Status] INT NOT NULL, 
    CONSTRAINT [CK_Posts_Status] CHECK (Status IN (0, 1))
)
