USE [Digitalizacion2013]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerValor]    Script Date: 06/11/2015 02:26:01 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROC [dbo].[ObtenerValor]
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

