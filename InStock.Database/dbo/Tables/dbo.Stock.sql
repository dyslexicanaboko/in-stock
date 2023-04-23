CREATE TABLE [dbo].[Stock]
(
[StockId] [int] NOT NULL IDENTITY(1, 1),
[Symbol] [varchar] (10) NOT NULL,
[Name] [nvarchar] (255) NOT NULL,
[Notes] [nvarchar] (4000) NULL,
[CreateOnUtc] [datetime2] (0) NOT NULL CONSTRAINT [DF_dbo.Stock_CreatedOnUtc] DEFAULT (sysutcdatetime()),
[UpdatedOnUtc] [datetime2] (0) NULL,
CONSTRAINT [PK_dbo.Stock_StockId] PRIMARY KEY NONCLUSTERED  ([StockId]),
CONSTRAINT [UK_dbo.Stock_Symbol] UNIQUE CLUSTERED ([Symbol])
)
