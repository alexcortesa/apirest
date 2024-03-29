USE [master]
GO
/****** Object:  Database [ContactsDB]    Script Date: 28/06/2019 03:37:56 a. m. ******/
CREATE DATABASE [ContactsDB] ON  PRIMARY 
( NAME = N'ContactsDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ContactsDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ContactsDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ContactsDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ContactsDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ContactsDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ContactsDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ContactsDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ContactsDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ContactsDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ContactsDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ContactsDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ContactsDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ContactsDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ContactsDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ContactsDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ContactsDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ContactsDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ContactsDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ContactsDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ContactsDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ContactsDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ContactsDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ContactsDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ContactsDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ContactsDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ContactsDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ContactsDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ContactsDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ContactsDB] SET  MULTI_USER 
GO
ALTER DATABASE [ContactsDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ContactsDB] SET DB_CHAINING OFF 
GO
USE [ContactsDB]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 28/06/2019 03:37:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ContactId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](10) NOT NULL,
	[PhoneMovil] [nvarchar](10) NULL,
	[DateCreate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 28/06/2019 03:37:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[DeleteContact]    Script Date: 28/06/2019 03:37:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteContact]
	@contactId UNIQUEIDENTIFIER
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM Contacts WHERE ContactId = @contactId

END
GO
/****** Object:  StoredProcedure [dbo].[GetContacts]    Script Date: 28/06/2019 03:37:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetContacts]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT CN.ContactId, CN.FirstName, CN.LastName, CN.PhoneNumber, CN.PhoneMovil, CN.Email, CN.DateCreate
	FROM Contacts AS CN
	ORDER BY CN.LastName, CN.FirstName
END
GO
/****** Object:  StoredProcedure [dbo].[InsertContact]    Script Date: 28/06/2019 03:37:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertContact]
	@firstName nvarchar(50),
	@lastName nvarchar(50),
	@Email nvarchar(50),
	@phoneNumber nvarchar(10),
	@movil nvarchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF EXISTS(SELECT * FROM Contacts WHERE PhoneMovil = @movil)
	BEGIN
		RETURN 0;
	END
	ELSE
	BEGIN
		INSERT INTO Contacts (ContactId, FirstName, LastName, Email, PhoneNumber, PhoneMovil, DateCreate)
				VALUES   (NEWID(), @firstName, @lastName, @Email, @phoneNumber, @movil, GETDATE())

		RETURN 1;
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ValidaUser]    Script Date: 28/06/2019 03:37:56 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Alejandro Cortes>
-- Create date: <27, 06 2019>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValidaUser]
	@email nvarchar(50),
	@password nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT UserId, FirstName, LastName, Email
	FROM Users
	WHERE Email = @email AND [Password] = @password

END
GO
USE [master]
GO
ALTER DATABASE [ContactsDB] SET  READ_WRITE 
GO
