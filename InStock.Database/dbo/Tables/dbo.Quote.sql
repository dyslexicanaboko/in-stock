CREATE TABLE [dbo].[Quote]
(
[QuoteId] [int] NOT NULL IDENTITY(1, 1),
[StockId] [int] NOT NULL,
[Date] [date] NOT NULL,
[Price] [float] NOT NULL,
[Volume] [bigint] NOT NULL,
[CreateOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.Quote_CreatedOnUtc] DEFAULT (sysutcdatetime()),
CONSTRAINT [PK_dbo.Quote_QuoteId] PRIMARY KEY NONCLUSTERED ([QuoteId]),
CONSTRAINT [FK_dbo.Quote_dbo.Stock_StockId] FOREIGN KEY ([StockId]) REFERENCES [dbo].[Stock] ([StockId])
)
GO

CREATE CLUSTERED INDEX [IX_dbo.Quote_StockId] ON [dbo].[Quote] ([StockId])
GO
