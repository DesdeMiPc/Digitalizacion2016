USE [Digitalizacion2013]
GO
/****** Object:  StoredProcedure [dbo].[sp_TiposCampos]    Script Date: 06/11/2015 02:30:10 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE [dbo].[sp_TiposCampos]
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
    Validar 01 = Devuelve Todos los Campos de la Tabla de un Registro
    Validar 02 = Crear o Actualizar los Datos del Grupo
    Validar 03 = Devuelve Todos los registros de la Tabla 
    Validar 04 = Devuelve los Registros que no Esten asignados a un Nodo en Especifico
  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --IdTipoCampo
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --Descripcion

  --Datos de Registro General
  Exec ObtenerValor 'C1', @Cabezero, '|', @C1 Output --Usuario Firmado
  Exec ObtenerValor 'C2', @Cabezero, '|', @C2 Output --Ip de Equipo Remoto
  Exec ObtenerValor 'C3', @Cabezero, '|', @C3 Output --Nombre de Usuario Firmado en el Equipo/Equipo

  

  -- Validar parametros
  If @Validar = 2 or @Validar = 4
	Begin
		if LEN(LTRIM(Rtrim(@Valor1))) = 0 
			Begin
				Select @Resul = '2R=ERROR|2M=Debe de Proporcionar el Id del Grupo|'
				Return
			End
	End
	
-- Devuelve todos los campos de la tabla 
If @Validar = 1 -- Devuelve Todos los registros del la Tabla
   Begin
     Select Id = idTipoCampo, Descripcion
    	From CTL_TiposCampos (NoLock)
     Select @Registro = @@RowCount
   End

If @Validar = 2 -- Devuelve el Registro para su Edición
	Begin
		Select idTipoCampo, Descripcion,
			dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
			dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
			dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From CTL_TiposCampos
			Where idTipoCampo = @Valor1 
		Select @Registro = @@RowCount
	End

  -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 1
        Select @Resul = '2R=OK|2M=no Existen Registros Actualmente|'
     If @Validar = 2
		Select @Resul = '2R=ERROR|2M=No existe en Id Solicitado'
     If @Validar = 3
		Select @Resul = '2R=ERROR|No se puedo agregar el Registro'
	 If @Validar = 4
		Select @Resul = '2R=ERROR|2M=No se desactivo el registro|'
   End
  Else
   Begin
     If @Validar = 1 or @Validar = 3
        Select @Resul = '2R=OK|2M=Operación Exitosa'
	 If @Validar = 2 
		Select @Resul = '2R=OK|2M=Registro encontrado'
	 If @Validar = 4
		Select @Resul = '2R=OK|2M=Registro Desactivado|'
   End
  Set NoCount Off
END


