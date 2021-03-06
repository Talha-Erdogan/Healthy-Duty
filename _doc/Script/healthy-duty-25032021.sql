USE [HealthyDuty ]
GO
/****** Object:  Table [dbo].[Auth]    Script Date: 25.03.2021 15:28:13 ******/
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
/****** Object:  Table [dbo].[BloodGroup]    Script Date: 25.03.2021 15:28:13 ******/
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
/****** Object:  Table [dbo].[City]    Script Date: 25.03.2021 15:28:13 ******/
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
/****** Object:  Table [dbo].[Hospital]    Script Date: 25.03.2021 15:28:13 ******/
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
/****** Object:  Table [dbo].[NeedForBlood]    Script Date: 25.03.2021 15:28:13 ******/
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
/****** Object:  Table [dbo].[Personnel]    Script Date: 25.03.2021 15:28:13 ******/
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
/****** Object:  Table [dbo].[Profile]    Script Date: 25.03.2021 15:28:13 ******/
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
/****** Object:  Table [dbo].[ProfileDetail]    Script Date: 25.03.2021 15:28:13 ******/
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
/****** Object:  Table [dbo].[ProfilePersonnel]    Script Date: 25.03.2021 15:28:13 ******/
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
/****** Object:  Table [dbo].[Sex]    Script Date: 25.03.2021 15:28:13 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 25.03.2021 15:28:13 ******/
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
SET IDENTITY_INSERT [dbo].[City] ON 

INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (1, 1, N'Adana')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (2, 2, N'Adıyaman')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (3, 3, N'Afyon')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (4, 4, N'Ağrı')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (5, 5, N'Amasya')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (6, 6, N'Ankara')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (7, 7, N'Antalya')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (8, 8, N'Artvin')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (9, 9, N'Aydın')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (10, 10, N'Balıkesir')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (11, 11, N'Bilecik')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (12, 12, N'Bingöl')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (13, 13, N'Bitlis')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (14, 14, N'Bolu')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (15, 15, N'Burdur')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (16, 16, N'Bursa')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (17, 17, N'Çanakkale')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (18, 18, N'Çankırı')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (19, 19, N'Çorum')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (20, 20, N'Denizli')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (21, 21, N'Diyarbakır')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (22, 22, N'Edirne')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (23, 23, N'Elazığ')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (24, 24, N'Erzincan')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (25, 25, N'Erzurum')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (26, 26, N'Eskişehir')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (27, 27, N'Gaziantep')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (28, 28, N'Giresun')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (29, 29, N'Gümüşhane')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (30, 30, N'Hakkari')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (31, 31, N'Hatay')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (32, 32, N'Isparta')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (33, 33, N'Mersin')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (34, 34, N'İstanbul')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (35, 35, N'İzmir')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (36, 36, N'Kars')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (37, 37, N'Kastamonu')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (38, 38, N'Kayseri')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (39, 39, N'Kırklareli')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (40, 40, N'Kırşehir')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (41, 41, N'Kocaeli')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (42, 42, N'Konya')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (43, 43, N'Kütahya')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (44, 44, N'Malatya')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (45, 45, N'Manisa')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (46, 46, N'Kahraman Maraş')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (47, 47, N'Mardin')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (48, 48, N'Muğla')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (49, 49, N'Muş')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (50, 50, N'Nevşehir')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (51, 51, N'Niğde')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (52, 52, N'Ordu')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (53, 53, N'Rize')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (54, 54, N'Sakarya')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (55, 55, N'Samsun')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (56, 56, N'Siirt')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (57, 57, N'Sinop')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (58, 58, N'Sivas')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (59, 59, N'Tekirdağ')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (60, 60, N'Tokat')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (61, 61, N'Trabzon')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (62, 62, N'Tunceli')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (63, 63, N'Şanlıurfa')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (64, 64, N'Uşak')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (65, 65, N'Van')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (66, 66, N'Yozgat')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (67, 67, N'Zonguldak')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (68, 68, N'Aksaray')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (69, 69, N'Bayburt')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (70, 70, N'Karaman')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (71, 71, N'Kırıkkale')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (72, 72, N'Batman')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (73, 73, N'Şırnak')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (74, 74, N'Bartın')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (75, 75, N'Ardahan')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (76, 76, N'Iğdır')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (77, 77, N'Yalova')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (78, 78, N'Karabük')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (79, 79, N'Kilis')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (80, 80, N'Osmaniye')
INSERT [dbo].[City] ([Id], [Code], [Name]) VALUES (81, 81, N'Düzce')
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[Hospital] ON 

