USE [master]
GO
/****** Object:  Database [DigitalJuridico]    Script Date: 26/11/2015 10:59:04 a.m. ******/
CREATE DATABASE [DigitalJuridico]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DigitalJuridico', FILENAME = N'C:\SQLData\Data\DigitalJuridico.mdf' , SIZE = 35840KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10240KB )
 LOG ON 
( NAME = N'DigitalJuridico_log', FILENAME = N'C:\SQLData\Data\DigitalJuridico_log.ldf' , SIZE = 32768KB , MAXSIZE = 2048GB , FILEGROWTH = 10240KB )
GO
ALTER DATABASE [DigitalJuridico] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DigitalJuridico].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DigitalJuridico] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DigitalJuridico] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DigitalJuridico] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DigitalJuridico] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DigitalJuridico] SET ARITHABORT OFF 
GO
ALTER DATABASE [DigitalJuridico] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DigitalJuridico] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DigitalJuridico] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DigitalJuridico] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DigitalJuridico] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DigitalJuridico] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DigitalJuridico] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DigitalJuridico] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DigitalJuridico] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DigitalJuridico] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DigitalJuridico] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DigitalJuridico] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DigitalJuridico] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DigitalJuridico] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DigitalJuridico] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DigitalJuridico] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DigitalJuridico] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DigitalJuridico] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DigitalJuridico] SET RECOVERY FULL 
GO
ALTER DATABASE [DigitalJuridico] SET  MULTI_USER 
GO
ALTER DATABASE [DigitalJuridico] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DigitalJuridico] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DigitalJuridico] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DigitalJuridico] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DigitalJuridico', N'ON'
GO
USE [DigitalJuridico]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerValor]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[ObtenerValor]
(
    @NombreCampo VARCHAR(50),
    @Cadena VARCHAR(MAX),
    @CaracterSeparador Varchar(2),
    @RegresaValorCampo Varchar(MAX) output
)
AS
BEGIN
    Set NoCount On

    --Declarar Variable de Trabajo
    Declare @Tamano        Int
    Declare @pos_campo     Int
    Declare @pos_separador Int

    --Asignar Datos
    SELECT @NombreCampo = UPPER(@NombreCampo)

    --Proceso para Extraer el Valor de la Cadena
    SELECT @Tamano = Len(@NombreCampo) + 1
    SELECT @Pos_Campo = CharIndex(@NombreCampo + "=",UPPER(@Cadena))

    If @Pos_Campo <= 0
      Begin
        SELECT @RegresaValorCampo = ""
      End
    Else
      Begin -- "|"
        Select @pos_separador = charindex(@CaracterSeparador,@Cadena,@pos_campo + 1)
        Select @RegresaValorCampo = substring(@Cadena, @pos_campo + @tamano, @pos_separador - (@pos_campo + @tamano))
      End

    Set NoCount Off
END


