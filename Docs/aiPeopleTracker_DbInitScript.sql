/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.2014)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2005
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [aiPeopleTracker]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LayoutTemplateCameraLink_LayoutTemplates]') AND parent_object_id = OBJECT_ID(N'[dbo].[LayoutTemplateCameraLink]'))
ALTER TABLE [dbo].[LayoutTemplateCameraLink] DROP CONSTRAINT [FK_LayoutTemplateCameraLink_LayoutTemplates]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LayoutTemplateCameraLink_Cameras]') AND parent_object_id = OBJECT_ID(N'[dbo].[LayoutTemplateCameraLink]'))
ALTER TABLE [dbo].[LayoutTemplateCameraLink] DROP CONSTRAINT [FK_LayoutTemplateCameraLink_Cameras]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CameraSettings_Cameras]') AND parent_object_id = OBJECT_ID(N'[dbo].[CameraSettings]'))
ALTER TABLE [dbo].[CameraSettings] DROP CONSTRAINT [FK_CameraSettings_Cameras]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Cameras_CameraStates]') AND parent_object_id = OBJECT_ID(N'[dbo].[Cameras]'))
ALTER TABLE [dbo].[Cameras] DROP CONSTRAINT [FK_Cameras_CameraStates]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_LayoutTemplatesUsage_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LayoutTemplatesUsage] DROP CONSTRAINT [DF_LayoutTemplatesUsage_CreateDate]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_LayoutTemplates_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LayoutTemplates] DROP CONSTRAINT [DF_LayoutTemplates_CreateDate]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_LayoutTemplates_IsActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LayoutTemplates] DROP CONSTRAINT [DF_LayoutTemplates_IsActive]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_LayoutTemplateCameraLink_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LayoutTemplateCameraLink] DROP CONSTRAINT [DF_LayoutTemplateCameraLink_CreateDate]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_CameraSettings_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CameraSettings] DROP CONSTRAINT [DF_CameraSettings_CreateDate]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_CameraSettings_IsActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CameraSettings] DROP CONSTRAINT [DF_CameraSettings_IsActive]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Cameras_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Cameras] DROP CONSTRAINT [DF_Cameras_CreateDate]
END
GO
/****** Object:  Table [dbo].[LayoutTemplatesUsage]    Script Date: 24.06.2019 0:35:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LayoutTemplatesUsage]') AND type in (N'U'))
DROP TABLE [dbo].[LayoutTemplatesUsage]
GO
/****** Object:  Table [dbo].[LayoutTemplates]    Script Date: 24.06.2019 0:35:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LayoutTemplates]') AND type in (N'U'))
DROP TABLE [dbo].[LayoutTemplates]
GO
/****** Object:  Table [dbo].[LayoutTemplateCameraLink]    Script Date: 24.06.2019 0:35:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LayoutTemplateCameraLink]') AND type in (N'U'))
DROP TABLE [dbo].[LayoutTemplateCameraLink]
GO
/****** Object:  Table [dbo].[CameraStates]    Script Date: 24.06.2019 0:35:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CameraStates]') AND type in (N'U'))
DROP TABLE [dbo].[CameraStates]
GO
/****** Object:  Table [dbo].[CameraSettings]    Script Date: 24.06.2019 0:35:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CameraSettings]') AND type in (N'U'))
DROP TABLE [dbo].[CameraSettings]
GO
/****** Object:  Table [dbo].[Cameras]    Script Date: 24.06.2019 0:35:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cameras]') AND type in (N'U'))
DROP TABLE [dbo].[Cameras]
GO
/****** Object:  Table [dbo].[Cameras]    Script Date: 24.06.2019 0:35:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cameras](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Number] [nvarchar](100) NOT NULL,
	[State] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Cameras] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CameraSettings]    Script Date: 24.06.2019 0:35:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CameraSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CameraId] [int] NOT NULL,
	[SourceAddress] [nvarchar](1024) NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_CameraSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CameraStates]    Script Date: 24.06.2019 0:35:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CameraStates](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_CameraStates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LayoutTemplateCameraLink]    Script Date: 24.06.2019 0:35:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LayoutTemplateCameraLink](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LayoutTemplateId] [int] NOT NULL,
	[CameraId] [int] NOT NULL,
	[X] [int] NOT NULL,
	[Y] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_LayoutTemplateCameraLink] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LayoutTemplates]    Script Date: 24.06.2019 0:35:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LayoutTemplates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ItemsX] [int] NOT NULL,
	[ItemsY] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_LayoutTemplates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LayoutTemplatesUsage]    Script Date: 24.06.2019 0:35:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LayoutTemplatesUsage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LayoutTemplateId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_LayoutTemplateUsage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cameras] ON 

INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (1, N'1й этаж, кабинет 01', N'№AF0001', 1, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (2, N'1й этаж, кабинет 02', N'№AF0002', 1, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (3, N'1й этаж, кабинет 03', N'№AF0003', 1, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (4, N'1й этаж, кабинет 04', N'№AF0004', 1, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (5, N'1й этаж, кабинет 05', N'№AF0005', 1, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (6, N'1й этаж, кабинет 06', N'№AF0006', 1, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (7, N'1й этаж, кабинет 07', N'№AF0007', 1, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (8, N'1й этаж, кабинет 08', N'№AF0008', 1, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (9, N'1й этаж, кабинет 09', N'№AF0009', 1, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (10, N'1й этаж, кабинет 10', N'№AF0010', 2, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (11, N'1й этаж, кабинет 11', N'№AF0011', 2, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (12, N'1й этаж, кабинет 12', N'№AF0012', 2, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (13, N'1й этаж, кабинет 13', N'№AF0013', 2, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (14, N'1й этаж, кабинет 14', N'№AF0014', 2, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (15, N'1й этаж, кабинет 15', N'№AF0015', 2, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (16, N'1й этаж, кабинет 16', N'№AF0016', 2, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (17, N'1й этаж, кабинет 17', N'№AF0017', 2, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (18, N'1й этаж, кабинет 18', N'№AF0018', 2, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (19, N'1й этаж, кабинет 19', N'№AF0019', 1, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
INSERT [dbo].[Cameras] ([Id], [Name], [Number], [State], [CreateDate], [UpdateDate]) VALUES (20, N'1й этаж, кабинет 20', N'№AF0020', 1, CAST(N'2019-06-17T00:39:37.680' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Cameras] OFF
SET IDENTITY_INSERT [dbo].[CameraSettings] ON 

INSERT [dbo].[CameraSettings] ([Id], [CameraId], [SourceAddress], [IsActive], [CreateDate], [UpdateDate]) VALUES (1, 1, N'C:\aiVideo\1.mp4', 1, CAST(N'2019-06-18T23:34:52.217' AS DateTime), NULL)
INSERT [dbo].[CameraSettings] ([Id], [CameraId], [SourceAddress], [IsActive], [CreateDate], [UpdateDate]) VALUES (2, 2, N'C:\aiVideo\2.mp4', 1, CAST(N'2019-06-18T23:34:54.433' AS DateTime), NULL)
INSERT [dbo].[CameraSettings] ([Id], [CameraId], [SourceAddress], [IsActive], [CreateDate], [UpdateDate]) VALUES (3, 3, N'C:\aiVideo\3.mp4', 1, CAST(N'2019-06-18T23:34:56.080' AS DateTime), NULL)
INSERT [dbo].[CameraSettings] ([Id], [CameraId], [SourceAddress], [IsActive], [CreateDate], [UpdateDate]) VALUES (4, 4, N'C:\aiVideo\4.mp4', 1, CAST(N'2019-06-18T23:34:59.260' AS DateTime), NULL)
INSERT [dbo].[CameraSettings] ([Id], [CameraId], [SourceAddress], [IsActive], [CreateDate], [UpdateDate]) VALUES (5, 5, N'C:\aiVideo\5.mp4', 1, CAST(N'2019-06-18T23:35:02.357' AS DateTime), NULL)
INSERT [dbo].[CameraSettings] ([Id], [CameraId], [SourceAddress], [IsActive], [CreateDate], [UpdateDate]) VALUES (6, 6, N'C:\aiVideo\6.mp4', 1, CAST(N'2019-06-18T23:35:05.300' AS DateTime), NULL)
INSERT [dbo].[CameraSettings] ([Id], [CameraId], [SourceAddress], [IsActive], [CreateDate], [UpdateDate]) VALUES (7, 7, N'C:\aiVideo\7.mp4', 1, CAST(N'2019-06-18T23:35:13.403' AS DateTime), NULL)
INSERT [dbo].[CameraSettings] ([Id], [CameraId], [SourceAddress], [IsActive], [CreateDate], [UpdateDate]) VALUES (8, 8, N'C:\aiVideo\8.mp4', 1, CAST(N'2019-06-18T23:35:15.730' AS DateTime), NULL)
INSERT [dbo].[CameraSettings] ([Id], [CameraId], [SourceAddress], [IsActive], [CreateDate], [UpdateDate]) VALUES (9, 9, N'C:\aiVideo\9.mp4', 1, CAST(N'2019-06-18T23:35:19.047' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[CameraSettings] OFF
INSERT [dbo].[CameraStates] ([Id], [Name]) VALUES (1, N'Работает')
INSERT [dbo].[CameraStates] ([Id], [Name]) VALUES (2, N'Не работает')
SET IDENTITY_INSERT [dbo].[LayoutTemplateCameraLink] ON 

INSERT [dbo].[LayoutTemplateCameraLink] ([Id], [LayoutTemplateId], [CameraId], [X], [Y], [CreateDate], [UpdateDate]) VALUES (1, 1, 1, 1, 1, CAST(N'2019-06-18T23:37:36.800' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplateCameraLink] ([Id], [LayoutTemplateId], [CameraId], [X], [Y], [CreateDate], [UpdateDate]) VALUES (2, 1, 2, 2, 1, CAST(N'2019-06-18T23:37:43.017' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplateCameraLink] ([Id], [LayoutTemplateId], [CameraId], [X], [Y], [CreateDate], [UpdateDate]) VALUES (3, 1, 3, 3, 1, CAST(N'2019-06-18T23:37:49.397' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplateCameraLink] ([Id], [LayoutTemplateId], [CameraId], [X], [Y], [CreateDate], [UpdateDate]) VALUES (4, 1, 4, 1, 2, CAST(N'2019-06-18T23:37:53.637' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplateCameraLink] ([Id], [LayoutTemplateId], [CameraId], [X], [Y], [CreateDate], [UpdateDate]) VALUES (5, 1, 5, 2, 2, CAST(N'2019-06-18T23:37:59.073' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplateCameraLink] ([Id], [LayoutTemplateId], [CameraId], [X], [Y], [CreateDate], [UpdateDate]) VALUES (6, 1, 6, 3, 2, CAST(N'2019-06-18T23:38:04.823' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplateCameraLink] ([Id], [LayoutTemplateId], [CameraId], [X], [Y], [CreateDate], [UpdateDate]) VALUES (7, 1, 7, 1, 3, CAST(N'2019-06-18T23:38:10.517' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplateCameraLink] ([Id], [LayoutTemplateId], [CameraId], [X], [Y], [CreateDate], [UpdateDate]) VALUES (8, 1, 8, 2, 3, CAST(N'2019-06-18T23:38:15.433' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplateCameraLink] ([Id], [LayoutTemplateId], [CameraId], [X], [Y], [CreateDate], [UpdateDate]) VALUES (9, 1, 9, 3, 3, CAST(N'2019-06-18T23:38:19.787' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[LayoutTemplateCameraLink] OFF
SET IDENTITY_INSERT [dbo].[LayoutTemplates] ON 

INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (1, N'Название сетки 01', 3, 3, 1, CAST(N'2019-06-15T22:26:40.713' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (2, N'Название сетки 02', 5, 5, 1, CAST(N'2019-06-15T22:27:15.767' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (3, N'Название сетки 03', 6, 6, 1, CAST(N'2019-06-15T22:27:23.543' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (4, N'Название сетки 04', 4, 4, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (5, N'Название сетки 05', 5, 5, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (6, N'Название сетки 06 (длинное-длинное-длинное)', 6, 6, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (7, N'Название сетки 07', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (8, N'Название сетки 08', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (9, N'Название сетки 09', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (10, N'Название сетки 10', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (11, N'Название сетки 11', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (12, N'Название сетки 12', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (13, N'Название сетки 13', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (14, N'Название сетки 14', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (15, N'Название сетки 15', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (16, N'Название сетки 16', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (17, N'Название сетки 17 (очень длинное-длинное-длинное)', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (18, N'Название сетки 18', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (19, N'Название сетки 19', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (20, N'Название сетки 20', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (21, N'Название сетки 21', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (22, N'Название сетки 22', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (23, N'Название сетки 23', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (24, N'Название сетки 24', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (25, N'Название сетки 25', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (26, N'Название сетки 26', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (27, N'Название сетки 27', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (28, N'Название сетки 28', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (29, N'Название сетки 29', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (30, N'Название сетки 30', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (31, N'Название сетки 31', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (32, N'Название сетки 32', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (33, N'Название сетки 33', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (34, N'Название сетки 34', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (35, N'Название сетки 35', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (36, N'Название сетки 36', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (37, N'Название сетки 37', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (38, N'Название сетки 38', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (39, N'Название сетки 39', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (40, N'Название сетки 40', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (41, N'Название сетки 41', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (42, N'Название сетки 42', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (43, N'Название сетки 43', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (44, N'Название сетки 44', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (45, N'Название сетки 45', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (46, N'Название сетки 46', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (47, N'Название сетки 47', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (48, N'Название сетки 48', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (49, N'Название сетки 49', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (50, N'Название сетки 50', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (51, N'Название сетки 51', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (52, N'Название сетки 52', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (53, N'Название сетки 53', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (54, N'Название сетки 54', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (55, N'Название сетки 55', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (56, N'Название сетки 56', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (57, N'Название сетки 57', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (58, N'Название сетки 58', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (59, N'Название сетки 59', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (60, N'Название сетки 60', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (61, N'Название сетки 61', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (62, N'Название сетки 62', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (63, N'Название сетки 63', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (64, N'Название сетки 64', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (65, N'Название сетки 65', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (66, N'Название сетки 66', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (67, N'Название сетки 67', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (68, N'Название сетки 68', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (69, N'Название сетки 69', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (70, N'Название сетки 70', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (71, N'Название сетки 71', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (72, N'Название сетки 72', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (73, N'Название сетки 73', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (74, N'Название сетки 74', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (75, N'Название сетки 75', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (76, N'Название сетки 76', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (77, N'Название сетки 77', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (78, N'Название сетки 78', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (79, N'Название сетки 79', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (80, N'Название сетки 80', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (81, N'Название сетки 81', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (82, N'Название сетки 82', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (83, N'Название сетки 83', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (84, N'Название сетки 84', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (85, N'Название сетки 85', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (86, N'Название сетки 86', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (87, N'Название сетки 87', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (88, N'Название сетки 88', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (89, N'Название сетки 89', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (90, N'Название сетки 90', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (91, N'Название сетки 91', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (92, N'Название сетки 92', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (93, N'Название сетки 93', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (94, N'Название сетки 94', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (95, N'Название сетки 95', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (96, N'Название сетки 96', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (97, N'Название сетки 97', 7, 7, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (98, N'Название сетки 98', 8, 8, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (99, N'Название сетки 99', 9, 9, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
GO
INSERT [dbo].[LayoutTemplates] ([Id], [Name], [ItemsX], [ItemsY], [IsActive], [CreateDate], [UpdateDate]) VALUES (100, N'Название сетки 100', 10, 10, 1, CAST(N'2019-06-16T18:44:49.223' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[LayoutTemplates] OFF
ALTER TABLE [dbo].[Cameras] ADD  CONSTRAINT [DF_Cameras_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[CameraSettings] ADD  CONSTRAINT [DF_CameraSettings_IsActive]  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
ALTER TABLE [dbo].[CameraSettings] ADD  CONSTRAINT [DF_CameraSettings_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[LayoutTemplateCameraLink] ADD  CONSTRAINT [DF_LayoutTemplateCameraLink_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[LayoutTemplates] ADD  CONSTRAINT [DF_LayoutTemplates_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LayoutTemplates] ADD  CONSTRAINT [DF_LayoutTemplates_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[LayoutTemplatesUsage] ADD  CONSTRAINT [DF_LayoutTemplatesUsage_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Cameras]  WITH CHECK ADD  CONSTRAINT [FK_Cameras_CameraStates] FOREIGN KEY([State])
REFERENCES [dbo].[CameraStates] ([Id])
GO
ALTER TABLE [dbo].[Cameras] CHECK CONSTRAINT [FK_Cameras_CameraStates]
GO
ALTER TABLE [dbo].[CameraSettings]  WITH CHECK ADD  CONSTRAINT [FK_CameraSettings_Cameras] FOREIGN KEY([CameraId])
REFERENCES [dbo].[Cameras] ([Id])
GO
ALTER TABLE [dbo].[CameraSettings] CHECK CONSTRAINT [FK_CameraSettings_Cameras]
GO
ALTER TABLE [dbo].[LayoutTemplateCameraLink]  WITH CHECK ADD  CONSTRAINT [FK_LayoutTemplateCameraLink_Cameras] FOREIGN KEY([CameraId])
REFERENCES [dbo].[Cameras] ([Id])
GO
ALTER TABLE [dbo].[LayoutTemplateCameraLink] CHECK CONSTRAINT [FK_LayoutTemplateCameraLink_Cameras]
GO
ALTER TABLE [dbo].[LayoutTemplateCameraLink]  WITH CHECK ADD  CONSTRAINT [FK_LayoutTemplateCameraLink_LayoutTemplates] FOREIGN KEY([LayoutTemplateId])
REFERENCES [dbo].[LayoutTemplates] ([Id])
GO
ALTER TABLE [dbo].[LayoutTemplateCameraLink] CHECK CONSTRAINT [FK_LayoutTemplateCameraLink_LayoutTemplates]
GO
