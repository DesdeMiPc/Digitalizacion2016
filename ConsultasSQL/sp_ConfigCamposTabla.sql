USE [Digitalizacion2013]
GO
/****** Object:  StoredProcedure [dbo].[sp_ConfigCampos]    Script Date: 17/02/2016 09:56:37 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE [dbo].[sp_ConfigCamposTabla]
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
	Validar 02 = Devuelve un registro en base al IdUnico

	Validar 04 = Mantenimiento del Registro
  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --idUnico
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --idCampo
  Exec ObtenerValor 'V3', @Cabezero, '|', @Valor3 Output --Valor

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
     Select id = a.idUnico, a.Valor
    	From Config_Campos_Tablas a (NoLock) 
	 Where a.idCampo= @Valor2
     Select @Registro = @@RowCount
   End

If @Validar = 2 -- Devuelve un registro en base al idUnico
	Begin
		Select a.idUnico, a.idCampo, a.Valor,
				dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
				dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
				dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Config_Campos_Tablas a (NoLock)
		Where a.idUnico = @Valor1
		Select @Registro = @@RowCount
	End

If @Validar = 4  -- Mantenimiento del Registro
	Begin
		--Buscar si existe este Campo para realizar modificaciones
		If Exists(Select idUnico From Config_Campos_Tablas Where idUnico = @Valor1)  -- Ya existe el registro
			Begin
				Update Config_Campos_Tablas
					Set Valor = @Valor3,
						dFecha_UltimaModificacion = GetDate(),
						cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50),
						cUsuario_UltimaModificacion = @C1
					Where idUnico = @Valor1
				Select @Registro = @@RowCount
			Select a.idUnico, a.idCampo, a.Valor,
					dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
					dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
					dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Config_Campos_Tablas a (NoLock)
				Where idUnico = @Valor1
			End
		Else --No existe el Registro Generar uno nuevo
			Begin
				Insert Config_Campos_Tablas (idCampo, Valor,
										dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
				Values(@Valor2, @Valor3, GetDate(), Left((@C3 + '/' +@C2),50), @C1)
				Select @Registro = @@RowCount
				Select a.idUnico, a.idCampo, a.Valor,
						dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
						dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
						dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
						From Config_Campos_Tablas a (NoLock)
				Where a.idUnico = @@IDENTITY
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


