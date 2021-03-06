USE [Digitalizacion2013]
GO
/****** Object:  StoredProcedure [dbo].[sp_ConfigFormularioCampos]    Script Date: 06/11/2015 02:27:13 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE [dbo].[sp_ConfigFormularioCampos]
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
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --idCampo
  Exec ObtenerValor 'V3', @Cabezero, '|', @Valor3 Output --Obligatorio?

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
If @Validar = 1 -- Devuelve Todos los Campos de un Formulario
   Begin
	 Select idFormulario, idCampo, Obligatorio
		From Config_Formularios_Campos a (NoLock)
	 Where a.idFormulario = @Valor1
     Select @Registro = @@RowCount
   End

If @Validar = 2 -- Devuelve un registro en base a su IdFormulario y IdCampo
	Begin
		select idFormulario, idCampo, Obligatorio,
				dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
				dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
				dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Config_Formularios_Campos (NoLock)
		Where idFormulario = @Valor1 and idCampo = @Valor2
		Select @Registro = @@RowCount
	End

If @Validar = 4 -- Guardar el registro 
	Begin
		If Exists(Select idFormulario From Config_Formularios_Campos Where idFormulario = @Valor1 And idCampo = @Valor2)
			Begin
				--Actualizar datos del registro
				Update Config_Formularios_Campos
						Set Obligatorio = @Valor3,
						dFecha_UltimaModificacion = GetDate(),
						cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50),
						cUsuario_UltimaModificacion = @C1
				Where idFormulario = @Valor1 And idCampo = @Valor2
				Select @Registro = @@RowCount
				Select idFormulario, idCampo, Obligatorio,
						dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
						dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
						dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Config_Formularios_Campos (NoLock)
					Where idFormulario = @Valor1 and idCampo = @Valor2
			End
		Else
			Begin
				--Agregar el Registro
				Insert Config_Formularios_Campos (idFormulario,idCampo,Obligatorio,
													dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
			    Values (@Valor1, @Valor2, @Valor3, GetDate(), Left((@C3 + '/' +@C2),50), @C1)
				Select @Registro = @@RowCount
				Select idFormulario, idCampo, Obligatorio,
						dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
						dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
						dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Config_Formularios_Campos (NoLock)
					Where idFormulario = @Valor1 and idCampo = @Valor2
			End
	End

  -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 1 
        Select @Resul = '2R=OK|2M=no Existen Registros Actualmente|'
	If @Validar = 2
		Select @Resul = '2R=ERROR|2M=Registro solicitado no existe|'
	If @Validar = 4
		Select @Resul = '2R=ERROR|2M=Problemas al actualizar o agregar el registro|'
   End
  Else
   Begin
     If @Validar = 1 or @Validar = 2 or @Validar = 4
        Select @Resul = '2R=OK|2M=Operación Exitosa'
   End
  Set NoCount Off
END


