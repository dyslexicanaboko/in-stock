CREATE TABLE [dbo].[Quote]
(
[QuoteId] [int] NOT NULL IDENTITY(1, 1),
[StockId] [int] NOT NULL,
[Date] [date] NOT NULL,
[Price] [decimal] (10, 2) NOT NULL,
[Volume] [decimal] (10, 2) NOT NULL,
[CreateOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.Quote_CreatedOnUtc] DEFAULT (sysutcdatetime()),
CONSTRAINT [PK_dbo.Quote_QuoteId] PRIMARY KEY CLUSTERED ([QuoteId]),
CONSTRAINT [FK_dbo.Quote_dbo.Stock_StockId] FOREIGN KEY ([StockId]) REFERENCES [dbo].[Stock] ([StockId])
)
