USE [Digitalizacion2013]
GO
/****** Object:  StoredProcedure [dbo].[sp_Datos_Expedientes_Campos]    Script Date: 20/06/2016 11:34:50 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE [dbo].[sp_Datos_Expedientes_Campos]
(
  @cabezero Varchar(MAX),
  @Resul    Varchar(8000)  OutPut,
  @DataSet  nText,
  @Validar  Int,
  @UsuarioSistema VarChar(8000)
)

AS
BEGIN
  Set NoCount On

  -------------------------------------------------
  --Tablas de Participantes
  ------------------------------------------------

  --Variables de Trabajo
  Declare @Valor1    VarChar(8000)
  Declare @Valor2    VarChar(8000)
  Declare @Valor3    VarChar(MAX)
  Declare @Valor4    VarChar(8000)
   
  Declare @Registro  Int
  Declare @Desc0     VarChar(8000)
  Declare @Sql       nvarchar(MAX),@paramDefinition nvarchar(MAX),@paramValue varchar(8000)
  Declare @Resul2    VarChar(8000)
  Declare @LlaveBusqueda VarChar(8000)

  --Variable de Registro de Equipo
  Declare @C1 Varchar(8000)
  Declare @C2 Varchar(8000)
  Declare @C3 Varchar(8000)

  --Asignar Valores
  Select @Desc0  = ''
  Select @Resul  = ''
  Select @Sql    = ''
  Select @Resul2 = ''

  /*   -->  Indice  <--
    Validar 01 = Devuelve el Registro Solicitado en Base a idDocumentoPadre y idCampo
	Validar 02 = Grabar el Dato del campo y Expediente
	Validar 03 = Devuelve todos los campos con sus valores
  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --idDocumentoPadre
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --idCampo
  Exec ObtenerValor 'V3', @Cabezero, '|', @Valor3 Output --Valor del Campo
  Exec ObtenerValor 'V4', @Cabezero, '|', @Valor4 Output --Id de Expediente

  --Datos de Registro General
  Exec ObtenerValor 'C1', @Cabezero, '|', @C1 Output --Usuario Firmado
  Exec ObtenerValor 'C2', @Cabezero, '|', @C2 Output --Ip de Equipo Remoto
  Exec ObtenerValor 'C3', @Cabezero, '|', @C3 Output --Nombre de Usuario Firmado en el Equipo/Equipo

  -- Validar parametros
 -- If @Validar = 2 
	--Begin
	--	if LEN(LTRIM(Rtrim(@Valor2))) = 0 
	--		Begin
	--			Select @Resul = '2R=ERROR|2M=Debe de Proporcionar La Descripción|'
	--			Return
	--		End
	--End

	
-- Devuelve todos los campos de la tabla 
If @Validar = 1 -- Devuelve un Campos de un Formulario
   Begin
	 Select idDocumentoPadre, idCampo, ValorCampo,
			dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
			dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
			dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
		From Datos_Expedientes_Campos (NoLock)
	 Where idDocumentoPadre = @Valor1 And idCampo = @Valor2
     Select @Registro = @@RowCount
   End

If @Validar = 2 -- Grabar el Dato del campo y Expediente
	Begin
		if exists(Select idDocumentoPadre From Datos_Expedientes_Campos (NoLock) Where idDocumentoPadre = @Valor1 And idCampo = @Valor2)
			Begin
				--Solo Actualizar el Campo
				Update Datos_Expedientes_Campos
					Set ValorCampo = @Valor3,
						dFecha_UltimaModificacion = GetDate(),
						cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50),
						cUsuario_UltimaModificacion = @C1
				Where idDocumentoPadre = @Valor1 And idCampo = @Valor2
				Select @Registro = @@RowCount
			end 
		Else
			Begin
				--Generar un Registro nuevo
				Insert Into Datos_Expedientes_Campos ( idDocumentoPadre, idCampo, ValorCampo,
												dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
					Values(@Valor1, @Valor2, @Valor3, GetDate(), Left((@C3 + '/' +@C2),50), @C1)
				Select @Registro = @@RowCount
			End
		if @Registro > 0
			Begin
				Select idDocumentoPadre, idCampo, ValorCampo,
					dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
					dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
					dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Datos_Expedientes_Campos (NoLock)
				Where idDocumentoPadre = @Valor1 And idCampo = @Valor2
			End
	End

  If @Validar = 3 --Devuelve todos los campos con sus valores en Base a su Id de Expediente
	Begin
		if (Select IsNull(b.CamposAutomaticos,0)
				From Datos_Expedientes a (NoLock)
					Inner Join Config_Formularios b (NoLock) On a.idFormulario = b.idFormulario
				Where a.idUnicoExpediente = @Valor4 ) = 1
		  Begin

		    --Proceso de Consulta Externa desde el Campo ConsultaExterna
			SET @LlaveBusqueda = (Select idLlaveBusqueda From Datos_Expedientes Where idUnicoExpediente = @Valor4)
			SET @paramDefinition = '@nEmpleado char(8000)' 
			SET @paramValue = @LlaveBusqueda
			Select @sql = Replace(IsNull(b.ConsultaExterna,''),'"','''')
				From Datos_Expedientes a (NoLock)
					Inner Join Config_Formularios b (NoLock) On a.idFormulario = b.idFormulario
				Where a.idUnicoExpediente = @Valor4 
			EXEC sp_executesql @sql, @paramDefinition, @paramValue
			Select @Registro = 1
		  End
		
		Else
		
		  Begin
			Select aa.*, 
				   valor = IsNull((Select ValorCampo From Datos_Expedientes_Campos aaa (NoLock) Where aaa.idCampo = aa.idCampo And aaa.idDocumentoPadre = @Valor1 ),''),
				   bautomatico = 0
			From
				(Select a.idFormulario, a.idcampo, 
						b.Descripcion, TipoCampo=e.Descripcion, Clasificacion = c.Descripcion, Explicacion=IsNull(b.Explicacion,'')
					From Config_Formularios_Campos a (NoLock)
						Inner Join Config_Campos b (NoLock) On a.idCampo = b.idCampo
						Inner Join Config_Campos_Clasificacion c (NoLock) On b.Clasificacion = c.idClasificacion
						Inner Join Config_Formularios d (NoLock) On a.idFormulario = d.idFormulario
						Inner Join CTL_TiposCampos e (NoLock) On b.TipoCampo = e.idTipoCampo
					Where a.idFormulario = (Select aa.idFormulario From Datos_Expedientes aa (NoLock) Where idUnicoExpediente = @Valor1)
				Union
				Select b.idFormulario, a.idCampo, c.Descripcion, TipoCampo = e.Descripcion, Clasificacion = d.Descripcion, c.Explicacion
					From Datos_Expedientes_Campos a (NoLock)
						Inner Join Datos_Expedientes b (NoLock) On a.idDocumentoPadre = b.idUnicoExpediente
						Inner Join Config_Campos c (NoLock) On a.idCampo= c.idCampo
						Inner Join Config_Campos_Clasificacion d (NoLock) On c.Clasificacion = d.idClasificacion
						Inner Join CTL_TiposCampos e (NoLock) On c.TipoCampo = e.idTipoCampo
				Where a.idDocumentoPadre = @Valor1
					And b.idFormulario = (Select aa.idFormulario From Datos_Documentos aa (NoLock) Where idUnicoDocumento = @Valor1)) aa
				Order By aa.idCampo

				Select @Registro = @@RowCount
  		  End
		
	End

  -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 1 
        Select @Resul = '2R=OK|2M=no Existen Registros Actualmente|'
	 If @Validar = 2
		Select @Resul = '2R=ERROR|2M=Problemas al Guardar Datos de Campos de Expediente|'
     If @Validar = 3
		Select @Resul = '2R=ERROR|2M=No Existen Campos para este Expediente|'
   End
  Else
   Begin
     If @Validar = 1 Or @Validar = 2 Or @Validar = 3
        Select @Resul = '2R=OK|2M=Operación Exitosa'
   End
  Set NoCount Off
END

