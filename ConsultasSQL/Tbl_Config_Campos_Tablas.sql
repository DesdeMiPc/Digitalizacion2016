USE [Digitalizacion2013]
GO

/****** Object:  Table [dbo].[Config_Campos_Tablas]    Script Date: 17/02/2016 12:34:33 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Config_Campos_Tablas](
	[idUnico] [bigint] IDENTITY(1,1) NOT NULL,
	[idCampo] [bigint] NOT NULL,
	[Valor] [nvarchar](100) NOT NULL,
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


