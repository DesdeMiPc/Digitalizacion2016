Select * From Datos_Documentos_Imagenes

Select * From Datos_Expedientes_Campos



Select * From Datos_Expedientes
Where idUnicoExpediente = 6
Select * From Datos_Expedientes_Campos
Where idDocumentoPadre = 6

Select * From Config_Formularios
Where idFormulario = 1
Select * From Config_Formularios_Campos
where idFormulario = 1

Select d.Descripcion as Formulario, d.CodeBar,a.idFormulario, a.idcampo, 
			   b.Descripcion, TipoCampo=e.Descripcion, Clasificacion = c.Descripcion, Explicacion=IsNull(b.Explicacion,''),
			   Valor = IsNull((Select aa.ValorCampo
							From  Datos_Expedientes_Campos aa (NoLock)
						Where aa.idDocumentoPadre = 6 And aa.idCampo = a.idCampo),'')
			From Config_Formularios_Campos a (NoLock)
				Inner Join Config_Campos b (NoLock) On a.idCampo = b.idCampo
				Inner Join Config_Campos_Clasificacion c (NoLock) On b.Clasificacion = c.idClasificacion
				Inner Join Config_Formularios d (NoLock) On a.idFormulario = d.idFormulario
				Inner Join CTL_TiposCampos e (NoLock) On b.TipoCampo = e.idTipoCampo
			Where a.idFormulario = 1

Delete From Datos_Expedientes_Campos
Where idDocumentoPadre = 6 And idCampo = 4
