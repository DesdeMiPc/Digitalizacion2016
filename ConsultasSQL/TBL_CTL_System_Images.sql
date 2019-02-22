USE [Digitalizacion2013]
GO

/****** Object:  Table [dbo].[CTL_System_Images]    Script Date: 01/03/2016 03:32:14 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CTL_System_Images](
	[idImage] [int] IDENTITY(1,1) NOT NULL,
	[cDescripcion] [varchar](50) NULL,
	[cGrupo] [varchar](50) NULL,
	[cDatosImagen] [varchar](max) NULL,
	[cTipo] [int] NULL,
 CONSTRAINT [PK_CTL_System_Images] PRIMARY KEY CLUSTERED 
(
	[idImage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


