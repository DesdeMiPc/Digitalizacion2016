USE [Digitalizacion2013]
GO
/****** Object:  StoredProcedure [dbo].[sp_ClasificacionCampos]    Script Date: 06/11/2015 02:26:44 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE [dbo].[sp_ClasificacionCampos]
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

  --Variable de Registro de Equipo
  Declare @C1 Varchar(8000)
  Declare @C2 Varchar(8000)
  Declare @C3 Varchar(8000)

  Declare @Registro  Int
  Declare @Desc0     VarChar(8000)
  Declare @Desc1     VarChar(8000)
  Declare @Desc2     VarChar(8000)
  Declare @Sql       VarChar(8000)
  Declare @Resul2    VarChar(8000)

  --Asignar Valores
  Select @Desc0  = ''
  Select @Desc1  = ''
  Select @Desc2  = ''
  Select @Resul  = ''
  Select @Sql    = ''
  Select @Resul2 = ''

  /*   -->  Indice  <--
    Validar 01 = Consulta para Grid en Mantenimiento
	Validar 02 = Devuelve un registro en especifico en Base al idClasificacion
	Validar 03 = Mantenimiento del Registro
  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --idClasificacion
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --Descripcion

  --Datos de Registro General
  Exec ObtenerValor 'C1', @Cabezero, '|', @C1 Output --Usuario Firmado
  Exec ObtenerValor 'C2', @Cabezero, '|', @C2 Output --Ip de Equipo Remoto
  Exec ObtenerValor 'C3', @Cabezero, '|', @C3 Output --Nombre de Usuario Firmado en el Equipo/Equipo

  -- Validar parametros
  If @Validar = 2 
	Begin
		if LEN(LTRIM(Rtrim(@Valor1))) = 0 
			Begin
				Select @Resul = '2R=ERROR|2M=Debe de Proporcionar el IdClasificacion|'
				Return
			End
	End

	
-- Devuelve todos los campos de la tabla 
If @Validar = 1 -- Devuelve Todos los registros del la Tabla
   Begin
     Select id = idClasificacion, Descripcion
    	From Config_Campos_Clasificacion (NoLock)
     Select @Registro = @@RowCount
   End

if @Validar = 2 -- Devuelve un registro en especifico en Base al idClasificacion
	Begin
		Select idClasificacion, Descripcion,
			dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
			dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
			dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Config_Campos_Clasificacion
			Where idClasificacion = @Valor1
		Select @Registro = @@RowCount
	End

If @Validar = 3  -- Mantenimiento del Registro
	Begin
		--Buscar si existe este Identificador para realizar modificaciones
		If Exists(Select idClasificacion From Config_Campos_Clasificacion Where idClasificacion = @Valor1)  -- Ya existe el registro
			Begin
				Update Config_Campos_Clasificacion
					Set Descripcion = @Valor2,
						dFecha_UltimaModificacion = GetDate(),
						cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50),
						cUsuario_UltimaModificacion = @C1
				Where idClasificacion = @Valor1
				Select idClasificacion, Descripcion,
					dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
					dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
					dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Config_Campos_Clasificacion
					Where idClasificacion = @Valor1
				Select @Registro = @@RowCount
			End
		Else --No existe el Registro Generar uno nuevo
			Begin
				Insert Config_Campos_Clasificacion (Descripcion, dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
					Values(@Valor2,GetDate(), Left((@C3 + '/' +@C2),50), @C1)
				Select idClasificacion, Descripcion,
					dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
					dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
					dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Config_Campos_Clasificacion
				Where idClasificacion = @@IDENTITY
				Select @Registro = @@RowCount
			End
	End

  -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 1 
        Select @Resul = '2R=OK|2M=No Existen Registros Actualmente|'
	 If @Validar = 2
		Select @Resul = '2R=ERROR|2M=No existe el registro solicitado|'
	 If @Validar = 2
		Select @Resul = '2R=ERROR|2M=No se puedo agregar o modificar el registro|'

   End
  Else
   Begin
     If @Validar = 1 or @Validar = 2 or @Validar = 3
        Select @Resul = '2R=OK|2M=Operación Exitosa'
   End
  Set NoCount Off
END


