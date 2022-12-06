CREATE TABLE [dbo].[Trade]
(
[TradeId] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[StockId] [int] NOT NULL,
[Type] [bit] NOT NULL,
[Price] [decimal] (10, 2) NOT NULL,
[Quantity] [decimal] (10, 2) NOT NULL,
[StartDate] [date] NOT NULL,
[EndDate] [date] NOT NULL,
[Confirmation] VARCHAR(50) NULL, 
[CreateOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.Trade_CreatedOnUtc] DEFAULT (sysutcdatetime()),
CONSTRAINT [PK_dbo.Trade_TradeId] PRIMARY KEY CLUSTERED ([TradeId]),
CONSTRAINT [FK_dbo.Trade_dbo.Stock_StockId] FOREIGN KEY ([StockId]) REFERENCES [dbo].[Stock] ([StockId]),
CONSTRAINT [FK_dbo.Trade_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
)
