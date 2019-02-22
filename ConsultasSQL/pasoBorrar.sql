Select a.idDocumentoPadre, campo = b.Descripcion, a.idCampo, a.ValorCampo,
			a.dFecha_Registro, a.cMaquina_Registro, a.cUsuario_Registro,
			a.dFecha_UltimaModificacion, a.cMaquina_UltimaModificacion, a.cUsuario_UltimaModificacion,
			a.dFecha_Eliminacion, a.cMaquina_Eliminacion, a.cUsuario_Eliminacion
		From Datos_Expedientes_Campos  a (NoLock)
			Join Config_Campos b (NoLock) On b.idCampo = a.idCampo


Select a.idFormulario, a.Descripcion, a.Clasificacion, a.TipoFormulario,
	   b.idCampo, c.Descripcion, c.Campo_Mostrar, c.Campo_Valor, c.Clasificacion, c.Explicacion, c.TipoCampo
	From Config_Formularios a (NoLock)
		Inner Join Config_Formularios_Campos b (NoLock) On a.idFormulario = b.idFormulario
		Inner Join Config_Campos c (NoLock) On b.idCampo = c.idCampo
Where a.idFormulario = 1

		Select a.idFormulario, a.idcampo, 
			   b.Descripcion, TipoCampo=e.Descripcion, Clasificacion = c.Descripcion, Explicacion=IsNull(b.Explicacion,''),
			   valor = IsNull((Select ValorCampo From Datos_Expedientes_Campos aa (NoLock) Where aa.idCampo = a.idCampo And aa.idDocumentoPadre = '10' ),'')
			From Config_Formularios_Campos a (NoLock)
				Inner Join Config_Campos b (NoLock) On a.idCampo = b.idCampo
				Inner Join Config_Campos_Clasificacion c (NoLock) On b.Clasificacion = c.idClasificacion
				Inner Join Config_Formularios d (NoLock) On a.idFormulario = d.idFormulario
				Inner Join CTL_TiposCampos e (NoLock) On b.TipoCampo = e.idTipoCampo
			Where a.idFormulario = (Select aa.idFormulario From Datos_Expedientes aa (NoLock) Where idUnicoExpediente = '10')

Select * From Config_Formularios_Campos
Where idFormulario = '20004'

Select a.*
	From Datos_Expedientes a (NoLock)
Where idDocumentoPadre = '10'