GO
/****** Object:  StoredProcedure [dbo].[sp_Arbol_General]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_Arbol_General]
(
  @cabezero Varchar(8000),
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

-- 0 Tipo Carpeta
-- 1 Tipo Expediente
-- 2 Tipo Documento Final

  --Variables de Trabajo
  Declare @Valor1    VarChar(8000)
  Declare @Valor2    VarChar(8000)
  Declare @Valor3    VarChar(8000)
  Declare @Valor4    VarChar(8000)
  Declare @Valor5    VarChar(8000)
  Declare @Valor6    VarChar(8000)
  
  Declare @Rol		 VarChar(8000)
      
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
    Validar 01 = Consulta en Base a NodoPadre 
    Validar 02 = Devuelve un Registro en Base al id del Nodo
    Validar 03 = Agrega un nuevo Nodo a la Tabla
    Validar 04 = Consulta en Base al Nodo Padre pero sin ser de Tipo 2 -- Documentos de Expedientes
	Validar 07 = Devuelve los Nodos en base al nodo padres para Archivo General

	Validar 100 = Devuelve los Expedientes para agregar en el Arbol General

  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --id de Nodo Padre
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --id del Nodo - o id de Nodo Template
  Exec ObtenerValor 'V3', @Cabezero, '|', @Valor3 Output --Descripcion
  Exec ObtenerValor 'V4', @Cabezero, '|', @Valor4 Output --Tipo de Nodo
  Exec ObtenerValor 'V5', @cabezero, '|', @Valor5 OutPut --id del Formulario
  Exec ObtenerValor 'V6', @cabezero, '|', @Valor6 OutPut --Activo
  
  --Datos de Registro General
  Exec ObtenerValor 'C1', @Cabezero, '|', @C1 Output --Usuario Firmado
  Exec ObtenerValor 'C2', @Cabezero, '|', @C2 Output --Ip de Equipo Remoto
  Exec ObtenerValor 'C3', @Cabezero, '|', @C3 Output --Nombre de Usuario Firmado en el Equipo/Equipo

 -- Validar parametros
 -- If @Validar = 1 or @Validar = 4 
 --  Begin
 --    If Len(LTrim(RTrim(@Valor1))) = 0
 --     Begin
 --       Select @Resul = '2R=ERROR|2M=Debe de Ingresar el Numero del Nodo Padre|'
 --       Return
 --     End
	--End

  -- Desarrollo de las Consultas
  If @Validar = 1 -- Consulta en Base a NodoPadre
   Begin
		Select a.idNodo, a.idPadre, a.Descripcion, a.Activo, a.Tipo, a.idFormulario,
			dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
			dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
			dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Arbol_General a (NoLock)
		Where a.idPadre = @Valor1
		Select @Registro = @@RowCount
   End

  If @Validar = 2 -- Devuelve un Registro en Base al id del Nodo
   Begin
		Select a.idNodo, a.idPadre, a.Descripcion, a.Activo, a.Tipo, a.idFormulario,
			dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
			dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
			dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Arbol_General a (NoLock)
		Where a.idNodo = @Valor2
		Select @Registro = @@RowCount
   End

  If @Validar = 3 -- Agrega un nodo nuevo a la tabla de Arbol General
	Begin
		--Buscar si existe en el nivel solicitado otro nodo de igual descrpción
		Select @Desc0 = Count(idNodo) From Arbol_General
			Where idPadre = @Valor1 And Descripcion = @Valor3
		if @Desc0 > 0 -- Ya existe un nodo en este nivel con la misma descripcion
			Begin
				Select @Resul = "2R=ERROR|2M=Ya existe un nodo con esta descripción en este nivel"
				Select @Registro = 0
				Return
			End
		Else
			--Proceder a Agregar el Nodo
			Begin
				Insert Arbol_General (idPadre, Descripcion, Tipo, idFormulario, Activo,
										dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
				Values (@Valor1,@Valor3,@Valor4,@Valor5,1,GetDate(), Left((@C3 + '/' +@C2),50), @C1)
				Select * From Arbol_General
					Where idNodo = @@IDENTITY
				Select @Registro = @@ROWCOUNT
			End
	End

  If @Validar = 4  -- Devuelve los Nodos en base al nodo padres pero que el Tipo no Sea 2
	Begin
     Select idNodo, 
			Descripcion, Tipo
    	From Arbol_General (NoLock)
			Where idPadre = @Valor1 and Tipo < 2
     Select @Registro = @@RowCount
	End

  If @Validar = 7  -- Devuelve los Nodos en base al nodo padres para Archivo General
	Begin
     Select idNodo, 
			Descripcion, Tipo
    	From Arbol_General (NoLock)
			Where idPadre = @Valor1
     Select @Registro = @@RowCount
	End

  If  @Validar = 100
	Begin
		Select idUnicoExpediente, idLlaveBusqueda
			From Datos_Expedientes a (NoLock)
		Where a.idPadre = @Valor1
		Select @Registro = @@RowCount
	End

  -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 1 or @Validar = 7
        Select @Resul = '2R=OK|2M=No Existen Nodos para este Padre Seleccionado|'
	 if @Validar = 2
		Select @Resul = '2R=ERROR|2M=No Existen el nodo solicitado|'
	 if @Validar = 100
		Select @Resul = '2R=OK|2M=No Existen Expedientes para este nodo|'
   End
  Else
   Begin
     If @Validar = 1 or @Validar = 2 or @Validar = 4 or @Validar = 7 or @Validar = 100
        Select @Resul = '2R=OK|'
	 If @Validar = 3
		Select @Resul = '2R=OK|2M=Nodo Agregado con exito'
   End
  
  Set NoCount Off
END


GO
/****** Object:  StoredProcedure [dbo].[sp_ClasificacionCampos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_ClasificacionCampos]
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



GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Campos_Clasificacion_old]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_Config_Campos_Clasificacion_old]
(
  @cabezero Varchar(8000),
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

  --Asignar Valores
  Select @Desc0  = ''
  Select @Desc1  = ''
  Select @Desc2  = ''
  Select @Resul  = ''
  Select @Sql    = ''
  Select @Resul2 = ''

  /*   -->  Indice  <--
    Validar 01 = Devuelve todos los Registros de la Tabla Config_Campos
  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --idClasificacion
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --Descripcion

  -- Validar parametros
  If @Validar = 2 
	Begin
		if LEN(LTRIM(Rtrim(@Valor2))) = 0 
			Begin
				Select @Resul = '2R=ERROR|2M=Debe de Proporcionar la Descripción|'
				Return
			End
	End
  -- Desarrollo de las Consultas

  If @Validar = 1 -- Devuelve Todos los Campos de la Tabla
   Begin
     Select [idClasificacion] = idClasificacion,  
			[Descripcion] = Descripcion
    	From Config_Campos_Clasificacion (NoLock)
     Select @Registro = @@RowCount
   End

  if @Validar = 2 -- Agregar o modificar un Registro
	Begin
		if @Valor1 = ''  -- Se agregara un nuevo Registro
			Begin
				Select @Valor1 = idClasificacion From Config_Campos_Clasificacion
								 Where Descripcion = @Valor2
				if @Valor1 <> '' --Ya existe un Registro con ese nombre de Clasificacion de Campos
					Begin
						Select @Resul = '2R=ERROR|2M=La Clasificación que intenta agregar ya existe en la Tabla|' + @Valor1
						Return
					End
				Else -- Se agregara correctamente el Campo en la Tabla de Campos
					Begin
						Insert Config_Campos_Clasificacion(Descripcion)
												Values (@Valor2)
						 Select [idClasificacion] = idclasificacion,  
								[Descripcion] = Descripcion
    						From Config_Campos_Clasificacion (NoLock)
    						Where idClasificacion = @@IDENTITY
    					Select @Registro = @@ROWCOUNT
					End
			End
		Else
			Begin
				Update Config_Campos_Clasificacion
					Set Descripcion = @Valor2
					Where idClasificacion = @Valor1
				Select [idClasificacion] = idclasificacion,  
						[Descripcion] = Descripcion
					From Config_Campos_clasificacion (NoLock)
					Where idClasificacion = @Valor1 
				Select @Registro = @@ROWCOUNT					
			End
	End

  -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 1
        Select @Resul = '2R=OK|2M=no Existen Registros Actualmente|'
     If @Validar = 2
        Select @Resul = '2R=ERROR|2M=Problemas al Actualizar el Registro o intentar dar de alta|'
   End

  Else
   Begin
     If @Validar = 1 or @Validar = 2
        Select @Resul = '2R=OK|2M=Operación Exitosa'
     If @Validar = 4 OR @Validar = 5
        Select @Resul = '2R=OK|2M=Operación Exitosa'
   End
  
  Set NoCount Off
END


GO
/****** Object:  StoredProcedure [dbo].[sp_ConfigCampos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_ConfigCampos]
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



GO
/****** Object:  StoredProcedure [dbo].[sp_ConfigFormularioCampos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_ConfigFormularioCampos]
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



GO
/****** Object:  StoredProcedure [dbo].[sp_ConfigFormularios]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_ConfigFormularios]
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
  Exec ObtenerValor 'V5', @Cabezero, '|', @Valor5 Output --CodeBar

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
	 Select Id = a.idFormulario, a.Descripcion, [Codigo de Barras] = Case When a.CodeBar = 1 Then 'Si' Else 'No' End
		From Config_Formularios a (NoLock)
	 Where a.TipoFormulario = @Valor4
     Select @Registro = @@RowCount
   End

If @Validar = 2 -- Devuelve un registro en base a su IdFormulario
	Begin
		Select idFormulario, Descripcion, Clasificacion, TipoFormulario, CodeBar,
				dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
				dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
				dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Config_Formularios (NoLock)
		Where idFormulario = @Valor1
	End

if @Validar = 3 -- Devuelve todos los campos configurados de este formulario
	Begin
		Select d.Descripcion as Formulario, d.CodeBar,a.idFormulario, a.idcampo, 
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
						CodeBar = @Valor5,
						dFecha_UltimaModificacion = GetDate(),
						cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50),
						cUsuario_UltimaModificacion = @C1
					Where idFormulario = @Valor1
				Select @Registro = @@ROWCOUNT
				Select idFormulario, Descripcion, Clasificacion, TipoFormulario, CodeBar,
						dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
						dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
						dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
					From Config_Formularios (NoLock)
					Where idFormulario = @Valor1
			End
		Else
			Begin
				Insert Config_Formularios (Descripcion, Clasificacion, TipoFormulario, CodeBar,
											dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
				Values(@Valor2, @Valor3, @Valor4, @Valor5, GetDate(), Left((@C3 + '/' +@C2),50), @C1)
				Select @Registro = @@RowCount
				Select idFormulario, Descripcion, Clasificacion, TipoFormulario, CodeBar,
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



GO
/****** Object:  StoredProcedure [dbo].[sp_Datos_Documentos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_Datos_Documentos]
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
  Declare @Valor7    VarChar(MAX)  
  Declare @Valor8    VarChar(8000)
  Declare @Valor9    VarChar(8000)

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
	Validar 01 = Devuelve el Registro en Base al idDocumentoUnico
    Validar 02 = Crear o Actualizar los Datos del Documento y Devolver el idUnico de Docmento
  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --id documento Unico
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --id Expediente Padre
  Exec ObtenerValor 'V3', @Cabezero, '|', @Valor3 Output --id de Formulario de Captura
  Exec ObtenerValor 'V4', @Cabezero, '|', @Valor4 Output --Llave de Busqueda Principal
  Exec ObtenerValor 'V5', @Cabezero, '|', @Valor5 Output --Orden a Usar
  Exec ObtenerValor 'V6', @Cabezero, '|', @Valor6 Output --Id del Padre dentro del Arbol General

  Exec ObtenerValor 'V7', @Cabezero, '|', @Valor7 Output --Imagen en Base64
  Exec ObtenerValor 'V8', @Cabezero, '|', @Valor8 Output --Tipo de Documento
  Exec ObtenerValor 'V9', @Cabezero, '|', @Valor9 Output --Id de Unica Imagen

  --Datos de Registro General
  Exec ObtenerValor 'C1', @Cabezero, '|', @C1 Output --Usuario Firmado
  Exec ObtenerValor 'C2', @Cabezero, '|', @C2 Output --Ip de Equipo Remoto
  Exec ObtenerValor 'C3', @Cabezero, '|', @C3 Output --Nombre de Usuario Firmado en el Equipo/Equipo
 
  -- Validar parametros
  If @Validar = 1 
   Begin
     If Len(LTrim(RTrim(@Valor1))) = 0
      Begin
        Select @Resul = '2R=ERROR|2M=Debe de Ingresar el Numero Unico de Documento|'
        Return
      End
	End
	
	
  -- Desarrollo de las Consultas

  if @Validar = 1 -- Devuelve el Registro en Base a su IDUnicoDocumento
	Begin
		Select idUnicoDocumento, idUnicoExpediente, idFormulario, idLlaveBusqueda, Orden, idPadre,
			dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
			dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
			dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Datos_Documentos (NoLock)
		Where idUnicoDocumento = @Valor1
		Select @Registro = @@RowCount
	End

  If @Validar = 2 -- Crear o Actualizar los Datos del Documento y Devolver el idUnico de Documento
	Begin
		--Determinar del Arbol General el Tipo de formulario en base al idPadre
		if @Valor3 = '0' or Len(@Valor3) = 0
			Begin
				Select @Valor3 = idFormulario From Arbol_General (NoLock) Where idNodo = @Valor6
			End
		-- Buscar si existe en este nivel otro Nodo con la Misma Descripcion
		Select @Desc0 = COUNT(idUnicoDocumento) From Datos_Documentos (NoLock)
			Where idUnicodocumento = @Valor1
		If @Desc0 > 0 -- Ya existe el Registro unico se procede a Actualizar los Datos
		  Begin
			--Proceso de Actualizacion y Devolver el Registro Actualizado
				Update Datos_Documentos
						Set idUnicoExpediente = @Valor2,
							idFormulario = @Valor3,
							idLlaveBusqueda = @Valor4,
							Orden = @Valor5,
							idPadre = @Valor6,
							dFecha_UltimaModificacion = GetDate(),
							cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50),
							cUsuario_UltimaModificacion = @C1
					Where idUnicoDocumento = @Valor1
					
				 Select idUnicoDocumento, idUnicoExpediente, idFormulario, idLlaveBusqueda, Orden, idPadre,
						dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
						dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
						dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
    				From Datos_Documentos (NoLock)
						Where idUnicoDocumento = @Valor1
				 Select @Registro = @@RowCount
		  End
		Else
			Begin
			--Proceso de Inserción de Datos y Devolver el Registro Generado
				Insert Datos_Documentos (idUnicoExpediente, idFormulario, idLlaveBusqueda, Orden, idPadre,
											dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
								Values(@Valor2,@Valor3,@Valor4,@Valor5,@Valor6,GetDate(), Left((@C3 + '/' +@C2),50), @C1)
										
				 Select idUnicoDocumento, idUnicoExpediente, idFormulario, idLlaveBusqueda, Orden, idPadre,
						dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
						dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
						dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
    				From Datos_Documentos (NoLock)
						Where idUnicoDocumento = @@IDENTITY
				 Select @Registro = @@RowCount
			End
	End

if @Validar = 3 -- Se Procede a guardar o Actualizar la imagen
	Begin
		if Exists(Select idUnicoDocumento From Datos_Documentos_Imagenes (NoLock) Where idUnicoDocumento = @Valor1 And idUnicaImagen = @Valor9)
			Begin
				--Solo se Actualiza la Imagen
				Update Datos_Documentos_Imagenes
					Set Imagen = @Valor7,
						Tipo = @Valor8
				Where idUnicoDocumento = @Valor1 And idUnicaImagen = @Valor9
				Select @Registro = @@RowCount
			End
		Else
			Begin
				--Se agrega el Registro nuevo de la Imagen
				Insert Into Datos_Documentos_Imagenes (idUnicoDocumento, Imagen, Tipo)
					Values ( @Valor1,  @Valor7, @Valor8)
				Select @Registro = @@RowCount
			End
	End

  If @Validar = 4 -- Devolver todas las Imagenes solicitadas
	Begin
		Select a.idUnicoDocumento, b.Imagen
			From Datos_Documentos a (NoLocK)
			Inner Join Datos_Documentos_Imagenes b (NoLock) On a.idUnicoDocumento = b.idUnicoDocumento
		Where a.idUnicoExpediente = @Valor2 and a.idPadre = @Valor6
		Select @Registro = @@RowCount
	End

   -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 1
        Select @Resul = '2R=OK|2M=No Existen Nodos para este Expediente Seleccionado|'
     If @Validar = 2
		Select @Resul = '2R=ERROR|2M=La Alta o Actualización de Datos tuvo Problemas|'
	 If @Validar = 3
		Select @Resul = '2R=ERROR|2M=La Alta o Actualización de la Imagen tuvo Problemas|'
	 If @Validar = 4
		Select @Resul = '2R=OK|2M=No Existen Imagenes solicitadas|'
   End

  Else
   Begin
     If @Validar = 1 OR @Validar = 2 or @Validar = 3 or @Validar = 4
        Select @Resul = '2R=OK|'
   End
  
  Set NoCount Off
END


GO
/****** Object:  StoredProcedure [dbo].[sp_Datos_Expedientes]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_Datos_Expedientes]
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
	Validar 01 = Devuelve el Registro en Base al idExpedienteUnico
    Validar 02 = Crear o Actualizar los Datos del Expediente y Devolver el idUnico de Expediente

	Validar 01 = Devuelve los Expediente de un Nodo Padre en Especifico
    
    Validar 03 = Devuelve un Registro en Base a su idUnicoDocumento e idLlaveBusqueda
    Validar 04 = Regresar los Tipos de Documentos que Contienen Datos para este Expediente
    Validar 05 = Devuelve los Datos de los campos con Valor de Datos_Expedientes_Campos
    Validar 06 = Eliminar el Expediete y Todos su Datos Relacionado
    Validar 07 = Proximamente
    Validar 08 = Modifica la llave de Busqueda o Identificador de Documento idLLaveBusqueda
    
    Parametro 99 = Usuario del sistema
   
    

  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --id Expediente Unico
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --id Nodo Padre
  Exec ObtenerValor 'V3', @Cabezero, '|', @Valor3 Output --id de Formulario de Captura
  Exec ObtenerValor 'V4', @Cabezero, '|', @Valor4 Output --Llave de Busqueda Principal
  
  --Datos de Registro General
  Exec ObtenerValor 'C1', @Cabezero, '|', @C1 Output --Usuario Firmado
  Exec ObtenerValor 'C2', @Cabezero, '|', @C2 Output --Ip de Equipo Remoto
  Exec ObtenerValor 'C3', @Cabezero, '|', @C3 Output --Nombre de Usuario Firmado en el Equipo/Equipo
 
  -- Validar parametros
  If @Validar = 1 
   Begin
     If Len(LTrim(RTrim(@Valor1))) = 0
      Begin
        Select @Resul = '2R=ERROR|2M=Debe de Ingresar el Numero Unico de Expediente|'
        Return
      End
	End
	
	
  -- Desarrollo de las Consultas

  if @Validar = 1 -- Devuelve el Registro en Base a su IDUnicoExpediente
	Begin
		Select idUnicoExpediente, idPadre, idFormulario, idLlaveBusqueda,
			dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
			dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
			dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
			From Datos_Expedientes
		Where idUnicoExpediente = @Valor1
		Select @Registro = @@RowCount
	End

  If @Validar = 2 -- Crear o Actualizar los Datos del Expediente y Devolver el idUnico de Expediente
	Begin
		-- Buscar si existe en este nivel otro Nodo con la Misma Descripcion
		Select @Desc0 = COUNT(idUnicoExpediente) From Datos_Expedientes
			Where idUnicoExpediente = @Valor1
		If @Desc0 > 0 -- Ya existe el Registro unico se procede a Actualizar los Datos
		  Begin
			--Proceso de Actualizacion y Devolver el Registro Actualizado
				Update Datos_Expedientes
						Set idPadre = @Valor2,
							idFormulario = @Valor3,
							idLlaveBusqueda = @Valor4,
							dFecha_UltimaModificacion = GetDate(),
							cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50),
							cUsuario_UltimaModificacion = @C1
					Where idUnicoExpediente = @Valor1
					
				 Select idUnicoExpediente, idPadre, idFormulario, idLlaveBusqueda,
						dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
						dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
						dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
    				From Datos_Expedientes (NoLock)
						Where idUnicoExpediente = @Valor1
				 Select @Registro = @@RowCount
		  End
		Else
			Begin
			--Proceso de Inserción de Datos y Devolver el Registro Generado
				Insert Datos_Expedientes (idPadre, idFormulario, idLlaveBusqueda,
											dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
								Values(@Valor2,@Valor3,@Valor4,GetDate(), Left((@C3 + '/' +@C2),50), @C1)
										
				 Select idUnicoExpediente, idPadre, idFormulario, idLlaveBusqueda,
						dFecha_Registro, cMaquina_Registro, cUsuario_Registro,
						dFecha_UltimaModificacion, cMaquina_UltimaModificacion, cUsuario_UltimaModificacion,
						dFecha_Eliminacion, cMaquina_Eliminacion, cUsuario_Eliminacion
    				From Datos_Expedientes (NoLock)
						Where idUnicoExpediente = @@IDENTITY
				 Select @Registro = @@RowCount
			End
	End

 -- If @Validar = 3 -- Devuelve un Registro en Base a su idPadre Unico y el Valor de idLLaveBusqueda
	--Begin
 --    Select idUnicoExpediente, idPadre, idFormulario, idLlaveBusqueda, DatosCampos
 --   	From Datos_Expedientes (NoLock)
	--		Where idPadre = @Valor1 and  idLlaveBusqueda = @Valor4
 --    Select @Registro = @@RowCount	
	--End
	
 -- If @Validar = 4 -- Regresar los Tipos de Documentos que Contienen Datos para este Expediente
	--Begin
	--	Select Distinct(a.idFormulario), b.Descripcion From Datos_Documentos a
	--			Inner Join Config_Formularios b On a.idFormulario = b.idFormulario
	--		Where idUnicoExpediente =  @Valor1
	--	Select @@ROWCOUNT
	--End
	
 -- If @Validar = 5 --Devuelve los Datos de los campos con Valor de Datos_Expedientes_Campos
	--Begin
	--	Select d.Descripcion as Formulario, a.idFormulario, a.idcampo, b.Descripcion, b.TipoCampo, Clasificación = c.Descripcion, Explicacion=IsNull(b.Explicacion,''),
	--			e.ValorCampo, e.idUnicoExpediente
	--		From Config_Formularios_Campos a
	--			Inner Join Config_Campos b On a.idCampo = b.idCampo
	--			Inner Join Config_Campos_Clasificacion c On b.Clasificacion = c.idClasificacion
	--			Inner Join Config_Formularios d On a.idFormulario = d.idFormulario
	--			Left Join Datos_Expedientes_Campos e On a.idCampo = e.idCampo
	--		Where a.idFormulario = @Valor3 and e.idUnicoExpediente = @Valor1
	--	Union
	--	Select d.Descripcion as Formulario, a.idFormulario, a.idcampo, b.Descripcion, b.TipoCampo, Clasificación = c.Descripcion, Explicacion=IsNull(b.Explicacion,''),
	--			e.ValorCampo, e.idUnicoExpediente
	--		From Config_Formularios_Campos a
	--			Inner Join Config_Campos b On a.idCampo = b.idCampo
	--			Inner Join Config_Campos_Clasificacion c On b.Clasificacion = c.idClasificacion
	--			Inner Join Config_Formularios d On a.idFormulario = d.idFormulario
	--			Left Join Datos_Expedientes_Campos e On a.idCampo = e.idCampo
	--		Where e.ValorCampo is null and e.idUnicoExpediente is null
	--	Select @Registro = @@ROWCOUNT
	--End

 -- If @Validar = 6 -- Eliminar el Expediente y todos los Datos Depedientes como es Documentos e Imagenes
	--Begin
	--	-- Eliminar los Registros de los Campos de Documentos Hijos
	--	Delete From Datos_Documentos_Campos
	--		Where idUnicoDocumento IN(
	--			Select a.idUnicoDocumento
	--				From Datos_Documentos_Campos a (Nolock)
	--					Inner Join Datos_Documentos b  (Nolock) On a.idUnicoDocumento = b.idUnicoDocumento
	--				Where b.idUnicoExpediente = @Valor1
	--		)
	--	Select @Registro = @@ROWCOUNT
	--	--Elimina las Imagenes Digitalizadas
	--	Delete From Datos_Documentos_Imagenes
	--		Where idUnicoDocumento IN(
	--			Select a.idUnicoDocumento
	--				From Datos_Documentos_Imagenes a (Nolock)
	--					Inner Join Datos_Documentos b (NoLock) On a.idUnicoDocumento = b.idUnicoDocumento
	--				Where b.idUnicoExpediente = @Valor1
	--		)
	--	Select @Registro = @Registro + @@ROWCOUNT
	--	--Eliminar del Maestro de Datos Documentos
	--	Delete From Datos_Documentos Where idUnicoExpediente = @Valor1
	--	Select @Registro = @Registro + @@ROWCOUNT
	--	--Elimina los Registros de Campos Relacionados con el Expediente
	--	Delete From Datos_Expedientes_Campos Where idUnicoExpediente = @Valor1
	--	Select @Registro = @Registro + @@ROWCOUNT
	--	--Por Ultimo Elimina El registro Maestro del Expediente
	--	Delete From Datos_Expedientes Where idUnicoExpediente = @Valor1
	--	Select @Registro = @Registro + @@ROWCOUNT
	--End
	
 -- if @Validar = 8 -- Cambiar el Valor de la Llave de busqueda
	--Begin
	--	Update Datos_Expedientes
	--		Set idLlaveBusqueda = @Valor4,
	--			id_User = @c01,
	--			Fecha_Mod = getdate(),
	--			ipModifico = @c02
	--	Where idUnicoExpediente =  @Valor1
	--	Select @Registro = @@ROWCOUNT
	--End	
	
 -- If @Validar = 9 -- Devuelve los Expediente por idLlaveBusqueda
 --  Begin
 --    Select idUnicoExpediente, idPadre, idFormulario, idLlaveBusqueda, DatosCampos
 --   	From Datos_Expedientes (NoLock)
	--		Where idLlaveBusqueda = @Valor4
 --    Select @Registro = @@RowCount
 --  End


   -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 1
        Select @Resul = '2R=OK|2M=No Existen Nodos para este Padre Seleccionado|'
     If @Validar = 2
		Select @Resul = '2R=ERROR|2M=La Alta o Actualización de Datos tuvo Problemas|'	 
	 If @Validar = 3
		Select @Resul = '2R=ERROR|2M=No el Registro Solicitado|'	 
	 If @Validar = 4
		Select @Resul = '2R=ERROR|2M=No Existen Documentos para el Expediente Seleccionado|'
	 if @Validar = 5
		Select @Resul = '2R=OK|2M=No Existen Valores para este Expediente|'
	 If @Validar = 6
		Select @Resul = '2R=OK|2M=Expediente inexistente o problemas de Datos|'
	 if @Validar = 8
		Select @Resul = '2R=OK|2M=No Existe en Expediente a Modificar|'
	 if @Validar = 9
		Select @Resul = '2R=OK|2M=No Existe el Expediente|'				 

   End

  Else
   Begin
     If @Validar = 1 OR @Validar = 2 or @Validar = 3 or @Validar = 4 OR @Validar = 5 or @Validar = 6 or @Validar = 8 or @Validar = 9
        Select @Resul = '2R=OK|'
   End
  
  Set NoCount Off
END


GO
/****** Object:  StoredProcedure [dbo].[sp_Datos_Expedientes_Campos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_Datos_Expedientes_Campos]
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
    Validar 01 = Devuelve el Registro Solicitado en Base a idDocumentoPadre y idCampo
	Validar 02 = Grabar el Dato del campo y Expediente
	Validar 03 = Devuelve todos los campos con sus valores
  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --idDocumentoPadre
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --idCampo
  Exec ObtenerValor 'V3', @Cabezero, '|', @Valor3 Output --Valor del Campo

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
		Select aa.*, valor = IsNull((Select ValorCampo From Datos_Expedientes_Campos aaa (NoLock) Where aaa.idCampo = aa.idCampo And aaa.idDocumentoPadre = @Valor1 ),'')
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
			Where a.idDocumentoPadre = @Valor1) aa
		Order By aa.idCampo
		Select @Registro = @@RowCount
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


GO
/****** Object:  StoredProcedure [dbo].[sp_Grupos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_Grupos]
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

If @Validar = 44 -- Devuelve los Registros que no Esten asignados a un Nodo en Especifico
	Begin
		Select * From Grupos c (NoLock)
			Where Not Exists
			(
				Select a.idRol, b.Descripcion 
					From Arbol_General_Rights a (NoLock)
						Inner Join Grupos b (NoLock) On a.idRol = b.id_Grupo
					Where a.idNodo = @Valor3 and c.id_Grupo = a.idrol 
			)    
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



GO
/****** Object:  StoredProcedure [dbo].[SP_Innova_MetaAyuda]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[SP_Innova_MetaAyuda]
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
  --Funciones
  ------------------------------------------------
  
  --Variables de Trabajo
  Declare @Valor1    VarChar(8000)
  Declare @Valor2    VarChar(8000)
  Declare @Valor3    VarChar(8000)

  Declare @Rol		 VarChar(8000)
      
  Declare @Registro  Int
  Declare @Desc0     VarChar(8000)
  Declare @Sql       VarChar(8000)

  --Asignar Valores
  Select @Desc0  = ''
  Select @Resul  = ''
  Select @Sql    = ''
  Select @Registro = 0

  --Parametros fijos
  Declare @c01 Varchar(8000)
  Declare @c02 Varchar(8000)
  
  Exec ObtenerValor 'C01', @cabezero, '|', @c01 OutPut --Usuario del sistema
  Exec ObtenerValor 'C02', @cabezero, '|', @c02 OutPut --Ip del equipo Cliente
  
  /*   -->  Indice  <--
    Validar 1 = Regresa Valores para llenado incial de Tabla
	Validar 2 = Regresa los Valores para llenado de ComboBox de Busquedas
  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1',  @Cabezero, '|', @Valor1  Output --Identificación de la Tabla
  Exec ObtenerValor 'V2',  @Cabezero, '|', @Valor2  Output --Campo Descripcion
  Exec ObtenerValor 'V3',  @Cabezero, '|', @Valor3  Output --Campo Campo

  -----------------------------------------------
  -- Desarrollo de las Consultas
  -----------------------------------------------
  Declare @nTerminal Int


  --Desarrollo de Consultas para las Ayudas de Este Catalogo
  If @Validar = 1 -- Regresa Valores para llenado incial de Tabla
	Begin
		Select Tabla, cProcedimiento, cValidar, cDescripcion
			From Innova_MetaAyuda a (NoLock)
		Where Tabla = @Valor1
		Select @Registro = @@ROWCOUNT
	End	

  If @Validar = 2 -- Regresa los Valores para llenado de ComboBox de Busquedas
	Begin
		Select cCampo, cDescripcion 
			From Innova_MetaAyuda_Campos a (NoLock)
		Where Tabla = @Valor1
		Select @Registro = @@ROWCOUNT
	End	

  If @Validar = 3 -- Registro para Realizar la Busqueda por Filtro
	Begin
		Select Tabla, cDescripcion, cCampo, cProcedimiento, cValidar, vValor
			From Innova_MetaAyuda_Campos a (NoLock)
		Where Tabla = @Valor1 And cDescripcion = @Valor2 And cCampo = @Valor3
		Select @Registro = @@ROWCOUNT
	End	
	  
  -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 1 Or @Validar = 2 Or @Validar = 3
        Select @Resul = '2R=ERROR|2M=No Existen Registros para esta consulta|'
   End
  Else
   Begin
     If @Validar = 1 Or @Validar = 2 Or @Validar = 3
        Select @Resul = '2R=OK|2M=Consulta Existosa con Registros Válidos|'
   End
  Set NoCount Off
END



GO
/****** Object:  StoredProcedure [dbo].[SP_Reportes]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[SP_Reportes]
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
  --Funciones
  ------------------------------------------------
  
  --Variables de Trabajo
  Declare @Valor1    VarChar(8000)
  Declare @Valor2    VarChar(8000)
  Declare @Valor3    VarChar(8000)
  Declare @Valor4    VarChar(8000)
  Declare @Valor5    VarChar(MAX)

  Declare @Valor11    VarChar(8000)
  Declare @Valor12    VarChar(8000)

  Declare @Valor102  VarChar(8000)

  Declare @Rol		 VarChar(8000)
      
  Declare @Registro  Int
  Declare @Desc0     VarChar(8000)
  Declare @newValor  VarChar(8000)
  Declare @Sql       VarChar(8000)

  --Asignar Valores
  Select @Desc0  = ''
  Select @Resul  = ''
  Select @Sql    = ''
  Select @Registro = 0

  --Parametros fijos
  Declare @c01 Varchar(8000)
  Declare @c02 Varchar(8000)
  
  Exec ObtenerValor 'C01', @cabezero, '|', @c01 OutPut --Usuario del sistema
  Exec ObtenerValor 'C02', @cabezero, '|', @c02 OutPut --Ip del equipo Cliente
  
  /*   -->  Indice  <--
	Validar   01 = Devuelve Registro de Reporte en base a IdReporte
	Validar   02 = Mantenimiento a Reportes
	Validar   03 = Listado de Reportes en base a idCategoria
	Validar   11 = Consulta de Todas las Categorias de Reportes
	Validar   12 = Mantenimiento de Categorias de Reportes
  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1',  @Cabezero, '|', @Valor1  Output --idReporte
  Exec ObtenerValor 'V2',  @Cabezero, '|', @Valor2  Output --cNombre
  Exec ObtenerValor 'V3',  @Cabezero, '|', @Valor3  Output --cDescripción
  Exec ObtenerValor 'V4',  @Cabezero, '|', @Valor4  Output --idCategoría
  Exec ObtenerValor 'V5',  @Cabezero, '|', @Valor5  Output --archivoRPX

  Exec ObtenerValor 'V11',  @Cabezero, '|', @Valor11  Output --Nombre de Categoria
  Exec ObtenerValor 'V12',  @Cabezero, '|', @Valor12  Output --Descripción de Categoria

  -----------------------------------------------
  -- Verificar Paramentros de Entrada
  -----------------------------------------------
  if @Validar = 1 -- Devuelve Registro de Reporte en base a IdReporte
	Begin
		if Len(RTrim(LTrim(@Valor1))) = 0
			Begin
				Select @Resul = '2R=ERROR|2M=Debe de Proporcionar un ID de Reporte|'
				Return
			End
	End

  if @Validar = 3 --Listado de Reportes en base a idCategoria
	Begin
		if Len(RTrim(LTrim(@Valor4))) = 0
			Begin
				Select @Resul = '2R=ERROR|2M=Debe de Proporcionar un ID de Categoría|'
				Return
			End
	End

  if @Validar = 12 -- Consulta por id de Departamento
    Begin
		if Len(RTrim(LTrim(@Valor11))) = 0 
			Begin
				Select @Resul = '2R=ERROR|2M=Debe de Proporcionar un nombre de Categoría|'
				Return
			End
	End
  -----------------------------------------------
  -- Desarrollo de las Consultas
  -----------------------------------------------
  If @Validar = 1 -- Devuelve Registro de Reporte en base a IdReporte
	Begin
		Select idReporte, cNombre, cDescripcion,
				archivoRPX, idCategoria
			From CTL_Reportes
		Where idReporte = @Valor1
		Select @Registro = @@ROWCOUNT
	End

  If @Validar = 2 -- Mantenimiento a Reportes
	Begin
		Select @Valor1 = IsNull(idReporte,'')
			From CTL_Reportes
			Where idReporte = @Valor1
		if @Valor1 = ''
			Begin
				-- Registro Nuevo
				Insert Into CTL_Reportes (cNombre, cDescripcion, idCategoria, archivoRPX)
					Values (@Valor2,@Valor3,@Valor4,@Valor5)
				Select @Registro = @@ROWCOUNT
				Select @Valor1 = @@IDENTITY
			End
		Else
			Begin
				-- Actualizar Registro
				Update CTL_Reportes
					Set cNombre = @Valor2,
						cDescripcion = @Valor3,
						idCategoria =@Valor4,
						archivoRPX = @Valor5
				Where idReporte = @Valor1
				Select @Registro = @@ROWCOUNT
			End
		Select idReporte, cNombre, cDescripcion, idCategoria, archivoRPX
			From CTL_Reportes
		Where idReporte = @Valor1
	End

  If @Validar = 3  -- Listado de Reportes en base a idCategoria	
	Begin
		Select idReporte, cNombre
			From CTL_Reportes
		Where idCategoria = @Valor4
		Select @Registro = @@ROWCOUNT
	End

  If @Validar = 11 -- Consulta de Todas las Categorias de Reportes
	Begin
		Select idCategoria, cNombre, cDescripcion
			From CTL_CategoriasReportes (NoLock)
		Select @Registro = @@ROWCOUNT
	End

  If @Validar = 12   --Mantenimiento de Categorias de Reportes
	Begin
		Select @Desc0 = IsNull(idCategoria,'')
			From CTL_CategoriasReportes
		Where cNombre = @Valor11
		if @Desc0 = ''
			Begin
				--No Existe la Categoria se Procede a Crear una nueva
				Insert Into CTL_CategoriasReportes (cNombre, cDescripcion)
					Values (@Valor11, @Valor12)
				Select @Registro = @@ROWCOUNT
				Select @newValor = @@IDENTITY
				Select idCategoria, cNombre, cDescripcion
					From CTL_CategoriasReportes (NoLock)
				Where idCategoria = @newValor
			End
		Else
			Begin
				Update CTL_CategoriasReportes
					Set cDescripcion = @Valor12,
						cNombre = @Valor11
				Where idCategoria = @Desc0
				Select @Registro = @@ROWCOUNT
				Select idCategoria, cNombre, cDescripcion
					From CTL_CategoriasReportes (NoLock)
				Where idCategoria = @Desc0
			End
	End

  -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 11
        Select @Resul = '2R=ERROR|2M=No Existen Registros para esta Operación|'
	 If @Validar = 2 Or @Validar = 12
		Select @Resul = '2R=ERROR|2M=Actualización o Alta no Realizada|'
	 If @Validar = 3
		Select @Resul = '2R=ERROR|2M=No Existen Reportes en esta Categoría|'
	 If @Validar = 1
		Select @Resul = '2R=ERROR|2M=No Existen El reporte Solicitado|'
   End
  Else
   Begin
     If @Validar = 2 Or @Validar = 11 or @Validar = 12 or @Validar = 3 or @Validar = 1
        Select @Resul = '2R=OK|2M=Operación Realizada con Exito|'
   End
  Set NoCount Off
END


GO
/****** Object:  StoredProcedure [dbo].[sp_TiposCampos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_TiposCampos]
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



GO
/****** Object:  StoredProcedure [dbo].[sp_Usuario]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_Usuario]
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


GO
/****** Object:  UserDefinedFunction [dbo].[GetPercentageOfTwoStringMatching]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetPercentageOfTwoStringMatching]
(
    @string1 NVARCHAR(100)
    ,@string2 NVARCHAR(100)
)
RETURNS INT
AS
BEGIN

    DECLARE @levenShteinNumber INT

    DECLARE @string1Length INT = LEN(@string1)
    , @string2Length INT = LEN(@string2)
    DECLARE @maxLengthNumber INT = CASE WHEN @string1Length > @string2Length THEN @string1Length ELSE @string2Length END

    SELECT @levenShteinNumber = [dbo].[LEVENSHTEIN] (   @string1  ,@string2)

    DECLARE @percentageOfBadCharacters INT = @levenShteinNumber * 100 / @maxLengthNumber

    DECLARE @percentageOfGoodCharacters INT = 100 - @percentageOfBadCharacters

    -- Return the result of the function
    RETURN @percentageOfGoodCharacters

END


GO
/****** Object:  UserDefinedFunction [dbo].[LEVENSHTEIN]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================     
-- Create date: 2011.12.14
-- Description: http://blog.sendreallybigfiles.com/2009/06/improved-t-sql-levenshtein-distance.html
-- =============================================

CREATE FUNCTION [dbo].[LEVENSHTEIN](@left  VARCHAR(100),
                                    @right VARCHAR(100))
returns INT
AS
  BEGIN
      DECLARE @difference    INT,
              @lenRight      INT,
              @lenLeft       INT,
              @leftIndex     INT,
              @rightIndex    INT,
              @left_char     CHAR(1),
              @right_char    CHAR(1),
              @compareLength INT

      SET @lenLeft = LEN(@left)
      SET @lenRight = LEN(@right)
      SET @difference = 0

      IF @lenLeft = 0
        BEGIN
            SET @difference = @lenRight

            GOTO done
        END

      IF @lenRight = 0
        BEGIN
            SET @difference = @lenLeft

            GOTO done
        END

      GOTO comparison

      COMPARISON:

      IF ( @lenLeft >= @lenRight )
        SET @compareLength = @lenLeft
      ELSE
        SET @compareLength = @lenRight

      SET @rightIndex = 1
      SET @leftIndex = 1

      WHILE @leftIndex <= @compareLength
        BEGIN
            SET @left_char = substring(@left, @leftIndex, 1)
            SET @right_char = substring(@right, @rightIndex, 1)

            IF @left_char <> @right_char
              BEGIN -- Would an insertion make them re-align?
                  IF( @left_char = substring(@right, @rightIndex + 1, 1) )
                    SET @rightIndex = @rightIndex + 1
                  -- Would an deletion make them re-align?
                  ELSE IF( substring(@left, @leftIndex + 1, 1) = @right_char )
                    SET @leftIndex = @leftIndex + 1

                  SET @difference = @difference + 1
              END

            SET @leftIndex = @leftIndex + 1
            SET @rightIndex = @rightIndex + 1
        END

      GOTO done

      DONE:

      RETURN @difference
  END 

GO
/****** Object:  Table [dbo].[Arbol_General]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Arbol_General](
	[idNodo] [bigint] IDENTITY(1,1) NOT NULL,
	[idPadre] [bigint] NULL,
	[Descripcion] [nvarchar](150) NOT NULL,
	[Tipo] [int] NOT NULL CONSTRAINT [DF_Arbol_General_Tipo]  DEFAULT ((0)),
	[idFormulario] [bigint] NOT NULL,
	[Activo] [bit] NULL,
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL,
 CONSTRAINT [PK_Arbol_General] PRIMARY KEY CLUSTERED 
(
	[idNodo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Config_Campos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Config_Campos](
	[idCampo] [bigint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
	[Explicacion] [nvarchar](200) NULL,
	[TipoCampo] [int] NOT NULL,
	[Clasificacion] [bigint] NOT NULL,
	[Longitud] [int] NULL CONSTRAINT [DF_Config_Campos_Longitud]  DEFAULT ((10)),
	[Campo_Valor] [nvarchar](100) NULL,
	[Campo_Mostrar] [nvarchar](100) NULL,
	[Consulta] [nvarchar](500) NULL,
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL,
 CONSTRAINT [PK_Config_Campos] PRIMARY KEY CLUSTERED 
(
	[idCampo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Config_Campos_Clasificacion]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Config_Campos_Clasificacion](
	[idClasificacion] [bigint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL,
 CONSTRAINT [PK_Config_Campos_Clasificacion] PRIMARY KEY CLUSTERED 
(
	[idClasificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Config_Formularios]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Config_Formularios](
	[idFormulario] [bigint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](150) NOT NULL,
	[Clasificacion] [bigint] NOT NULL CONSTRAINT [DF_Config_Formularios_Clasificacion]  DEFAULT ((0)),
	[TipoFormulario] [int] NOT NULL CONSTRAINT [DF_Config_Formularios_TipoFormulario]  DEFAULT ((-1)),
	[CodeBar] [tinyint] NOT NULL CONSTRAINT [DF_Config_Formularios_CodeBar]  DEFAULT ((0)),
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL,
 CONSTRAINT [PK_Config_Formularios] PRIMARY KEY CLUSTERED 
(
	[idFormulario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Config_Formularios_Campos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Config_Formularios_Campos](
	[idFormulario] [bigint] NOT NULL,
	[idCampo] [bigint] NOT NULL,
	[Obligatorio] [tinyint] NOT NULL CONSTRAINT [DF_Config_Formularios_Campos_Obligatorio]  DEFAULT ((0)),
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CTL_CategoriasReportes]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CTL_CategoriasReportes](
	[idCategoria] [bigint] IDENTITY(1,1) NOT NULL,
	[cNombre] [varchar](100) NOT NULL,
	[cDescripcion] [varchar](250) NULL,
 CONSTRAINT [PK_CTL_CategoriasReportes] PRIMARY KEY CLUSTERED 
(
	[idCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CTL_Reportes]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CTL_Reportes](
	[idReporte] [bigint] IDENTITY(1,1) NOT NULL,
	[cNombre] [varchar](250) NOT NULL,
	[cDescripcion] [varchar](1000) NULL,
	[archivoRPX] [varchar](max) NULL,
	[idCategoria] [bigint] NOT NULL,
 CONSTRAINT [PK_CTL_Reportes] PRIMARY KEY CLUSTERED 
(
	[idReporte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CTL_System_Images]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CTL_System_Images](
	[idImage] [int] IDENTITY(1,1) NOT NULL,
	[cDatosImagen] [varchar](max) NULL,
	[cTipo] [int] NULL,
 CONSTRAINT [PK_CTL_System_Images] PRIMARY KEY CLUSTERED 
(
	[idImage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CTL_TiposCampos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CTL_TiposCampos](
	[idTipoCampo] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](20) NOT NULL,
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL,
 CONSTRAINT [PK_CTL_TiposCampos] PRIMARY KEY CLUSTERED 
(
	[idTipoCampo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Datos_Documentos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Datos_Documentos](
	[idUnicoDocumento] [bigint] IDENTITY(1,1) NOT NULL,
	[idUnicoExpediente] [bigint] NOT NULL,
	[idPadre] [bigint] NOT NULL,
	[idFormulario] [bigint] NOT NULL,
	[idLlaveBusqueda] [nvarchar](max) NOT NULL,
	[Orden] [bigint] NOT NULL CONSTRAINT [DF_Datos_Documentos_Orden]  DEFAULT ((0)),
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL,
 CONSTRAINT [PK_Datos_Documentos] PRIMARY KEY CLUSTERED 
(
	[idUnicoDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Datos_Documentos_Campos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Datos_Documentos_Campos](
	[idDocumentoPadre] [bigint] NOT NULL,
	[idCampo] [bigint] NOT NULL,
	[ValorCampo] [nvarchar](max) NULL,
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL,
 CONSTRAINT [PK_Datos_Documentos_Campos] PRIMARY KEY CLUSTERED 
(
	[idDocumentoPadre] ASC,
	[idCampo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Datos_Documentos_Imagenes]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Datos_Documentos_Imagenes](
	[idUnicaImagen] [bigint] IDENTITY(1,1) NOT NULL,
	[idUnicoDocumento] [bigint] NOT NULL,
	[Imagen] [varchar](max) NOT NULL,
	[Tipo] [varchar](50) NULL,
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL,
	[HashCode] [varchar](50) NULL,
 CONSTRAINT [PK_Datos_Documentos_Imagenes] PRIMARY KEY CLUSTERED 
(
	[idUnicaImagen] ASC,
	[idUnicoDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Datos_Expedientes]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Datos_Expedientes](
	[idUnicoExpediente] [bigint] IDENTITY(1,1) NOT NULL,
	[idPadre] [bigint] NOT NULL,
	[idFormulario] [bigint] NOT NULL,
	[idLlaveBusqueda] [nvarchar](max) NULL,
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL,
 CONSTRAINT [PK_Datos_Expedientes] PRIMARY KEY CLUSTERED 
(
	[idUnicoExpediente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Datos_Expedientes_Campos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Datos_Expedientes_Campos](
	[idDocumentoPadre] [bigint] NOT NULL,
	[idCampo] [bigint] NOT NULL,
	[ValorCampo] [nvarchar](max) NULL,
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL,
 CONSTRAINT [PK_Datos_Expedientes_Campos] PRIMARY KEY CLUSTERED 
(
	[idDocumentoPadre] ASC,
	[idCampo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Grupos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Grupos](
	[id_Grupo] [bigint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](100) NULL,
	[bActivo] [bit] NOT NULL CONSTRAINT [DF_Grupos_bActivo]  DEFAULT ((1)),
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL,
 CONSTRAINT [PK_Grupos] PRIMARY KEY CLUSTERED 
(
	[id_Grupo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Innova_MetaAyuda]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Innova_MetaAyuda](
	[Tabla] [varchar](250) NOT NULL,
	[cDescripcion] [varchar](250) NULL,
	[cProcedimiento] [varchar](max) NULL,
	[cValidar] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Innova_MetaAyuda_Campos]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Innova_MetaAyuda_Campos](
	[Tabla] [varchar](250) NOT NULL,
	[cDescripcion] [varchar](250) NOT NULL,
	[cCampo] [varchar](250) NOT NULL,
	[cProcedimiento] [varchar](250) NULL,
	[cValidar] [varchar](50) NULL,
	[vValor] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 26/11/2015 10:59:04 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuarios](
	[idUnico] [bigint] IDENTITY(1,1) NOT NULL,
	[id_User] [nvarchar](20) NOT NULL,
	[NombreCompleto] [nvarchar](100) NULL,
	[Puesto] [nvarchar](100) NULL,
	[Password] [nvarchar](max) NULL,
	[Correo] [nvarchar](100) NULL,
	[id_Grupo] [bigint] NULL,
	[Activo] [bit] NULL,
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[idUnico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Index [IX_Datos_Documentos_Imagenes]    Script Date: 26/11/2015 10:59:04 a.m. ******/
CREATE NONCLUSTERED INDEX [IX_Datos_Documentos_Imagenes] ON [dbo].[Datos_Documentos_Imagenes]
(
	[idUnicoDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Datos_Documentos]  WITH CHECK ADD  CONSTRAINT [FK_Datos_Documentos_Config_Formularios] FOREIGN KEY([idFormulario])
REFERENCES [dbo].[Config_Formularios] ([idFormulario])
GO
ALTER TABLE [dbo].[Datos_Documentos] CHECK CONSTRAINT [FK_Datos_Documentos_Config_Formularios]
GO
ALTER TABLE [dbo].[Datos_Documentos]  WITH CHECK ADD  CONSTRAINT [FK_Datos_Documentos_Datos_Expedientes] FOREIGN KEY([idUnicoExpediente])
REFERENCES [dbo].[Datos_Expedientes] ([idUnicoExpediente])
GO
ALTER TABLE [dbo].[Datos_Documentos] CHECK CONSTRAINT [FK_Datos_Documentos_Datos_Expedientes]
GO
ALTER TABLE [dbo].[Datos_Documentos_Campos]  WITH CHECK ADD  CONSTRAINT [FK_Datos_Documentos_Campos_Config_Campos] FOREIGN KEY([idCampo])
REFERENCES [dbo].[Config_Campos] ([idCampo])
GO
ALTER TABLE [dbo].[Datos_Documentos_Campos] CHECK CONSTRAINT [FK_Datos_Documentos_Campos_Config_Campos]
GO
ALTER TABLE [dbo].[Datos_Documentos_Campos]  WITH CHECK ADD  CONSTRAINT [FK_Datos_Documentos_Campos_Datos_Documentos] FOREIGN KEY([idDocumentoPadre])
REFERENCES [dbo].[Datos_Documentos] ([idUnicoDocumento])
GO
ALTER TABLE [dbo].[Datos_Documentos_Campos] CHECK CONSTRAINT [FK_Datos_Documentos_Campos_Datos_Documentos]
GO
ALTER TABLE [dbo].[Datos_Expedientes]  WITH CHECK ADD  CONSTRAINT [FK_Datos_Expedientes_Arbol_General] FOREIGN KEY([idPadre])
REFERENCES [dbo].[Arbol_General] ([idNodo])
GO
ALTER TABLE [dbo].[Datos_Expedientes] CHECK CONSTRAINT [FK_Datos_Expedientes_Arbol_General]
GO
ALTER TABLE [dbo].[Datos_Expedientes]  WITH CHECK ADD  CONSTRAINT [FK_Datos_Expedientes_Config_Formularios] FOREIGN KEY([idFormulario])
REFERENCES [dbo].[Config_Formularios] ([idFormulario])
GO
ALTER TABLE [dbo].[Datos_Expedientes] CHECK CONSTRAINT [FK_Datos_Expedientes_Config_Formularios]
GO
ALTER TABLE [dbo].[Datos_Expedientes_Campos]  WITH CHECK ADD  CONSTRAINT [FK_Datos_Expedientes_Campos_Config_Campos] FOREIGN KEY([idCampo])
REFERENCES [dbo].[Config_Campos] ([idCampo])
GO
ALTER TABLE [dbo].[Datos_Expedientes_Campos] CHECK CONSTRAINT [FK_Datos_Expedientes_Campos_Config_Campos]
GO
ALTER TABLE [dbo].[Datos_Expedientes_Campos]  WITH CHECK ADD  CONSTRAINT [FK_Datos_Expedientes_Campos_Datos_Expedientes] FOREIGN KEY([idDocumentoPadre])
REFERENCES [dbo].[Datos_Expedientes] ([idUnicoExpediente])
GO
ALTER TABLE [dbo].[Datos_Expedientes_Campos] CHECK CONSTRAINT [FK_Datos_Expedientes_Campos_Datos_Expedientes]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Grupos] FOREIGN KEY([id_Grupo])
REFERENCES [dbo].[Grupos] ([id_Grupo])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Grupos]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Determina el Tipo dentro del Arbol, 1=Carpeta, 2=Expediente, 3=Documento' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Arbol_General', @level2type=N'COLUMN',@level2name=N'Tipo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Determina el Tipo de fomulario, 1=Expediente, 2=Documento' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Formularios', @level2type=N'COLUMN',@level2name=N'TipoFormulario'
GO
USE [master]
GO
ALTER DATABASE [DigitalJuridico] SET  READ_WRITE 
GO
