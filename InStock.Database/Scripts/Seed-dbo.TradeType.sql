MERGE INTO [dbo].[TradeType] AS Target
USING ( VALUES
(1, 'Buy'),
(2, 'Sell')
) 
AS SOURCE 
(
    [TradeTypeId],
    [Description]
) 
ON 
(
    Target.[TradeTypeId] = Source.[TradeTypeId]
)
WHEN MATCHED AND Target.[Description] <> Source.[Description] THEN
    UPDATE SET
        [Description] = Source.[Description]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([TradeTypeId], [Description])
    VALUES ([TradeTypeId], [Description]);
