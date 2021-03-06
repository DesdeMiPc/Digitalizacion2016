
/****** Object:  StoredProcedure [dbo].[sp_Arbol_General]    Script Date: 04/06/2016 09:38:01 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[sp_Arbol_General]
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
  Declare @Valor7    VarChar(8000)

  Declare @Logico1   VarChar(8000)
  Declare @Logico2   VarChar(8000)
  Declare @Logico3   VarChar(8000)
  Declare @Logico4   VarChar(8000)
  Declare @Logico5   VarChar(8000)
  Declare @Logico6   VarChar(8000)

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


  Select @Logico1 = '0'
  Select @Logico2 = '0'
  Select @Logico3 = '0'
  Select @Logico4 = '0'
  Select @Logico5 = '0'
  Select @Logico6 = '0'

  /*   -->  Indice  <--
    Validar 01 = Consulta en Base a NodoPadre 
    Validar 02 = Devuelve un Registro en Base al id del Nodo
    Validar 03 = Agrega un nuevo Nodo a la Tabla
    Validar 04 = Consulta en Base al Nodo Padre pero sin ser de Tipo 2 -- Documentos de Expedientes
	Validar 07 = Devuelve los Nodos en base al nodo padres para Archivo General

	Validar 100 = Devuelve los Expedientes para agregar en el Arbol General

	Validar 201 = Devuelve los Grupos Actuales del Nodo
	Validar 202 = Carga los Grupos que no tienen derechos al nodo actual
	Validar 203 = Guadar o Actualizar los Derechos del Arbol por Grupo
    Validar 204 = Obtener los derechos de un Grupo y un Nodo Especifico

  */

  --Obtener los Parametros
  Exec ObtenerValor 'V1', @Cabezero, '|', @Valor1 Output --id de Nodo Padre
  Exec ObtenerValor 'V2', @Cabezero, '|', @Valor2 Output --id del Nodo - o id de Nodo Template
  Exec ObtenerValor 'V3', @Cabezero, '|', @Valor3 Output --Descripcion
  Exec ObtenerValor 'V4', @Cabezero, '|', @Valor4 Output --Tipo de Nodo
  Exec ObtenerValor 'V5', @cabezero, '|', @Valor5 OutPut --id del Formulario
  Exec ObtenerValor 'V6', @cabezero, '|', @Valor6 OutPut --Activo
  
  Exec ObtenerValor 'V7', @cabezero, '|', @Valor7 OutPut --Id_Grupo

  --Valores Logicos para los Derechos del Arbol
  Exec ObtenerValor 'L1', @Cabezero, '|', @Logico1 Output --Valor de Lectura
  Exec ObtenerValor 'L2', @Cabezero, '|', @Logico2 Output --Valor de Imprimir
  Exec ObtenerValor 'L3', @Cabezero, '|', @Logico3 Output --Valor de Exportar
  Exec ObtenerValor 'L4', @Cabezero, '|', @Logico4 Output --Valor de Agregar
  Exec ObtenerValor 'L5', @Cabezero, '|', @Logico5 Output --Valor de Modificar
  Exec ObtenerValor 'L6', @Cabezero, '|', @Logico6 Output --Valor de Borrar

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
  
  If @Validar = 31 --Renombrar Nodo de Tipo 0
	Begin
		Update Arbol_General
			Set Descripcion = @Valor3,
			dFecha_UltimaModificacion = GetDate(),
			cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50),
			cUsuario_UltimaModificacion = @C1
		Where idNodo = @Valor2 and Tipo = 0
		Select @Registro = @@ROWCOUNT
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