INSERT [dbo].[Hospital] ([Id], [Name], [Phone], [Address], [CityCode], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (1, N'Darıca Farabi Eğitim ve Araştırma Hastanesi', N'(0262) 656 43 45', N'Fevziçakmak Mahallesi, Dr. Zeki Acar Cd. No:62, 41700 Darıca/Kocaeli', 41, 0, NULL, NULL)
INSERT [dbo].[Hospital] ([Id], [Name], [Phone], [Address], [CityCode], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (2, N'Gebze Fatih Devlet Hastanesi', N'(0262) 502 22 40', N'Osman Yılmaz, 1, İstanbul Cd. No:127, 41400 Gebze/Kocaeli', 41, 0, NULL, NULL)
INSERT [dbo].[Hospital] ([Id], [Name], [Phone], [Address], [CityCode], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (3, N'Anadolu Sağlık Merkezi ・ Sağlığın Merkezi', N'444 4 276', N'Cumhuriyet Mah, 2255. Sk. No: 3, 41400 Gebze/Kocaeli', 41, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Hospital] OFF
GO
SET IDENTITY_INSERT [dbo].[NeedForBlood] ON 

INSERT [dbo].[NeedForBlood] ([Id], [BloodGroupId], [HospitalId], [DonorName], [DonorLastName], [DonorPhone], [IsFound], [FoundDateTime]) VALUES (1, 4, 1, N'Talha', N'Erdoğan', N'55365478965', 1, CAST(N'2021-03-25T15:17:34.283' AS DateTime))
INSERT [dbo].[NeedForBlood] ([Id], [BloodGroupId], [HospitalId], [DonorName], [DonorLastName], [DonorPhone], [IsFound], [FoundDateTime]) VALUES (2, 2, 1, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[NeedForBlood] ([Id], [BloodGroupId], [HospitalId], [DonorName], [DonorLastName], [DonorPhone], [IsFound], [FoundDateTime]) VALUES (3, 8, 1, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[NeedForBlood] ([Id], [BloodGroupId], [HospitalId], [DonorName], [DonorLastName], [DonorPhone], [IsFound], [FoundDateTime]) VALUES (4, 4, 2, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[NeedForBlood] ([Id], [BloodGroupId], [HospitalId], [DonorName], [DonorLastName], [DonorPhone], [IsFound], [FoundDateTime]) VALUES (5, 2, 2, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[NeedForBlood] ([Id], [BloodGroupId], [HospitalId], [DonorName], [DonorLastName], [DonorPhone], [IsFound], [FoundDateTime]) VALUES (6, 4, 2, NULL, NULL, NULL, 0, NULL)
SET IDENTITY_INSERT [dbo].[NeedForBlood] OFF
GO
SET IDENTITY_INSERT [dbo].[Personnel] ON 

INSERT [dbo].[Personnel] ([Id], [TC], [Name], [LastName], [Phone], [Address], [UserName], [Password], [HospitalId]) VALUES (1, N'12345678912', N'Admin', N'Profile', N'5453439867', N'A Sk. B Cd. C Mh. D:1 E City ', N'admin', N'1', 1)
INSERT [dbo].[Personnel] ([Id], [TC], [Name], [LastName], [Phone], [Address], [UserName], [Password], [HospitalId]) VALUES (2, N'98765432123', N'Talha', N'Erdogan', N'5453457789', N'Adres Bilgisi ', N'talha', N'1', 1)
INSERT [dbo].[Personnel] ([Id], [TC], [Name], [LastName], [Phone], [Address], [UserName], [Password], [HospitalId]) VALUES (3, N'56897987654', N'Emre', N'Değirmenci', N'5369856321', N'Adres Bilgisi', N'emre', N'1', 2)
SET IDENTITY_INSERT [dbo].[Personnel] OFF
GO
SET IDENTITY_INSERT [dbo].[Profile] ON 

INSERT [dbo].[Profile] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (1, N'SYSTEMADMIN', N'System Admin Profile', 1, CAST(N'2021-03-24T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Profile] ([Id], [Code], [Name], [CreatedBy], [CreatedDateTime], [IsDeleted], [DeletedBy], [DeletedDateTime]) VALUES (2, N'HOSPITAL_PERSONEL_PROFILE', N'Hospital Personel Profile', 1, CAST(N'2021-03-24T20:16:15.193' AS DateTime), 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Profile] OFF
GO
SET IDENTITY_INSERT [dbo].[ProfileDetail] ON 

INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (1, 1, 2)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (2, 1, 4)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (3, 1, 5)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (4, 1, 3)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (5, 1, 1)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (6, 1, 7)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (7, 1, 8)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (8, 1, 6)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (9, 1, 10)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (10, 1, 12)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (11, 1, 11)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (12, 1, 9)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (13, 1, 14)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (14, 1, 17)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (15, 1, 16)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (16, 1, 15)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (17, 1, 13)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (21, 1, 22)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (22, 1, 24)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (23, 1, 23)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (24, 1, 21)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (25, 1, 28)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (26, 1, 31)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (27, 1, 25)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (28, 1, 30)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (29, 1, 29)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (30, 1, 27)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (31, 1, 26)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (32, 2, 19)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (33, 2, 20)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (34, 2, 18)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (35, 1, 33)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (36, 1, 35)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (37, 1, 34)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (38, 1, 32)
SET IDENTITY_INSERT [dbo].[ProfileDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[ProfilePersonnel] ON 

INSERT [dbo].[ProfilePersonnel] ([Id], [ProfileId], [PersonnelId]) VALUES (1, 1, 1)
INSERT [dbo].[ProfilePersonnel] ([Id], [ProfileId], [PersonnelId]) VALUES (3, 2, 2)
INSERT [dbo].[ProfilePersonnel] ([Id], [ProfileId], [PersonnelId]) VALUES (4, 2, 3)
SET IDENTITY_INSERT [dbo].[ProfilePersonnel] OFF
GO
SET IDENTITY_INSERT [dbo].[Sex] ON 

INSERT [dbo].[Sex] ([Id], [Name]) VALUES (1, N'Male')
INSERT [dbo].[Sex] ([Id], [Name]) VALUES (2, N'Female')
SET IDENTITY_INSERT [dbo].[Sex] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Name], [LastName], [Phone], [Age], [Email], [SexId], [UserName], [Password], [BloodGroupId], [CityCode]) VALUES (1, N'Talha', N'Erdogan', N'5453457789', 26, N'abc@gmail.com', 1, N'talha', N'1', 8, 41)
INSERT [dbo].[User] ([Id], [Name], [LastName], [Phone], [Age], [Email], [SexId], [UserName], [Password], [BloodGroupId], [CityCode]) VALUES (2, N'Emre', N'Değirmenci', N'5468964521', 21, N'aaa@gmail.com', 1, N'emre', N'1', 2, 41)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
