USE [Digitalizacion2013]
GO
/****** Object:  StoredProcedure [dbo].[sp_Datos_Expedientes]    Script Date: 06/11/2015 02:28:01 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[sp_Datos_Expedientes]
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

