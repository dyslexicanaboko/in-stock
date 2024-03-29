CREATE TABLE [dbo].[Trade]
(
[TradeId] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[StockId] [int] NOT NULL,
[TradeTypeId] [int] NOT NULL,
[Price] [decimal] (10, 2) NOT NULL,
[Quantity] [decimal] (10, 2) NOT NULL,
[ExecutionDate] [datetime2](0) NOT NULL,
[Confirmation] VARCHAR(50) NULL, 
[CreateOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.Trade_CreatedOnUtc] DEFAULT (sysutcdatetime()),
[UpdatedOnUtc] [datetime2] (0) NULL,
CONSTRAINT [PK_dbo.Trade_TradeId] PRIMARY KEY NONCLUSTERED ([TradeId]),
CONSTRAINT [FK_dbo.Trade_dbo.Stock_StockId] FOREIGN KEY ([StockId]) REFERENCES [dbo].[Stock] ([StockId]),
CONSTRAINT [FK_dbo.Trade_dbo.TradeType_TradeTypeId] FOREIGN KEY ([TradeTypeId]) REFERENCES [dbo].[TradeType] ([TradeTypeId]),
CONSTRAINT [FK_dbo.Trade_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]),
CONSTRAINT [UK_dbo.Trade_UniqueUserTrade] UNIQUE ([UserId], [StockId], [TradeTypeId], [Price], [ExecutionDate])
)
GO

CREATE CLUSTERED INDEX [IX_dbo.Trade_UserIdStockId] ON [dbo].[Trade] ([UserId], [StockId])
GO
