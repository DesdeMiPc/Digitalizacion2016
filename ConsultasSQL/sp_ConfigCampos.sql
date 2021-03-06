USE [Digitalizacion2013]
GO
/****** Object:  StoredProcedure [dbo].[sp_ConfigCampos]    Script Date: 06/11/2015 02:26:59 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE [dbo].[sp_ConfigCampos]
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
  Declare @Valor6    VarChar(8000)
  Declare @Valor7    VarChar(8000)
  Declare @Valor8    VarChar(8000)
  Declare @Valor9    VarChar(8000)
    
  Declare @Registro  Int
  Declare @Desc0     VarChar(8000)
  Declare @Desc1     VarChar(8000)
  Declare @Desc2     VarChar(8000)
  Declare @Sql       VarChar(8000)
  Declare @Resul2    VarChar(8000)

  --Variable de Registro de Equipo
  Declare @C1 Varchar(8000)
  Declare @C2 Varchar(8000)
  Declare @C3 Varchar(8000)

  --Asignar Valores
  Select @Desc0  = ''
  Select @Desc1  = ''
  Select @Desc2  = ''
  Select @Resul  = ''
  Select @Sql    = ''
  Select @Resul2 = ''

  /*   -->  Indice  <--
    Validar 01 = Consulta para Grid en Mantenimiento
	Validar 02 = Devuelve un registro en base al IdCampo

	Validar 04 = Mantenimiento del Registro
  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --idCampo
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --Descripcion
  Exec ObtenerValor 'V3', @Cabezero, '|', @Valor3 Output --Explicación
  Exec ObtenerValor 'V4', @Cabezero, '|', @Valor4 Output --Tipo de Campo
  Exec ObtenerValor 'V5', @Cabezero, '|', @Valor5 Output --Clasificación
  Exec ObtenerValor 'V6', @Cabezero, '|', @Valor6 Output --Longitud
  Exec ObtenerValor 'V7', @Cabezero, '|', @Valor7 Output --Campo Valor
  Exec ObtenerValor 'V8', @Cabezero, '|', @Valor8 Output --Campo Mostrar
  Exec ObtenerValor 'V9', @Cabezero, '|', @Valor9 Output --Consulta

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
     Select id = a.idCampo, a.Descripcion, Clasificacion = b.Descripcion
    	From Config_Campos a (NoLock) 
			Inner Join Config_Campos_Clasificacion b (NoLock) On a.Clasificacion = b.idClasificacion
	 Where a.Clasificacion=(Case When @Valor5 <> '' Then @Valor5 Else a.Clasificacion End)
     Select @Registro = @@RowCount
   End

If @Validar = 2 -- Devuelve un registro en base al IdCampo
	Begin
		Select a.idCampo, a.Descripcion, a.Explicacion, a.TipoCampo, a.Clasificacion,
				a.Longitud, a.Campo_Valor, a.Campo_Mostrar, a.Consulta,
				dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
				dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
				dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Config_Campos a (NoLock)
		Where idCampo = @Valor1
		Select @Registro = @@RowCount
	End

If @Validar = 4  -- Mantenimiento del Registro
	Begin
		--Buscar si existe este Campo para realizar modificaciones
		If Exists(Select idCampo From Config_Campos Where idCampo = @Valor1)  -- Ya existe el registro
			Begin
				Update Config_Campos
					Set Descripcion = @Valor2,
						Explicacion = @Valor3,
						TipoCampo = @Valor4,
						Clasificacion = @Valor5,
						Longitud = @Valor6,
						Campo_Valor = @Valor7,
						Campo_Mostrar = @Valor8,
						Consulta = @Valor9,
						dFecha_UltimaModificacion = GetDate(),
						cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50),
						cUsuario_UltimaModificacion = @C1
					Where idCampo = @Valor1
				Select @Registro = @@RowCount
				Select a.idCampo, a.Descripcion, a.Explicacion, a.TipoCampo, a.Clasificacion,
						a.Longitud, a.Campo_Valor, a.Campo_Mostrar, a.Consulta,
						dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
						dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
						dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Config_Campos a (NoLock)
				Where idCampo = @Valor1
			End
		Else --No existe el Registro Generar uno nuevo
			Begin
				Insert Config_Campos (Descripcion, Explicacion, TipoCampo, Clasificacion, Longitud, Campo_Valor, Campo_Mostrar, Consulta,
										dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
				Values(@Valor2, @Valor3, @Valor4, @Valor5, @Valor6, @Valor7, @Valor8, @Valor9, GetDate(), Left((@C3 + '/' +@C2),50), @C1)
				Select @Registro = @@RowCount
				Select a.idCampo, a.Descripcion, a.Explicacion, a.TipoCampo, a.Clasificacion,
						a.Longitud, a.Campo_Valor, a.Campo_Mostrar, a.Consulta,
						dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
						dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
						dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Config_Campos a (NoLock)
				Where idCampo = @@IDENTITY
			End
	End

  -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 1 or @Validar = 2
        Select @Resul = '2R=OK|2M=no Existen Registros Actualmente|'
	 If @Validar = 4 
		Select @Resul = '2R=ERROR|2M=Registro no actualizado y/o creado|'
   End
  Else
   Begin
     If @Validar = 1 or @Validar = 2
        Select @Resul = '2R=OK|2M=Operación Exitosa'
	 If @Validar = 4
		Select @Resul = '2R=OK|2M=Registro actualizado y/o creado con exito'
   End
  Set NoCount Off
END


