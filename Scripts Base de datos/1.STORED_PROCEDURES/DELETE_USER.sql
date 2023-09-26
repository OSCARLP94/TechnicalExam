-------------------------------------------------
--AUTOR:	OSCAR LOZANO
--FECHA:	
--FUNCION:	ELIMINA USUARIO
--CIA:		TECHNICAL EXAM
-------------------------------------------------
IF EXISTS(SELECT  *FROM    sys.objects WHERE   object_id = OBJECT_ID(N'DELETE_USER')AND type IN ( N'P', N'PC' ))
BEGIN
	 DROP PROCEDURE DELETE_USER
	 PRINT 'DROPED PROCEDURE DELETE_USER...'
END
GO

CREATE PROCEDURE DELETE_USER(
	@ID	varchar(20)
)
AS
BEGIN
		BEGIN TRY
			BEGIN TRAN

			-- Verificar si el registro existe
			IF NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE Id = @ID)
			BEGIN
				-- El registro no existe
				ROLLBACK
				SELECT 0
			END

			-- Eliminar el registro
			DELETE FROM dbo.[User] WHERE Id = @ID

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

IF EXISTS(SELECT  *FROM    sys.objects WHERE   object_id = OBJECT_ID(N'DELETE_USER')AND type IN ( N'P', N'PC' ))
	PRINT 'CREATED PROCEDURE DELETE_USER...'
GO
