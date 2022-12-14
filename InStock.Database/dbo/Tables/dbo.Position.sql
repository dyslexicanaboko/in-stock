CREATE TABLE [dbo].[Position]
(
[PositionId] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[StockId] [int] NOT NULL,
[DateOpened] [date] NOT NULL,
[DateClosed] [date] NULL,
[Price] [decimal] (10, 2) NOT NULL,
[Quantity] [decimal] (10, 2) NOT NULL,
[CreateOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.Position_CreatedOnUtc] DEFAULT (sysutcdatetime()),
CONSTRAINT [PK_dbo.Position_PositionId] PRIMARY KEY CLUSTERED ([PositionId]),
CONSTRAINT [FK_dbo.Position_dbo.Stock_StockId] FOREIGN KEY ([StockId]) REFERENCES [dbo].[Stock] ([StockId]),
CONSTRAINT [FK_dbo.Position_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
)
