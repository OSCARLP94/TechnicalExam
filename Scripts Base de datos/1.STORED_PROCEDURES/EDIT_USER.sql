-------------------------------------------------
--AUTOR:	OSCAR LOZANO
--FECHA:	
--FUNCION:	Actualiza USUARIO
--CIA:		TECHNICAL EXAM
-------------------------------------------------
IF EXISTS(SELECT  *FROM    sys.objects WHERE   object_id = OBJECT_ID(N'EDIT_USER')AND type IN ( N'P', N'PC' ))
BEGIN
	 DROP PROCEDURE EDIT_USER
	 PRINT 'DROPED PROCEDURE EDIT_USER...'
END
GO

CREATE PROCEDURE EDIT_USER(
	@ID	varchar(20),
	@NAME varchar(100),
	@BIRTH_DATE DATETIME,
	@SEX VARCHAR(1)
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

			-- Actualizar el registro
			UPDATE dbo.[User] SET[Name] =@NAME, BirthDate = @BIRTH_DATE , Sex= @SEX WHERE Id = @ID

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

IF EXISTS(SELECT  *FROM    sys.objects WHERE   object_id = OBJECT_ID(N'EDIT_USER')AND type IN ( N'P', N'PC' ))
	PRINT 'CREATED PROCEDURE EDIT_USER...'
GO
