/****** Object:  StoredProcedure [dbo].[sp_ConfigFormularios]    Script Date: 22/06/2016 10:55:19 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE [dbo].[sp_ConfigFormularios]
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
  Declare @Valor3    VarChar(8000)
  Declare @Valor4    VarChar(8000)
  Declare @Valor5    VarChar(8000)
  Declare @Valor6    nvarchar(MAX)

  Declare @Registro  Int
  Declare @Desc0     VarChar(8000)
  Declare @Sql       VarChar(8000)
  Declare @Resul2    VarChar(8000)

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
    Validar 01 = Devuelve todos los registros en base a Tipo de Formulario
  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --idFormulario
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --Descripcion
  Exec ObtenerValor 'V3', @Cabezero, '|', @Valor3 Output --Clasificacion
  Exec ObtenerValor 'V4', @Cabezero, '|', @Valor4 Output --Tipo de Formulario
  Exec ObtenerValor 'V5', @Cabezero, '|', @Valor5 Output --CamposAutomaticos
  Exec ObtenerValor 'V6', @Cabezero, '|', @Valor6 Output --Consulta Externa

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
If @Validar = 1 -- Devuelve Todos los registros del la Tabla
   Begin
	 Select Id = a.idFormulario, a.Descripcion--, [Codigo de Barras] = Case When a.CamposAutomaticos = 1 Then 'Si' Else 'No' End
		From Config_Formularios a (NoLock)
	 Where a.TipoFormulario = @Valor4
     Select @Registro = @@RowCount
   End

If @Validar = 2 -- Devuelve un registro en base a su IdFormulario
	Begin
		Select idFormulario, Descripcion, Clasificacion, TipoFormulario, CamposAutomaticos, ConsultaExterna,
				dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
				dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
				dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Config_Formularios (NoLock)
		Where idFormulario = @Valor1
	End

if @Validar = 3 -- Devuelve todos los campos configurados de este formulario
	Begin
		Select d.Descripcion as Formulario, d.CamposAutomaticos,a.idFormulario, a.idcampo, 
			   b.Descripcion, TipoCampo=e.Descripcion, Clasificacion = c.Descripcion, Explicacion=IsNull(b.Explicacion,'')
			From Config_Formularios_Campos a (NoLock)
				Inner Join Config_Campos b (NoLock) On a.idCampo = b.idCampo
				Inner Join Config_Campos_Clasificacion c (NoLock) On b.Clasificacion = c.idClasificacion
				Inner Join Config_Formularios d (NoLock) On a.idFormulario = d.idFormulario
				Inner Join CTL_TiposCampos e (NoLock) On b.TipoCampo = e.idTipoCampo
			Where a.idFormulario = @Valor1
		Select @Registro = @@ROWCOUNT
	End

if @Validar = 4 -- Guardar los Datos del Formulario en la Tabla
	Begin
		-- Buscar si existe este id de Formulario
		if Exists(Select idFormulario From Config_Formularios (NoLock) Where idFormulario = @Valor1)
			Begin
				-- Actualizar el Registro 
				Update Config_Formularios
					Set Descripcion = @Valor2,
						Clasificacion = @Valor3,
						TipoFormulario = @Valor4,
						CamposAutomaticos = @Valor5,
						ConsultaExterna = @Valor6,
						dFecha_UltimaModificacion = GetDate(),
						cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50),
						cUsuario_UltimaModificacion = @C1
					Where idFormulario = @Valor1
				Select @Registro = @@ROWCOUNT
				Select idFormulario, Descripcion, Clasificacion, TipoFormulario, CamposAutomaticos, ConsultaExterna,
						dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
						dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
						dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Config_Formularios (NoLock)
					Where idFormulario = @Valor1
			End
		Else
			Begin
				Insert Config_Formularios (Descripcion, Clasificacion, TipoFormulario, CamposAutomaticos, ConsultaExterna,
											dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
				Values(@Valor2, @Valor3, @Valor4, @Valor5, @Valor6, GetDate(), Left((@C3 + '/' +@C2),50), @C1)
				Select @Registro = @@RowCount
				Select idFormulario, Descripcion, Clasificacion, TipoFormulario, CamposAutomaticos, ConsultaExterna,
						dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
						dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
						dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Config_Formularios (NoLock)
					Where idFormulario = @@Identity
			End
	End

  -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 1 Or @Validar = 3
        Select @Resul = '2R=OK|2M=no Existen Registros Actualmente|'
	If @Validar = 2
		Select @Resul = '2R=ERROR|2M=Registro solicitado no existe|'
	If @Validar = 4
		Select @Resul = '2R=ERROR|2M=No se pudo realizar la inserción/modificación del registro|'
   End
  Else
   Begin
     If @Validar = 1 or @Validar = 2 or @Validar = 3 or @Validar = 4
        Select @Resul = '2R=OK|2M=Operación Exitosa'
   End
  Set NoCount Off
END


