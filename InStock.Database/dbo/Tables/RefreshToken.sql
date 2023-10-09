CREATE TABLE [dbo].[RefreshToken]
(
	 [RefreshTokenId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_dbo.RefreshToken_RefreshTokenId] DEFAULT (NEWID())
	,[UserId] INT NOT NULL
	,[Token] VARCHAR(255) NOT NULL
	,[CreatedOnUtc] DATETIME2(3) NOT NULL
	,[ExpiresOnUtc] DATETIME2(3) NOT NULL
	,[CreatedByIp] VARCHAR(39) NOT NULL
	,CONSTRAINT [FK_dbo.User_dbo.RefreshToken_UserId] FOREIGN KEY (UserId) REFERENCES dbo.[User](UserId)
	,CONSTRAINT [PK_dbo.RefreshToken_RefreshTokenId] PRIMARY KEY NONCLUSTERED (RefreshTokenId)
	,CONSTRAINT [UK_dbo.RefreshToken_Token] UNIQUE (Token)
);
GO

CREATE CLUSTERED INDEX [IX_dbo.RefreshToken_UserIdToken] ON [dbo].[RefreshToken] ([UserId], [Token])
GO

CREATE NONCLUSTERED INDEX [IX_dbo.RefreshToken_Token] ON [dbo].[RefreshToken] ([Token])
GO
