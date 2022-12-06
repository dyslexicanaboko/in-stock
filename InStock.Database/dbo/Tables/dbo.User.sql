CREATE TABLE [dbo].[User]
(
[UserId] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (255) NOT NULL,
[CreateOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.User_CreatedOnUtc] DEFAULT (sysutcdatetime()),
CONSTRAINT [PK_dbo.User_UserId] PRIMARY KEY CLUSTERED ([UserId]),
CONSTRAINT [UK_dbo.User_Name] UNIQUE ([Name])
)
