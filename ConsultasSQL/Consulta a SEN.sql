Select aa.*, valor = IsNull((Select ValorCampo From Datos_Expedientes_Campos aaa (NoLock) Where aaa.idCampo = aa.idCampo And aaa.idDocumentoPadre = 1 ),'')
		From
			(Select a.idFormulario, a.idcampo, 
					b.Descripcion, TipoCampo=e.Descripcion, Clasificacion = c.Descripcion, Explicacion=IsNull(b.Explicacion,'')
				From Config_Formularios_Campos a (NoLock)
					Inner Join Config_Campos b (NoLock) On a.idCampo = b.idCampo
					Inner Join Config_Campos_Clasificacion c (NoLock) On b.Clasificacion = c.idClasificacion
					Inner Join Config_Formularios d (NoLock) On a.idFormulario = d.idFormulario
					Inner Join CTL_TiposCampos e (NoLock) On b.TipoCampo = e.idTipoCampo
				Where a.idFormulario = (Select aa.idFormulario From Datos_Expedientes aa (NoLock) Where idUnicoExpediente = 1)
			Union
			Select b.idFormulario, a.idCampo, c.Descripcion, TipoCampo = e.Descripcion, Clasificacion = d.Descripcion, c.Explicacion
				From Datos_Expedientes_Campos a (NoLock)
					Inner Join Datos_Expedientes b (NoLock) On a.idDocumentoPadre = b.idUnicoExpediente
					Inner Join Config_Campos c (NoLock) On a.idCampo= c.idCampo
					Inner Join Config_Campos_Clasificacion d (NoLock) On c.Clasificacion = d.idClasificacion
					Inner Join CTL_TiposCampos e (NoLock) On c.TipoCampo = e.idTipoCampo
			Where a.idDocumentoPadre = 1
				And b.idFormulario = (Select aa.idFormulario From Datos_Documentos aa (NoLock) Where idUnicoDocumento = 1)) aa
		Order By aa.idCampo



Select * From Config_Formularios
Where CamposAutomaticos = 1
Select * From Datos_Expedientes

Select IsNull(b.CamposAutomaticos,0)
	From Datos_Expedientes a (NoLock)
		Inner Join Config_Formularios b (NoLock) On a.idFormulario = b.idFormulario
	Where a.idUnicoExpediente = 13

Select a.CamposAutomaticos, a.idFormulario
	From Config_Formularios a (NoLock)
	Where a.idFormulario = (
								Select a.idFormulario
									From Datos_Expedientes a (NoLock)
								Where idUnicoExpediente = 50004
							)


--idFormulario
--idCampo
--Descripcion
--TipoCampo
--Explicacion
--Valor

Select *
	From SEN..MetaEmpleados
Where nEmpleado = 10941

Declare @LlaveBusquedaI VarChar(8000)
Select @LlaveBusquedaI = 140
Declare @Parametros Varchar(800)
Set @Parametros = N'@LlaveBusqueda Varchar(8000)'

Declare @tSQL Varchar(8000);
Set @tSQL = N'
Create Table #campos (idFormulario bigint, idCampo int, Descripcion VarChar(200), TipoCampo Varchar(50), Clasificación VarChar(50), Explicacion VarChar(200), valor VarChar(8000));
Insert Into #campos (idFormulario, idCampo, Descripcion, TipoCampo, Clasificación, Explicacion, valor)
(Select 20010, 0, "01-Nombre", "Texto", "Datos Personales", "Campo del Sistema de Nominas", cEmpleado From SEN..MetaEmpleados Where nEmpleado = @LlaveBusqueda
Union
Select 20010, 0, "02-Fecha de Ingreso", "Fecha", "Datos Personales", "Campo del Sistema de Nominas", Convert(VarChar(200), dFechaIngreso, 105) From SEN..MetaEmpleados (NoLock) Where nEmpleado = @LlaveBusqueda
Union
Select 20010, 0, "03-RFC", "Texto", "Datos Personales", "Campo del Sistema de Nominas", cRFC From SEN..MetaEmpleados(NoLock)  Where nEmpleado = @LlaveBusqueda
Union
Select 20010, 0, "04-CURP", "Texto", "Datos Personales", "Campo del Sistema de Nominas", cCURP From SEN..MetaEmpleados (NoLock) Where nEmpleado = @LlaveBusqueda
Union
Select 20010, 0, "05-IMSS", "Texto", "Adscripción", "Campo del Sistema de Nominas", cFiliacionIMSS From SEN..MetaEmpleados (NoLock) Where nEmpleado = @LlaveBusqueda
Union
Select 20010, 0, "06-Departamento", "Texto", "Adscripción", "Campo del Sistema de Nominas", b.cDescripcion From SEN..MetaEmpleados a (NoLock) 
	Inner Join SEN..CTL_Departamentos b (NoLock) On a.nDepartamento = b.nDepartamento
		Where a.nEmpleado = @LlaveBusqueda
);
Select * From #campos;
Drop Table #campos;';
Select @tSQL

Execute sp_executesql @tSQL, @Parametros, @LlaveBusquedaI


Declare @LlaveBusqueda VarChar(8000)
Select @LlaveBusqueda = 140

DECLARE @sql nvarchar(MAX),
	@paramDefinition nvarchar(MAX),
	@paramValue varchar(8000)

SET @paramDefinition = '@nEmpleado char(8000)' 
SET @paramValue = @LlaveBusqueda
SET @sql = Replace('
Create Table #campos (idFormulario bigint, idCampo int, Descripcion VarChar(200), TipoCampo Varchar(50), Clasificación VarChar(50), Explicacion VarChar(200), valor VarChar(8000), bAutomatico int);
Insert Into #campos (idFormulario, idCampo, Descripcion, TipoCampo, Clasificaciónn, Explicacion, valor, bAutomatico)
(Select 20010, 0, "01-Nombre", "Texto", "Datos Personales", "Campo del Sistema de Nominas", cEmpleado, 1 From SEN..MetaEmpleados Where nEmpleado = @nEmpleado
Union
Select 20010, 0, "02-Fecha de Ingreso", "Fecha", "Datos Personales", "Campo del Sistema de Nominas", Convert(VarChar(200), dFechaIngreso, 105), 1 From SEN..MetaEmpleados (NoLock) Where nEmpleado = @nEmpleado
Union
Select 20010, 0, "03-RFC", "Texto", "Datos Personales", "Campo del Sistema de Nominas", cRFC, 1 From SEN..MetaEmpleados(NoLock)  Where nEmpleado = @nEmpleado
Union
Select 20010, 0, "04-CURP", "Texto", "Datos Personales", "Campo del Sistema de Nominas", cCURP, 1 From SEN..MetaEmpleados (NoLock) Where nEmpleado = @nEmpleado
Union
Select 20010, 0, "05-IMSS", "Texto", "Adscripción", "Campo del Sistema de Nominas", cFiliacionIMSS, 1 From SEN..MetaEmpleados (NoLock) Where nEmpleado = @nEmpleado
Union
Select 20010, 0, "06-Departamento", "Texto", "Adscripción", "Campo del Sistema de Nominas", b.cDescripcion, 1 From SEN..MetaEmpleados a (NoLock) 
	Inner Join SEN..CTL_Departamentos b (NoLock) On a.nDepartamento = b.nDepartamento
		Where a.nEmpleado = @nEmpleado
)
Select * From #campos
Drop Table #campos','"','''')

Select LEN(@sql)

EXEC sp_executesql @sql, @paramDefinition, @paramValue

 




