/*
   lunes, 06 de junio de 201610:52:10 a.m.
   Usuario: 
   Servidor: .
   Base de datos: Digitalizacion2013
   Aplicación: 
*/

/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Datos_Documentos_Imagenes
	(
	idUnicaImagen bigint NOT NULL IDENTITY (1, 1),
	idUnicoDocumento bigint NOT NULL,
	Imagen varchar(MAX) NOT NULL,
	Tipo varchar(50) NULL,
	Orden int NULL,
	dFecha_Registro datetime NULL,
	cMaquina_Registro varchar(50) NULL,
	cUsuario_Registro varchar(50) NULL,
	dFecha_UltimaModificacion datetime NULL,
	cMaquina_UltimaModificacion varchar(50) NULL,
	cUsuario_UltimaModificacion varchar(50) NULL,
	dFecha_Eliminacion datetime NULL,
	cMaquina_Eliminacion varchar(50) NULL,
	cUsuario_Eliminacion varchar(50) NULL,
	HashCode varchar(50) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Datos_Documentos_Imagenes SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Datos_Documentos_Imagenes ON
GO
IF EXISTS(SELECT * FROM dbo.Datos_Documentos_Imagenes)
	 EXEC('INSERT INTO dbo.Tmp_Datos_Documentos_Imagenes (idUnicaImagen, idUnicoDocumento, Imagen, Tipo, dFecha_Registro, cMaquina_Registro, cUsuario_Registro, dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion, dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion, HashCode)
		SELECT idUnicaImagen, idUnicoDocumento, Imagen, Tipo, dFecha_Registro, cMaquina_Registro, cUsuario_Registro, dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion, dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion, HashCode FROM dbo.Datos_Documentos_Imagenes WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Datos_Documentos_Imagenes OFF
GO
DROP TABLE dbo.Datos_Documentos_Imagenes
GO
EXECUTE sp_rename N'dbo.Tmp_Datos_Documentos_Imagenes', N'Datos_Documentos_Imagenes', 'OBJECT' 
GO
ALTER TABLE dbo.Datos_Documentos_Imagenes ADD CONSTRAINT
	PK_Datos_Documentos_Imagenes PRIMARY KEY CLUSTERED 
	(
	idUnicaImagen,
	idUnicoDocumento
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Datos_Documentos_Imagenes ON dbo.Datos_Documentos_Imagenes
	(
	idUnicoDocumento
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
-- =============================================
-- Author:		<InnovaWeb>
-- Create date: <13-Sept-2015>
-- Description:	<Generar codigo HASH de imagen insertada >
-- =============================================
CREATE TRIGGER [dbo].[ins_Datos_Documentos_Imagenes] 
   ON  dbo.Datos_Documentos_Imagenes
   AFTER INSERT, UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    -- Insert statements for trigger here
		Update Datos_Documentos_Imagenes
			Set HashCode = Upper(master.dbo.fn_varbintohexstr(HASHBYTES('SHA1',Convert(VarChar(8000), Datos_Documentos_Imagenes.Imagen))))
		From Datos_Documentos_Imagenes
			Inner Join Inserted On inserted.idUnicaImagen = Datos_Documentos_Imagenes.idUnicaImagen
		Where Datos_Documentos_Imagenes.idUnicaImagen = inserted.idUnicaImagen
END
GO
COMMIT