--Seccion de Seguridad para Grupos y Derechos a Ejercer
 If @Validar = 201              --Cargar los Grupos Actuales del Nodo
	Begin
		Select idUnico, idNodo, a.id_Grupo, b.Descripcion
			From Arbol_General_Rights a (NoLock)
				Inner Join Grupos b (NoLock) On a.id_Grupo = b.id_Grupo
		Where idNodo = @Valor2
		Select @Registro = @@RowCount
	End

 if @Validar = 202              --Carga los Grupos que no tienen derechos al nodo actual
	Begin
		Select a.id_Grupo, a.Descripcion
			From Grupos a (NoLock)
		Where a.bActivo = 1
			And Not Exists (Select aa.id_Grupo From Arbol_General_Rights aa (NoLock) Where aa.id_Grupo = a.id_Grupo And  aa.idNodo = @Valor2)
		Select @Registro = @@RowCount
	End

 If @Validar = 203              --Guadar o Actualizar los Derechos del Arbol por Grupo
	Begin
		-- Proceso de Actualizacion de Derechos
		With Arbol AS
		(
			Select a.idNodo,a.idPadre 
				from arbol_general a (NoLock)
				Where idnodo = @Valor2
			Union All
			Select b.idNodo,b.idPadre
				From arbol_general b (NoLock)
				Inner Join Arbol c On c.idnodo = b.idpadre
		)	
		Update Arbol_General_Rights
			Set RightRead = @Logico1,
				RightPrint = @Logico2,
				RightExport = @Logico3,
				RightModify = @Logico4,
				RightAdd = @Logico5,
				RightDelete = @Logico6,
				dFecha_UltimaModificacion = GetDate(),
				cMaquina_UltimaModificacion = Left((@C3 + '/' +@C2),50),
				cUsuario_UltimaModificacion = @C1
			Where id_Grupo=@Valor7 AND Exists (Select Arbol.idNodo from Arbol Where Arbol.idNodo = Arbol_General_Rights.idnodo)
		Select @Registro = @@ROWCOUNT;
		Select @Desc1 = @Registro;

		-- Proceso de Agregar Registros Faltantes
		With Arbol2 AS
		(
			Select a.idNodo,a.idPadre 
				from arbol_general a (NoLock)
				Where idnodo = @Valor2
			Union All
			Select b.idNodo,b.idPadre
				From arbol_general b (NoLock)
				Inner Join Arbol2 c On c.idnodo = b.idpadre
		)
		Insert Into Arbol_General_Rights (idNodo,id_Grupo,RightRead, RightPrint, RightExport, RightModify, RightAdd, RightDelete, dFecha_Registro, cMaquina_Registro, cUsuario_Registro)
				(Select a.idnodo, @Valor7, @Logico1, @Logico2, @Logico3, @Logico4, @Logico5, @Logico6, GetDate(), Left((@C3 + '/' +@C2),50), @C1
						From Arbol2 a
						Where Not Exists (Select b.idNodo From Arbol_General_Rights b 
											Where b.id_Grupo=@Valor7 And b.idNodo = a.idNodo
										  )
				)
		Select @Desc2 = @@ROWCOUNT
		Select @Registro = @Registro + @Desc2
	End

  if @Validar = 204        --Obtener los derechos de un Grupo y un Nodo Especifico
	Begin
		Select a.*
			From Arbol_General_Rights a (NoLock)
		Where a.idNodo = @Valor2 And a.id_Grupo = @Valor7
		Select @Registro = @@ROWCOUNT;
	End

  -- Enviar Resultado
  If @Registro = 0
   Begin
     If @Validar = 1 or @Validar = 7
        Select @Resul = '2R=OK|2M=No Existen Nodos para este Padre Seleccionado|'
	 if @Validar = 2
		Select @Resul = '2R=ERROR|2M=No Existen el nodo solicitado|'
	 if @Validar = 3
		Select @Resul = '2R=ERROR|2M=No se pudo agregar el nodo solicitado|'
	 if @Validar = 31
		Select @Resul = '2R=ERROR|2M=No se pudo renombrar el nodo solicitado|'
	 if @Validar = 100
		Select @Resul = '2R=OK|2M=No Existen Expedientes para este nodo|'
	 If @Validar = 201 OR @Validar = 204
		Select @Resul = '2R=OK|2M=No Existen Grupos Asignados Actualmente a este Nodo|'
	 If @Validar = 202
		Select @Resul = '2R=ERROR|2M=No Existen Grupos Disponibles|'
	 If @Validar = 203
		Select @Resul = '2R=ERROR|2M=No Afecto ningun registro|'
   End
  Else
   Begin
     If @Validar = 1 or @Validar = 2 or @Validar = 31 or @Validar = 4 or @Validar = 7 or @Validar = 100
        Select @Resul = '2R=OK|'
	 If @Validar = 3
		Select @Resul = '2R=OK|2M=Nodo Agregado con exito'
	 If @Validar = 201 or @Validar = 202 or @Validar = 203 or @Validar = 204
		Select @Resul = '2R=OK|'
   End
  
  Set NoCount Off
END

