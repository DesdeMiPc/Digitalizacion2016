
/****** Object:  StoredProcedure [dbo].[sp_Grupos]    Script Date: 06/11/2015 02:28:37 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE [dbo].[sp_Grupos]
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
    Validar 01 = Devuelve Todos los Campos de la Tabla de un Registro
    Validar 02 = Crear o Actualizar los Datos del Grupo
    Validar 03 = Devuelve Todos los registros de la Tabla 
    Validar 04 = Devuelve los Registros que no Esten asignados a un Nodo en Especifico
  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --id_Grupo
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --Descripcion
  Exec ObtenerValor 'V3', @Cabezero, '|', @Valor3 Output --Activo

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
     Select Grupo = id_Grupo, Descripcion, Estatus = Case When bActivo = 1 Then 'Activo' Else 'Inactivo' End
    	From Grupos(NoLock)
     Select @Registro = @@RowCount
   End

If @Validar = 2 -- Devuelve el Registro para su Edición
	Begin
		Select id_Grupo, Descripcion, bActivo,
			dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
			dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
			dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Grupos
			Where id_Grupo = @Valor1 
		Select @Registro = @@RowCount
	End

if @Validar = 3 -- Mantenimiento del Registro
	Begin
		If Exists(Select id_Grupo From Grupos Where id_Grupo = @Valor1)
			Begin
				--Mantenimiento al Registro
				Update Grupos
					Set Descripcion = @Valor2,
						bActivo = @Valor3,
						dFecha_UltimaModificacion = GetDate(),
						cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50),
						cUsuario_UltimaModificacion = @C1
				Where id_Grupo = @Valor1
				Select @Registro = @@RowCount
				Select id_Grupo, Descripcion, bActivo,
					dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
					dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
					dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Grupos
				Where id_Grupo = @Valor1
			End
		Else
			Begin
				--Agregar Registro
				Insert Grupos (Descripcion, bActivo, dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
					Values (@Valor2, @Valor3,GetDate(), Left((@C3 + '/' +@C2),50), @C1)
				Select id_Grupo, Descripcion, bActivo,
					dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
					dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
					dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Grupos
				Where id_Grupo = @@IDENTITY
				Select @Registro = @@RowCount
			End
	End

if @Validar = 4  -- Desactivar el Registro solicitado en Base al Id Unico
	Begin
		Update Grupos
			Set bActivo = 0,
			dFecha_Eliminacion = GetDate(),
			cMaquina_Eliminacion = Left((@C3 + '/' +@C2),50),
			cUsuario_Eliminacion = @C1
		Where id_Grupo = @Valor1
		Select @Registro = @@RowCount
				Select id_Grupo, Descripcion, bActivo,
					dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
					dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
					dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Grupos
		Where id_Grupo = @Valor1
	End

  If @Validar = 22 -- Crear o Actualizar los Datos del Grupo y Devolver el id_Grupo
	Begin
		-- Buscar si existe ya este Grupo para realizar la modificacion
		if @Valor1 = '0' or @Valor1 = '' -- Se agregara un nuevo Registro
			Begin
				Select @Valor1 = IsNull(id_Grupo,'') From Grupos 
								 Where Descripcion = @Valor2
				if @@ROWCOUNT > 0 --Ya existe un Registro con ese nombre de Grupo
					Begin
						Select @Resul = '2R=ERROR|2M=El nombre de Grupo que intenta agregar ya existe en la Tabla|' + @Valor1
						Return
					End
				Else -- Se agregara correctamente el Campo en la Tabla de Grupos
					Begin
						Insert Grupos (Descripcion)
								Values (@Valor2)
						 Select [id_Grupo] = id_Grupo,  
								[Descripcion] = Descripcion
    						From Grupos (NoLock)
    						Where id_Grupo = @@IDENTITY
    					Select @Registro = @@ROWCOUNT
					End
			End
		Else
			Begin
				Update Grupos 
					Set Descripcion = @Valor2
					Where id_Grupo = @Valor1
					 Select [id_Grupo] = id_Grupo,  
							[Descripcion] = Descripcion
						From Grupos (NoLock)
						Where id_Grupo = @Valor1 
					Select @Registro = @@ROWCOUNT					
			End
	End

--If @Validar = 44 -- Devuelve los Registros que no Esten asignados a un Nodo en Especifico
--	Begin
--		Select * From Grupos c (NoLock)
--			Where Not Exists
--			(
--				Select a.idRol, b.Descripcion 
--					From Arbol_General_Rights a (NoLock)
--						Inner Join Grupos b (NoLock) On a.idRol = b.id_Grupo
--					Where a.idNodo = @Valor3 and c.id_Grupo = a.idrol 
--			)    
--	End
	
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


