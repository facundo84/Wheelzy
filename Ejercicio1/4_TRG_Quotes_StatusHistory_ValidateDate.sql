-- Nombre del trigger: `trg_quoteStatusHistory_ValidateDate`
-- Se activa para INSERT y UPDATE en la tabla `quoteStatusHistory`
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'Wheelzy')
BEGIN
    USE Wheelzy;
END
GO
CREATE TRIGGER trg_quoteStatusHistory_ValidateDate
ON quoteStatusHistory
FOR INSERT, UPDATE
AS
BEGIN
    -- Comprueba si la validacion falla
    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN quoteStatus qs ON i.quoteStatusId = qs.quoteStatusId
        WHERE 
            -- Condiciones para que falle la validacion:
            -- 1. Se requiere fecha pero no se indica
            (qs.isRequiredDate = 1 AND i.quoteStatusDate IS NULL) 
            OR
            -- 2. No se requiere fecha pero si se indica
            (qs.isRequiredDate = 0 AND i.quoteStatusDate IS NOT NULL)
    )
    BEGIN
        -- Si la validacion falla, lanza un error y revierte la operacion mediante el rollback
        RAISERROR ('The status date is invalid.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;