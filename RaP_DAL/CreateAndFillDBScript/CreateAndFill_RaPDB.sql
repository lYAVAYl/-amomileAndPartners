USE [master]
GO
/****** Object:  Database [RaP]    Script Date: 2020/09/06 0:41:56 ******/
CREATE DATABASE [RaP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RaP', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RaP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RaP_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RaP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RaP] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RaP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RaP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RaP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RaP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RaP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RaP] SET ARITHABORT OFF 
GO
ALTER DATABASE [RaP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RaP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RaP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RaP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RaP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RaP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RaP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RaP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RaP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RaP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RaP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RaP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RaP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RaP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RaP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RaP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RaP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RaP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RaP] SET  MULTI_USER 
GO
ALTER DATABASE [RaP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RaP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RaP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RaP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RaP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RaP] SET QUERY_STORE = OFF
GO
USE [RaP]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 2020/09/06 0:41:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[ContractStatus] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2020/09/06 0:41:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](150) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Companies] ON 

INSERT [dbo].[Companies] ([Id], [Name], [ContractStatus]) VALUES (1, N'Компания 1', N'Заключен')
INSERT [dbo].[Companies] ([Id], [Name], [ContractStatus]) VALUES (2, N'Компания 2', N'Ещё не заключен')
INSERT [dbo].[Companies] ([Id], [Name], [ContractStatus]) VALUES (3, N'Компания 3', N'Ещё не заключён')
INSERT [dbo].[Companies] ([Id], [Name], [ContractStatus]) VALUES (4, N'CompanyWithLongLongLongLongLongLongLongLongLongLongLOOOOoooOOOOoooooOooooOoOooOOoooongName', N'Расторгнут')
INSERT [dbo].[Companies] ([Id], [Name], [ContractStatus]) VALUES (6, N'Компания 6', N'Расторгнут')
SET IDENTITY_INSERT [dbo].[Companies] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Login], [Password], [CompanyId]) VALUES (1, N'User1', N'pass1234', 1)
INSERT [dbo].[Users] ([Id], [Login], [Password], [CompanyId]) VALUES (2, N'UserWithLongLongLongLongLongLongLongLongLonglongLongLoooooOOoOooOOOOoooongLogin', N'pass1234', 1)
INSERT [dbo].[Users] ([Id], [Login], [Password], [CompanyId]) VALUES (3, N'User3', N'pass1234', 2)
INSERT [dbo].[Users] ([Id], [Login], [Password], [CompanyId]) VALUES (4, N'User4', N'pass1234', 2)
INSERT [dbo].[Users] ([Id], [Login], [Password], [CompanyId]) VALUES (5, N'User5', N'pass1234', 3)
INSERT [dbo].[Users] ([Id], [Login], [Password], [CompanyId]) VALUES (6, N'User6', N'pass1234', 3)
INSERT [dbo].[Users] ([Id], [Login], [Password], [CompanyId]) VALUES (7, N'User7', N'pass1234', 4)
INSERT [dbo].[Users] ([Id], [Login], [Password], [CompanyId]) VALUES (8, N'User8', N'pass1234', 4)
INSERT [dbo].[Users] ([Id], [Login], [Password], [CompanyId]) VALUES (9, N'User9', N'pass1234', 6)
INSERT [dbo].[Users] ([Id], [Login], [Password], [CompanyId]) VALUES (10, N'User10', N'pass1234', NULL)
INSERT [dbo].[Users] ([Id], [Login], [Password], [CompanyId]) VALUES (11, N'User11', N'pass1234', NULL)
INSERT [dbo].[Users] ([Id], [Login], [Password], [CompanyId]) VALUES (13, N'User12', N'112233kdlsfs', 3)
INSERT [dbo].[Users] ([Id], [Login], [Password], [CompanyId]) VALUES (14, N'User14', N'Yesww22', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Companies]
GO
USE [master]
GO
ALTER DATABASE [RaP] SET  READ_WRITE 
GO
