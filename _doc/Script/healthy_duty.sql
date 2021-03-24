USE [HealthyDuty ]
GO
/****** Object:  Table [dbo].[Auth]    Script Date: 24.03.2021 18:19:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auth](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](150) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
 CONSTRAINT [PK_Auth] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BloodGroup]    Script Date: 24.03.2021 18:19:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BloodGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_BloodGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 24.03.2021 18:19:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hospital]    Script Date: 24.03.2021 18:19:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hospital](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](250) NOT NULL,
	[CityCode] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
 CONSTRAINT [PK_Hospital] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NeedForBlood]    Script Date: 24.03.2021 18:19:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NeedForBlood](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BloodGroupId] [int] NOT NULL,
	[HospitalId] [int] NOT NULL,
	[DonorName] [nvarchar](100) NULL,
	[DonorLastName] [nvarchar](100) NULL,
	[DonorPhone] [nvarchar](50) NULL,
	[IsFound] [bit] NOT NULL,
	[FoundDateTime] [datetime] NULL,
 CONSTRAINT [PK_NeedForBlood] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personnel]    Script Date: 24.03.2021 18:19:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personnel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TC] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](200) NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[HospitalId] [int] NOT NULL,
 CONSTRAINT [PK_Personnel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 24.03.2021 18:19:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](150) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDateTime] [datetime] NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfileDetail]    Script Date: 24.03.2021 18:19:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfileId] [int] NOT NULL,
	[AuthId] [int] NOT NULL,
 CONSTRAINT [PK_ProfileDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfilePersonnel]    Script Date: 24.03.2021 18:19:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfilePersonnel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfileId] [int] NOT NULL,
	[PersonnelId] [int] NOT NULL,
 CONSTRAINT [PK_ProfilePersonnel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sex]    Script Date: 24.03.2021 18:19:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sex](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Sex] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 24.03.2021 18:19:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Age] [int] NOT NULL,
	[Email] [nvarchar](50) NULL,
	[SexId] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[BloodGroupId] [int] NOT NULL,
	[CityCode] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Auth] ON 

INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (1, N'PAGE_AUTH_LIST', N'Auth List', 1, CAST(N'2021-03-24T15:40:38.080' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (2, N'PAGE_AUTH_ADD', N'Auth Add', 1, CAST(N'2021-03-24T15:41:31.333' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (3, N'PAGE_AUTH_EDIT', N'Auth Edit', 1, CAST(N'2021-03-24T15:42:01.540' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (4, N'PAGE_AUTH_DISPLAY', N'Auth Display', 1, CAST(N'2021-03-24T15:42:14.917' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (5, N'PAGE_AUTH_DELETE', N'Auth Display', 1, CAST(N'2021-03-24T15:42:26.127' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (6, N'PAGE_BLOODGROUP_LIST', N'Blood Group List', 1, CAST(N'2021-03-24T15:43:06.167' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (7, N'PAGE_BLOODGROUP_ADD', N'Blood Group Add', 1, CAST(N'2021-03-24T15:43:22.313' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (8, N'PAGE_BLOODGROUP_EDIT', N'Blood Group Edit', 1, CAST(N'2021-03-24T15:43:38.243' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (9, N'PAGE_CITY_LIST', N'City List', 1, CAST(N'2021-03-24T15:43:50.740' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (10, N'PAGE_CITY_ADD', N'City Add', 1, CAST(N'2021-03-24T15:44:09.133' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (11, N'PAGE_CITY_EDIT', N'City Edit', 1, CAST(N'2021-03-24T15:44:22.820' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (12, N'PAGE_CITY_DISPLAY', N'City Display', 1, CAST(N'2021-03-24T15:44:36.857' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (13, N'PAGE_HOSPITAL_LIST', N'Hospital List', 1, CAST(N'2021-03-24T15:44:50.623' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (14, N'PAGE_HOSPITAL_ADD', N'Hospital Add', 1, CAST(N'2021-03-24T15:45:04.610' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (15, N'PAGE_HOSPITAL_EDIT', N'Hospital Edit', 1, CAST(N'2021-03-24T15:45:16.403' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (16, N'PAGE_HOSPITAL_DISPLAY', N'Hospital Display', 1, CAST(N'2021-03-24T15:45:30.267' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (17, N'PAGE_HOSPITAL_DELETE', N'Hospital Delete', 1, CAST(N'2021-03-24T15:45:41.150' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (18, N'PAGE_NEEDFORBLOOD_LISTFORCURRENTUSER', N'Need For Blood - List For Current User', 1, CAST(N'2021-03-24T15:46:33.103' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (19, N'PAGE_NEEDFORBLOOD_ADDFORCURRENTUSER', N'Need For Blood - Add For Current User', 1, CAST(N'2021-03-24T15:46:59.827' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (20, N'PAGE_NEEDFORBLOOD_EDITFORCURRENTUSER', N'Need For Blood - Edit For Current User', 1, CAST(N'2021-03-24T15:47:17.850' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (21, N'PAGE_PERSONNEL_LIST', N'Personnel List', 1, CAST(N'2021-03-24T15:47:46.500' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (22, N'PAGE_PERSONNEL_ADD', N'Personnel Add', 1, CAST(N'2021-03-24T15:48:01.623' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (23, N'PAGE_PERSONNEL_EDIT', N'Personnel Edit', 1, CAST(N'2021-03-24T15:48:15.823' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (24, N'PAGE_PERSONNEL_DISPLAY', N'Personnel Display', 1, CAST(N'2021-03-24T15:48:32.090' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (25, N'PAGE_PROFILEDETAIL_BATCHEDIT', N'Profile Detail Batch Edit', 1, CAST(N'2021-03-24T15:48:50.247' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (26, N'PAGE_PROFILEPERSONNEL_BATCHEDIT', N'Profile Personnel - Batch Edit', 1, CAST(N'2021-03-24T15:49:12.057' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (27, N'PAGE_PROFILE_LIST', N'Profile List', 1, CAST(N'2021-03-24T15:49:28.730' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (28, N'PAGE_PROFILE_ADD', N'Profile Add', 1, CAST(N'2021-03-24T15:49:40.087' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (29, N'PAGE_PROFILE_EDIT', N'Profile Edit', 1, CAST(N'2021-03-24T15:49:50.693' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (30, N'PAGE_PROFILE_DISPLAY', N'Profile Display', 1, CAST(N'2021-03-24T15:50:06.003' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (31, N'PAGE_PROFILE_DELETE', N'Profile Delete', 1, CAST(N'2021-03-24T15:50:18.113' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (32, N'PAGE_USER_LIST', N'User List', 1, CAST(N'2021-03-24T15:50:30.383' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (33, N'PAGE_USER_ADD', N'User Add', 1, CAST(N'2021-03-24T15:50:41.503' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (34, N'PAGE_USER_EDIT', N'User Edit', 1, CAST(N'2021-03-24T15:50:55.037' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (35, N'PAGE_USER_DISPLAY', N'User Display', 1, CAST(N'2021-03-24T15:51:11.140' AS DateTime), 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Auth] OFF
GO
SET IDENTITY_INSERT [dbo].[BloodGroup] ON 

INSERT [dbo].[BloodGroup] ([Id], [Name]) VALUES (1, N'AB Rh+')
INSERT [dbo].[BloodGroup] ([Id], [Name]) VALUES (2, N'AB Rh-')
INSERT [dbo].[BloodGroup] ([Id], [Name]) VALUES (3, N'A Rh+')
INSERT [dbo].[BloodGroup] ([Id], [Name]) VALUES (4, N'A Rh-')
INSERT [dbo].[BloodGroup] ([Id], [Name]) VALUES (5, N'B Rh+')
INSERT [dbo].[BloodGroup] ([Id], [Name]) VALUES (6, N'B Rh-')
INSERT [dbo].[BloodGroup] ([Id], [Name]) VALUES (7, N'O Rh+')
INSERT [dbo].[BloodGroup] ([Id], [Name]) VALUES (8, N'O Rh-')
SET IDENTITY_INSERT [dbo].[BloodGroup] OFF
GO
SET IDENTITY_INSERT [dbo].[Hospital] ON 

INSERT [dbo].[Hospital] ([Id], [Name], [Phone], [Address], [CityCode], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (1, N'Darıca Farabi Eğitim ve Araştırma Hastanesi', N'(0262) 656 43 45', N'Fevziçakmak Mahallesi, Dr. Zeki Acar Cd. No:62, 41700 Darıca/Kocaeli', 41, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Hospital] OFF
GO
SET IDENTITY_INSERT [dbo].[Personnel] ON 

INSERT [dbo].[Personnel] ([Id], [TC], [Name], [LastName], [Phone], [Address], [UserName], [Password], [HospitalId]) VALUES (1, N'12345678912', N'Admin', N'Profile', N'5453439867', N'A Sk. B Cd. C Mh. D:1 E City ', N'admin', N'1', 1)
INSERT [dbo].[Personnel] ([Id], [TC], [Name], [LastName], [Phone], [Address], [UserName], [Password], [HospitalId]) VALUES (2, N'98765432123', N'Talha', N'Erdogan', N'5453457789', N'Adres Bilgisi ', N'talha', N'1', 1)
SET IDENTITY_INSERT [dbo].[Personnel] OFF
GO
SET IDENTITY_INSERT [dbo].[Profile] ON 

INSERT [dbo].[Profile] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (1, N'SYSTEMADMIN', N'System Admin Profile', 1, CAST(N'2021-03-24T00:00:00.000' AS DateTime), 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Profile] OFF
GO
