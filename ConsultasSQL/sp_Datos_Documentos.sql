USE [Digitalizacion2013]
GO
/****** Object:  StoredProcedure [dbo].[sp_Datos_Documentos]    Script Date: 17/06/2016 10:09:43 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[sp_Datos_Documentos]
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
		
		--Determinar el Siguiente Numero de Orden si llega en CERO
		if @Valor5 = '0' Or LEN(@Valor5) = 0
			Begin
				Select @Valor5 = IsNull(MAX(orden),0) + 1 From Datos_Documentos (NoLock) 
					   Where idUnicoExpediente = @Valor2
					     And idPadre = @Valor6
						 And idFormulario = @Valor3
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
						Tipo = @Valor8,
						Orden = @Valor5,
						dFecha_UltimaModificacion = GetDate(), 
						cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50), 
						cUsuario_UltimaModificacion = @C1
				Where idUnicoDocumento = @Valor1 And idUnicaImagen = @Valor9
				Select @Registro = @@RowCount
			End
		Else
			Begin
				--Se agrega el Registro nuevo de la Imagen
				Insert Into Datos_Documentos_Imagenes (idUnicoDocumento, Imagen, Tipo, Orden,
													dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
					Values ( @Valor1,  @Valor7, @Valor8, @Valor5, GetDate(), Left((@C3 + '/' +@C2),50), @C1)
				Select @Registro = @@RowCount
			End
	End

  If @Validar = 4 -- Devolver todas las Imagenes solicitadas
	Begin
		Select b.idUnicaImagen, a.idUnicoDocumento, b.Imagen
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

