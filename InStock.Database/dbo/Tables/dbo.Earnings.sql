CREATE TABLE [dbo].[Earnings]
(
[EarningsId] [int] NOT NULL IDENTITY(1, 1),
[StockId] [int] NOT NULL,
[Date] [date] NOT NULL,
[Order] [int] NOT NULL,
[CreateOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.Earnings_CreatedOnUtc] DEFAULT (sysutcdatetime()),
CONSTRAINT [PK_dbo.Earnings_EarningsId] PRIMARY KEY CLUSTERED ([EarningsId]),
CONSTRAINT [FK_dbo.Earnings_dbo.Stock_StockId] FOREIGN KEY ([StockId]) REFERENCES [dbo].[Stock] ([StockId]),
CONSTRAINT [UK_dbo.Earnings_OrderedDates] UNIQUE NONCLUSTERED ([StockId], [Order], [Date])
)
