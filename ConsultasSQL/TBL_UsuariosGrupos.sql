

/****** Object:  Table [dbo].[UsuariosGrupos]    Script Date: 04/06/2016 12:18:41 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[UsuariosGrupos](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[idUsuario] [bigint] NOT NULL,
	[idGrupo] [bigint] NOT NULL,
	[dFecha_Registro] [datetime] NULL,
	[cMaquina_Registro] [varchar](50) NULL,
	[cUsuario_Registro] [varchar](50) NULL,
	[dFecha_UltimaModificacion] [datetime] NULL,
	[cMaquina_UltimaModificacion] [varchar](50) NULL,
	[cUsuario_UltimaModificacion] [varchar](50) NULL,
	[dFecha_Eliminacion] [datetime] NULL,
	[cMaquina_Eliminacion] [varchar](50) NULL,
	[cUsuario_Eliminacion] [varchar](50) NULL,
 CONSTRAINT [PK_UsuariosGrupos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


