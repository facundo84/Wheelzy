USE [Wheelzy]
GO
---
CREATE TRIGGER [dbo].[TR_Quotes_ValidateZipCodesByBuyer]
ON [dbo].[quotes]
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON; --Quito el conteo de filas

	IF EXISTS (
	-- Se hace el join de una tabla temporal Inserted con la tabla 'quotes'
	SELECT 1
		FROM Inserted I
			JOIN quotes Q ON I.buyerId = Q.buyerId
			GROUP BY I.buyerId
			HAVING COUNT(DISTINCT Q.zipCodeId) > 10
	)
	BEGIN
		RAISERROR('The Buyer has exceeded the 10 zip codes limit.', 16, 1);
		ROLLBACK TRANSACTION;
	END
END;