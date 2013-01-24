Create Database EntityFrameworkDemo
Go
USE [EntityFrameworkDemo]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 05/24/2012 06:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Website] [varchar](50) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Company] ON
INSERT [dbo].[Company] ([Id], [Name], [Website]) VALUES (84, N'Microsoft', N'www.bing.com')
SET IDENTITY_INSERT [dbo].[Company] OFF
/****** Object:  Table [dbo].[Employee]    Script Date: 05/24/2012 06:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Boss] [int] NULL,
	[Company] [int] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON
INSERT [dbo].[Employee] ([Id], [Name], [Boss], [Company]) VALUES (92, N'Lynn', 93, 84)
INSERT [dbo].[Employee] ([Id], [Name], [Boss], [Company]) VALUES (93, N'Steve', NULL, 84)
SET IDENTITY_INSERT [dbo].[Employee] OFF
/****** Object:  Table [dbo].[Job]    Script Date: 05/24/2012 06:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Job](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NULL,
	[Employee] [int] NOT NULL,
	[Status] [varchar](50) NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Job] ON
INSERT [dbo].[Job] ([Id], [Title], [Employee], [Status]) VALUES (81, N'Developer', 92, N'old')
INSERT [dbo].[Job] ([Id], [Title], [Employee], [Status]) VALUES (82, N'SqlAzure Evanglist', 92, N'current')
SET IDENTITY_INSERT [dbo].[Job] OFF
/****** Object:  Table [dbo].[Events]    Script Date: 05/24/2012 06:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Events](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Employee] [int] NULL,
	[EventTitle] [varchar](50) NULL,
	[Details] [varchar](50) NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Events] ON
INSERT [dbo].[Events] ([Id], [Employee], [EventTitle], [Details]) VALUES (69, NULL, N'SxSW', NULL)
INSERT [dbo].[Events] ([Id], [Employee], [EventTitle], [Details]) VALUES (70, 92, N'Sql VUG', NULL)
SET IDENTITY_INSERT [dbo].[Events] OFF
/****** Object:  ForeignKey [FK_Boss]    Script Date: 05/24/2012 06:00:48 ******/
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Boss] FOREIGN KEY([Boss])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Boss]
GO
/****** Object:  ForeignKey [FK_Employee_Company]    Script Date: 05/24/2012 06:00:48 ******/
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Company] FOREIGN KEY([Company])
REFERENCES [dbo].[Company] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Company]
GO
/****** Object:  ForeignKey [FK_Events_Employee]    Script Date: 05/24/2012 06:00:48 ******/
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Employee] FOREIGN KEY([Employee])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Employee]
GO
/****** Object:  ForeignKey [FK_Job_Employee]    Script Date: 05/24/2012 06:00:48 ******/
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_Employee] FOREIGN KEY([Employee])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_Employee]
GO
