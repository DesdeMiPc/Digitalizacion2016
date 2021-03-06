USE [Digitalizacion2013]
GO
/****** Object:  StoredProcedure [dbo].[sp_Usuario]    Script Date: 06/11/2015 02:30:20 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE [dbo].[sp_Usuario]
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
    Validar 01 = Devuelve todos los Registros de la Usuarios para la Consulta GRID
	Validar 02 = Devuelve un registro es Especifico para su Mantenimiento en base al idUnico
	Validar 03 = Devuelve un registro es Especifico para su Mantenimiento en base al id_User
	Validar 04 = Mantenimiento del Registro
	Validar 05 = Desactivar el Registro solicitado en Base al Id Unico
  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --id_User
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --NombreCompleto
  Exec ObtenerValor 'V3', @Cabezero, '|', @Valor3 Output --Puesto
  Exec ObtenerValor 'V4', @Cabezero, '|', @Valor4 Output --Password
  Exec ObtenerValor 'V5', @Cabezero, '|', @Valor5 Output --Correo
  Exec ObtenerValor 'V6', @Cabezero, '|', @Valor6 Output --id_Grupo
  Exec ObtenerValor 'V7', @Cabezero, '|', @Valor7 Output --Activo
  Exec ObtenerValor 'V8', @Cabezero, '|', @Valor8 Output --idUnico
  
  --Datos de Registro General
  Exec ObtenerValor 'C1', @Cabezero, '|', @C1 Output --Usuario Firmado
  Exec ObtenerValor 'C2', @Cabezero, '|', @C2 Output --Ip de Equipo Remoto
  Exec ObtenerValor 'C3', @Cabezero, '|', @C3 Output --Nombre de Usuario Firmado en el Equipo/Equipo

  -- Validar parametros
 -- If @Validar = 2 
	--Begin
	--	if LEN(Ltrim(Rtrim(@Valor1))) = 0 or LEN(LTRIM(Rtrim(@Valor2))) = 0 or LEN(LTRIM(Rtrim(@Valor4))) = 0 or LEN(LTRIM(Rtrim(@Valor6))) = 0
	--		Begin
	--			Select @Resul = '2R=ERROR|2M=Debe de Proporcionar el Usuario, Nombre y Password|'
	--			Return
	--		End
	--End


If @Validar = 1 -- Devuelve Todos los Campos de la Tabla de un usuario
   Begin
     Select id = a.idUnico, Usuario = a.id_User, Descripcion = a.NombreCompleto, a.Puesto, Email = a.Correo, Grupo = b.Descripcion, Estatus = Case When a.Activo = 1 Then 'Activo' Else 'Inactivo' End
    	From Usuarios a (NoLock)
		Inner Join Grupos b (NoLock) On a.id_Grupo = b.id_Grupo
    	--Where id_User = @Valor1 
     Select @Registro = @@RowCount
   End

if @Validar = 2 -- Devuelve un registro en especifico en Base al idUnico
	Begin
		Select idUnico, id_User, NombreCompleto, Puesto, [Password], Correo, id_Grupo, Activo,
			dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
			dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
			dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Usuarios
			Where idUnico = @Valor8
		Select @Registro = @@RowCount
	End

if @Validar = 22 -- Devuelve un registro en especifico en Base al id_User
	Begin
		Select idUnico, id_User, NombreCompleto, Puesto, [Password], Correo, id_Grupo, Activo,
			dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
			dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
			dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Usuarios
			Where id_User = @Valor1
		Select @Registro = @@RowCount
	End

if @Validar = 3 -- Devuelve un registro en especifico en Base al id_User
	Begin
		Select idUnico, id_User, NombreCompleto, Puesto, [Password], Correo, id_Grupo, Activo,
			dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
			dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
			dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Usuarios
			Where idUnico = @Valor1
		Select @Registro = @@RowCount
	End

If @Validar = 4  -- Mantenimiento del Registro
	Begin
		--Buscar si existe este usuario para realizar modificaciones
		If Exists(Select id_User From Usuarios Where idUnico = @Valor8)  -- Ya existe el registro
			Begin
				Update Usuarios
					Set NombreCompleto = @Valor2,
						Puesto = @Valor3,
						[Password] = @Valor4,
						id_Grupo = @Valor6,
						Activo = @Valor7,
						dFecha_UltimaModificacion = GetDate(),
						cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50),
						cUsuario_UltimaModificacion = @C1
				Where idUnico = @Valor8
				Select idUnico, id_User, NombreCompleto, Puesto, [Password], Correo, id_Grupo, Activo,
					dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
					dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
					dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Usuarios
				Where idUnico = @Valor8
				Select @Registro = @@RowCount
			End
		Else --No existe el Registro Generar uno nuevo
			Begin
				Insert Usuarios (id_User, NombreCompleto, Puesto, [Password], Correo, id_Grupo, Activo, dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
					Values(@Valor1, @Valor2, @Valor3, @Valor4, @Valor5, @Valor6,1,GetDate(), Left((@C3 + '/' +@C2),50), @C1)
				Select idUnico, id_User, NombreCompleto, Puesto, [Password], Correo, id_Grupo, Activo,
					dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
					dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
					dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Usuarios
				Where idUnico = @@IDENTITY
				Select @Registro = @@RowCount
			End
	End

if @Validar = 5  -- Desactivar el Registro solicitado en Base al Id Unico
	Begin
		Update Usuarios
			Set Activo = 0,
			dFecha_Eliminacion = GetDate(),
			cMaquina_Eliminacion = Left((@C3 + '/' +@C2),50),
			cUsuario_Eliminacion = @C1
		Where idUnico = @Valor8
		Select @Registro = @@RowCount
		Select idUnico, id_User, NombreCompleto, Puesto, [Password], Correo, id_Grupo, Activo,
			dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
			dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
			dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Usuarios
		Where idUnico = @Valor8
	End

  -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 1
        Select @Resul = '2R=OK|2M=No Existen Registros Actualmente|'
	 If @Validar = 2 or @Validar = 3 or @Validar = 22
		Select @Resul = '2R=ERROR|2M=No Existen Registros Actualmente|'
	 If @Validar = 4
		Select @Resul = '2R=ERROR|2M=Problemas al Guardar el Registro|'
	 If @Validar = 5
		Select @Resul = '2R=ERROR|2M=No se desactivo el registro|'
   End

  Else
   Begin
     If @Validar = 1 or @Validar = 2 or @Validar = 3 or @Validar = 22
        Select @Resul = '2R=OK|2M=Consulta exitosa|'
	 If @Validar = 4
		Select @Resul = '2R=OK|2M=Registro Guardado de manera Correcta|'
	 If @Validar = 5
		Select @Resul = '2R=OK|2M=Registro Desactivado|'
   End
  
  Set NoCount Off
END

