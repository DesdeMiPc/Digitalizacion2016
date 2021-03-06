USE [Digitalizacion2013]
GO
/****** Object:  StoredProcedure [dbo].[SP_Reportes]    Script Date: 06/11/2015 02:29:54 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[SP_Reportes]
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

