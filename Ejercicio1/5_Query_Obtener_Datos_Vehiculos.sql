-- Consulta SQL para mostrar la informacion del vehiculo
-- Nombre del comprador actual, cotizacion, estado actual con fecha.
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'Wheelzy')
BEGIN
    USE Wheelzy;
END
GO
SELECT
    B.name AS BuyerName,
    Q.amount AS CurrentQuote,
    QS.name AS CurrentStatus,
    QHS.quoteStatusDate StatusDate,
	QHS.modifiedAt ModifiedAt
FROM
    cars AS c
INNER JOIN
    quotes AS Q ON C.carId = Q.carId
INNER JOIN
    buyers AS B ON Q.buyerId = B.buyerId
INNER JOIN
    quoteStatusHistory AS QHS ON Q.quoteId = QHS.quoteId
INNER JOIN
    quoteStatus AS QS ON QHS.quoteStatusId = QS.quoteStatusId
WHERE
    Q.isCurrent = 1
    AND QHS.historyId = (
        SELECT TOP 1 historyId
        FROM quoteStatusHistory
        WHERE quoteId = Q.quoteId
        ORDER BY modifiedAt DESC
);