USE [Digitalizacion2013]
GO
/****** Object:  StoredProcedure [dbo].[SP_Innova_MetaAyuda]    Script Date: 06/11/2015 02:28:53 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[SP_Innova_MetaAyuda]
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


