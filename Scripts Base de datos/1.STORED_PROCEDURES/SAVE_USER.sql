-------------------------------------------------
--AUTOR:	OSCAR LOZANO
--FECHA:	
--FUNCION:	REGISTRA USUARIO
--CIA:		TECHNICAL EXAM
-------------------------------------------------
IF EXISTS(SELECT  *FROM    sys.objects WHERE   object_id = OBJECT_ID(N'SAVE_USER')AND type IN ( N'P', N'PC' ))
BEGIN
	 DROP PROCEDURE SAVE_USER
	 PRINT 'DROPED PROCEDURE SAVE_USER...'
END
GO

CREATE PROCEDURE SAVE_USER(
	@ID	varchar(20),
	@NAME varchar(100),
	@BIRTH_DATE DATETIME,
	@SEX VARCHAR(1)
)
AS

BEGIN
		BEGIN TRY
			BEGIN TRAN

			-- Insertar el registro
				INSERT INTO dbo.[User] VALUES(@ID, @NAME, @BIRTH_DATE, @SEX)

			COMMIT
			SELECT 1
		END TRY
		BEGIN CATCH
			-- Error durante la transacción
			IF @@TRANCOUNT > 0
				ROLLBACK
			SELECT 0
		END CATCH
END
GO

IF EXISTS(SELECT  *FROM    sys.objects WHERE   object_id = OBJECT_ID(N'SAVE_USER')AND type IN ( N'P', N'PC' ))
	PRINT 'CREATED PROCEDURE SAVE_USER...'
GO
