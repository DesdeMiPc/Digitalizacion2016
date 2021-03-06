USE [Digitalizacion2013]
GO
/****** Object:  Trigger [dbo].[ins_Datos_Documentos_Imagenes]    Script Date: 19/10/2015 09:20:52 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<InnovaWeb>
-- Create date: <13-Sept-2015>
-- Description:	<Generar codigo HASH de imagen insertada >
-- =============================================
Alter TRIGGER [dbo].[ins_Datos_Documentos_Imagenes] 
   ON  [dbo].[Datos_Documentos_Imagenes]
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
