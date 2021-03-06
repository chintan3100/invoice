USE [master]
GO
/****** Object:  Database [Invoice]    Script Date: 8/26/2017 4:55:38 PM ******/
CREATE DATABASE [Invoice]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Invoice', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Invoice.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Invoice_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Invoice_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Invoice] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Invoice].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Invoice] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Invoice] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Invoice] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Invoice] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Invoice] SET ARITHABORT OFF 
GO
ALTER DATABASE [Invoice] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Invoice] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Invoice] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Invoice] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Invoice] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Invoice] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Invoice] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Invoice] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Invoice] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Invoice] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Invoice] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Invoice] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Invoice] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Invoice] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [Invoice] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Invoice] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Invoice] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Invoice] SET RECOVERY FULL 
GO
ALTER DATABASE [Invoice] SET  MULTI_USER 
GO
ALTER DATABASE [Invoice] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Invoice] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Invoice] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Invoice] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Invoice] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Invoice]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 8/26/2017 4:55:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[GSTIN] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[StateCode] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DetailOfConsignee]    Script Date: 8/26/2017 4:55:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailOfConsignee](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[GSTIN] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[StateCode] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 8/26/2017 4:55:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](max) NOT NULL,
	[Date] [date] NOT NULL,
	[State] [nvarchar](max) NULL,
	[StateCode] [bigint] NULL,
	[ReverseCharge] [nvarchar](max) NULL,
	[TransportationModeId] [bigint] NULL,
	[CustomerId] [bigint] NULL,
	[PaymentDetailId] [bigint] NULL,
	[DetailOfConsigneeId] [bigint] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentDetail]    Script Date: 8/26/2017 4:55:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Mode] [nvarchar](max) NULL,
	[IFSCCode] [nvarchar](max) NULL,
	[AccountNumber] [nvarchar](max) NULL,
	[Amount] [numeric](18, 2) NULL,
 CONSTRAINT [PK__PaymentD__3214EC075A57B44B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 8/26/2017 4:55:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[HSN] [int] NULL,
	[UOM] [int] NULL,
	[Quantity] [int] NULL,
	[Rate] [numeric](18, 2) NULL,
	[Amount] [numeric](18, 2) NULL,
	[Discount] [float] NULL,
	[TaxableValue] [numeric](18, 2) NULL,
	[CGSTRate] [float] NULL,
	[CGSTAmount] [numeric](18, 2) NULL,
	[SGSTRate] [float] NULL,
	[SGSTAmount] [numeric](18, 2) NULL,
	[IGSTRate] [float] NULL,
	[IGSTAmount] [numeric](18, 2) NULL,
	[Total] [numeric](18, 2) NULL,
	[InvoiceId] [bigint] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TransportaionMode]    Script Date: 8/26/2017 4:55:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransportaionMode](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[VechicleNo] [nvarchar](max) NULL,
	[DateOfSupply] [datetime] NULL,
	[PlaceOfSupply] [nvarchar](max) NULL,
	[Mode] [nvarchar](max) NULL,
	[StateCode] [bigint] NULL,
 CONSTRAINT [PK__Transpor__3214EC0732A4245C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Customer]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_DetailOfConsignee] FOREIGN KEY([DetailOfConsigneeId])
REFERENCES [dbo].[DetailOfConsignee] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_DetailOfConsignee]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Invoice] FOREIGN KEY([Id])
REFERENCES [dbo].[Invoice] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Invoice]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_PaymentDetail] FOREIGN KEY([PaymentDetailId])
REFERENCES [dbo].[PaymentDetail] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_PaymentDetail]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_TransportaionMode] FOREIGN KEY([TransportationModeId])
REFERENCES [dbo].[TransportaionMode] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_TransportaionMode]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Invoice]
GO
USE [master]
GO
ALTER DATABASE [Invoice] SET  READ_WRITE 
GO
