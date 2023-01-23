CREATE TABLE [dbo].[Earnings]
(
[EarningsId] [int] NOT NULL IDENTITY(1, 1),
[StockId] [int] NOT NULL,
[Date] [date] NOT NULL,
[Order] [int] NOT NULL,
[CreateOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.Earnings_CreatedOnUtc] DEFAULT (sysutcdatetime()),
CONSTRAINT [PK_dbo.Earnings_EarningsId] PRIMARY KEY NONCLUSTERED ([EarningsId]),
CONSTRAINT [FK_dbo.Earnings_dbo.Stock_StockId] FOREIGN KEY ([StockId]) REFERENCES [dbo].[Stock] ([StockId]),
CONSTRAINT [UK_dbo.Earnings_Order] UNIQUE NONCLUSTERED ([StockId], [Order]),
CONSTRAINT [UK_dbo.Earnings_Date] UNIQUE NONCLUSTERED ([StockId], [Date])
)
GO

CREATE CLUSTERED INDEX [IX_dbo.Earnings_StockId] ON [dbo].[Earnings] ([StockId])
GO
