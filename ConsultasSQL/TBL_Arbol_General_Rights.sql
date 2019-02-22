
/****** Object:  Table [dbo].[Arbol_General_Rights]    Script Date: 12/04/2016 01:53:11 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Arbol_General_Rights](
	[idUnico] [bigint] IDENTITY(1,1) NOT NULL,
	[idNodo] [bigint] NOT NULL,
	[id_Grupo] [bigint] NOT NULL,
	[RightRead] [bit] NULL CONSTRAINT [DF_Arbol_General_Rights_RightRead]  DEFAULT ((0)),
	[RightPrint] [bit] NULL CONSTRAINT [DF_Arbol_General_Rights_RightPrint]  DEFAULT ((0)),
	[RightExport] [bit] NULL CONSTRAINT [DF_Arbol_General_Rights_RightExport]  DEFAULT ((0)),
	[RightAdd] [bit] NULL CONSTRAINT [DF_Arbol_General_Rights_RightWrite]  DEFAULT ((0)),
	[RightModify] [bit] NULL CONSTRAINT [DF_Arbol_General_Rights_RightModify]  DEFAULT ((0)),
	[RightDelete] [bit] NULL CONSTRAINT [DF_Arbol_General_Rights_RightDelete]  DEFAULT ((0)),
 CONSTRAINT [PK_Arbol_General_Rights] PRIMARY KEY CLUSTERED 
(
	[idUnico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


