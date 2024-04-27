USE [master]
GO
/****** Object:  Database [AutoDealership]    Script Date: 4/19/2024 1:57:43 PM ******/
CREATE DATABASE [AutoDealership]
GO
USE [AutoDealership]
GO
/****** Object:  Table [dbo].[AutoDealerships]    Script Date: 4/19/2024 1:57:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AutoDealerships](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CityId] [int] NULL,
	[Street] [nvarchar](50) NOT NULL,
	[Number] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_AutoDealerships] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 4/19/2024 1:57:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CountryId] [int] NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarBodyTypes]    Script Date: 4/19/2024 1:57:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarBodyTypes](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_CarBodyTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarComfortOptions]    Script Date: 4/19/2024 1:57:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarComfortOptions](
	[CarId] [int] NOT NULL,
	[ComfortOptionId] [int] NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__CarComfo__812AFC7B50B373A4] PRIMARY KEY CLUSTERED 
(
	[CarId] ASC,
	[ComfortOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarDelivery]    Script Date: 4/19/2024 1:57:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarDelivery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SaleId] [int] NOT NULL,
	[DistributorId] [int] NOT NULL,
	[DeliveryDate] [date] NOT NULL,
	[DeliveryCost] [decimal](10, 2) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__CarDeliv__626D8FCE5A2DD524] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarMultimediaOptions]    Script Date: 4/19/2024 1:57:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarMultimediaOptions](
	[CarId] [int] NOT NULL,
	[MultimediaOptionId] [int] NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__CarMulti__9D58B07C816F4ABB] PRIMARY KEY CLUSTERED 
(
	[CarId] ASC,
	[MultimediaOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 4/19/2024 1:57:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CarTypeId] [int] NOT NULL,
	[Doors] [int] NOT NULL,
	[Seats] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[ColorId] [int] NOT NULL,
	[BodyTypeId] [int] NOT NULL,
	[EngineId] [int] NOT NULL,
	[BrandId] [int] NULL,
	[Model] [nvarchar](50) NULL,
	[Generation] [nvarchar](50) NULL,
	[Weight] [int] NULL,
	[MaxSpeed] [int] NULL,
	[GearsCount] [int] NULL,
	[Height] [float] NULL,
	[Width] [float] NULL,
	[Length] [float] NULL,
	[GearBoxId] [int] NULL,
	[FuelTankCapacity] [int] NULL,
	[WheelsCount] [int] NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__Cars__3214EC07FD549B2F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarSafetyOptions]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarSafetyOptions](
	[CarId] [int] NOT NULL,
	[SafetyOptionId] [int] NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__CarSafet__7F73AA4C10FA322A] PRIMARY KEY CLUSTERED 
(
	[CarId] ASC,
	[SafetyOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarSales]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarSales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DealershipCarId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[SaleDate] [date] NOT NULL,
	[StatusId] [int] NULL,
	[ExpectedDeliveryDate] [date] NULL,
	[EmployeeId] [int] NULL,
	[PaymentMethodId] [int] NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__CarSales__1EE3C3FFD470F4B6] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarTypes]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarTypes](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_CarTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CountryId] [int] NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colors]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colors](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NOT NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_Colors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComfortOptions]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComfortOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OptionName] [nvarchar](255) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__ComfortO__3214EC07D7B7C2FA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Conditions]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conditions](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_Conditions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__Customer__A4AE64D8E3ED2AA2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DealershipCars]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DealershipCars](
	[Id] [int] NOT NULL,
	[CarId] [int] NOT NULL,
	[DealershipId] [int] NULL,
	[CarsCount] [int] NULL,
	[CarStatusId] [int] NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_DealershipCars] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DealershipCarStatuses]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DealershipCarStatuses](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_DealershipCarStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Distributors]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Distributors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DistributorName] [nvarchar](255) NOT NULL,
	[DistributorAddress] [nvarchar](255) NOT NULL,
	[DistributorIdentifier] [nvarchar](50) NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__Distribu__FD1AEB9E754F1A9A] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[DealershipId] [int] NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Engines]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Engines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BrandId] [int] NOT NULL,
	[EngineTypeId] [int] NOT NULL,
	[EngineVolume] [decimal](5, 2) NOT NULL,
	[EnginePower] [int] NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__Engines__3214EC0762EC2A83] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EngineTypes]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EngineTypes](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__EngineTy__737584F755900501] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GearBoxTypes]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GearBoxTypes](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_GearBoxTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaseProposalConditions]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaseProposalConditions](
	[Id] [int] NOT NULL,
	[LeaseProposalId] [int] NOT NULL,
	[ConditionId] [int] NOT NULL,
	[Value] [bit] NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_LeaseProposalConditions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaseProposals]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaseProposals](
	[Id] [int] NOT NULL,
	[LeaseTypeId] [int] NOT NULL,
	[InsuranceRequired] [bit] NOT NULL,
	[MonthlyPayment] [decimal](10, 2) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_LeaseProposals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Leases]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL,
	[ProposalId] [int] NULL,
	[CustomerId] [int] NOT NULL,
	[DealershipCarId] [int] NOT NULL,
	[LeaseSignDate] [date] NOT NULL,
	[LeaseStartDate] [date] NOT NULL,
	[LeaseEndDate] [date] NOT NULL,
	[LeaseUniqueNumber] [nvarchar](20) NULL,
	[TotalPrice] [decimal](10, 2) NULL,
	[Description] [nvarchar](max) NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__Lease__21FA58C149035493] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaseTypes]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaseTypes](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_LeaseTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MultimediaOptions]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MultimediaOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OptionName] [nvarchar](255) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__Multimed__3214EC0738D865A3] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethods]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethods](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_PaymentMethods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SafetyOptions]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SafetyOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OptionName] [nvarchar](255) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK__SafetyOp__3214EC0714E9FAC0] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleStatus]    Script Date: 4/19/2024 1:57:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleStatus](
	[Id] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_SaleStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_AutoDealerships_CityId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_AutoDealerships_CityId] ON [dbo].[AutoDealerships]
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Brands_CountryId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Brands_CountryId] ON [dbo].[Brands]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CarComfortOptions_ComfortOptionId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_CarComfortOptions_ComfortOptionId] ON [dbo].[CarComfortOptions]
(
	[ComfortOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CarDelivery_DistributorId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_CarDelivery_DistributorId] ON [dbo].[CarDelivery]
(
	[DistributorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CarDelivery_SaleId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_CarDelivery_SaleId] ON [dbo].[CarDelivery]
(
	[SaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CarMultimediaOptions_MultimediaOptionId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_CarMultimediaOptions_MultimediaOptionId] ON [dbo].[CarMultimediaOptions]
(
	[MultimediaOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cars_BodyTypeId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Cars_BodyTypeId] ON [dbo].[Cars]
(
	[BodyTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cars_BrandId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Cars_BrandId] ON [dbo].[Cars]
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cars_CarTypeId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Cars_CarTypeId] ON [dbo].[Cars]
(
	[CarTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cars_ColorId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Cars_ColorId] ON [dbo].[Cars]
(
	[ColorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cars_EngineId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Cars_EngineId] ON [dbo].[Cars]
(
	[EngineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cars_GearBoxId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Cars_GearBoxId] ON [dbo].[Cars]
(
	[GearBoxId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CarSafetyOptions_SafetyOptionId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_CarSafetyOptions_SafetyOptionId] ON [dbo].[CarSafetyOptions]
(
	[SafetyOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CarSales_CustomerId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_CarSales_CustomerId] ON [dbo].[CarSales]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CarSales_DealershipCarId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_CarSales_DealershipCarId] ON [dbo].[CarSales]
(
	[DealershipCarId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CarSales_EmployeeId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_CarSales_EmployeeId] ON [dbo].[CarSales]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CarSales_PaymentMethodId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_CarSales_PaymentMethodId] ON [dbo].[CarSales]
(
	[PaymentMethodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CarSales_StatusId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_CarSales_StatusId] ON [dbo].[CarSales]
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cities_CountryId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Cities_CountryId] ON [dbo].[Cities]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DealershipCars_CarStatusId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_DealershipCars_CarStatusId] ON [dbo].[DealershipCars]
(
	[CarStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DealershipCars_DealershipId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_DealershipCars_DealershipId] ON [dbo].[DealershipCars]
(
	[DealershipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_DealershipId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Employees_DealershipId] ON [dbo].[Employees]
(
	[DealershipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Engines_BrandId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Engines_BrandId] ON [dbo].[Engines]
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Engines_EngineTypeId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Engines_EngineTypeId] ON [dbo].[Engines]
(
	[EngineTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_LeaseProposalConditions_ConditionId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_LeaseProposalConditions_ConditionId] ON [dbo].[LeaseProposalConditions]
(
	[ConditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_LeaseProposalConditions_LeaseProposalId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_LeaseProposalConditions_LeaseProposalId] ON [dbo].[LeaseProposalConditions]
(
	[LeaseProposalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_LeaseProposals_LeaseTypeId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_LeaseProposals_LeaseTypeId] ON [dbo].[LeaseProposals]
(
	[LeaseTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Leases_CustomerId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Leases_CustomerId] ON [dbo].[Leases]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Leases_DealershipCarId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Leases_DealershipCarId] ON [dbo].[Leases]
(
	[DealershipCarId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Leases_EmployeeId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Leases_EmployeeId] ON [dbo].[Leases]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Leases_ProposalId]    Script Date: 4/19/2024 1:57:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_Leases_ProposalId] ON [dbo].[Leases]
(
	[ProposalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AutoDealerships]  WITH NOCHECK ADD  CONSTRAINT [FK_AutoDealerships_Cities] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
GO
ALTER TABLE [dbo].[AutoDealerships] CHECK CONSTRAINT [FK_AutoDealerships_Cities]
GO
ALTER TABLE [dbo].[Brands]  WITH NOCHECK ADD  CONSTRAINT [FK_Brands_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Brands] CHECK CONSTRAINT [FK_Brands_Countries]
GO
ALTER TABLE [dbo].[CarComfortOptions]  WITH CHECK ADD  CONSTRAINT [FK__CarComfor__CarId__37703C52] FOREIGN KEY([CarId])
REFERENCES [dbo].[Cars] ([Id])
GO
ALTER TABLE [dbo].[CarComfortOptions] CHECK CONSTRAINT [FK__CarComfor__CarId__37703C52]
GO
ALTER TABLE [dbo].[CarComfortOptions]  WITH CHECK ADD  CONSTRAINT [FK__CarComfor__Comfo__3864608B] FOREIGN KEY([ComfortOptionId])
REFERENCES [dbo].[ComfortOptions] ([Id])
GO
ALTER TABLE [dbo].[CarComfortOptions] CHECK CONSTRAINT [FK__CarComfor__Comfo__3864608B]
GO
ALTER TABLE [dbo].[CarDelivery]  WITH NOCHECK ADD  CONSTRAINT [FK__CarDelive__Distr__5BAD9CC8] FOREIGN KEY([DistributorId])
REFERENCES [dbo].[Distributors] ([Id])
GO
ALTER TABLE [dbo].[CarDelivery] CHECK CONSTRAINT [FK__CarDelive__Distr__5BAD9CC8]
GO
ALTER TABLE [dbo].[CarDelivery]  WITH NOCHECK ADD  CONSTRAINT [FK__CarDelive__SaleI__5AB9788F] FOREIGN KEY([SaleId])
REFERENCES [dbo].[CarSales] ([Id])
GO
ALTER TABLE [dbo].[CarDelivery] CHECK CONSTRAINT [FK__CarDelive__SaleI__5AB9788F]
GO
ALTER TABLE [dbo].[CarMultimediaOptions]  WITH CHECK ADD  CONSTRAINT [FK__CarMultim__CarId__3D2915A8] FOREIGN KEY([CarId])
REFERENCES [dbo].[Cars] ([Id])
GO
ALTER TABLE [dbo].[CarMultimediaOptions] CHECK CONSTRAINT [FK__CarMultim__CarId__3D2915A8]
GO
ALTER TABLE [dbo].[CarMultimediaOptions]  WITH CHECK ADD  CONSTRAINT [FK__CarMultim__Multi__3E1D39E1] FOREIGN KEY([MultimediaOptionId])
REFERENCES [dbo].[MultimediaOptions] ([Id])
GO
ALTER TABLE [dbo].[CarMultimediaOptions] CHECK CONSTRAINT [FK__CarMultim__Multi__3E1D39E1]
GO
ALTER TABLE [dbo].[Cars]  WITH NOCHECK ADD  CONSTRAINT [FK__Cars__EngineId__32AB8735] FOREIGN KEY([EngineId])
REFERENCES [dbo].[Engines] ([Id])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK__Cars__EngineId__32AB8735]
GO
ALTER TABLE [dbo].[Cars]  WITH NOCHECK ADD  CONSTRAINT [FK_Cars_Brands] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brands] ([Id])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_Brands]
GO
ALTER TABLE [dbo].[Cars]  WITH NOCHECK ADD  CONSTRAINT [FK_Cars_CarBodyTypes] FOREIGN KEY([BodyTypeId])
REFERENCES [dbo].[CarBodyTypes] ([Id])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_CarBodyTypes]
GO
ALTER TABLE [dbo].[Cars]  WITH NOCHECK ADD  CONSTRAINT [FK_Cars_Colors] FOREIGN KEY([ColorId])
REFERENCES [dbo].[Colors] ([Id])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_Colors]
GO
ALTER TABLE [dbo].[Cars]  WITH NOCHECK ADD  CONSTRAINT [FK_Cars_GearBoxTypes] FOREIGN KEY([GearBoxId])
REFERENCES [dbo].[GearBoxTypes] ([Id])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_GearBoxTypes]
GO
ALTER TABLE [dbo].[CarSafetyOptions]  WITH CHECK ADD  CONSTRAINT [FK__CarSafety__CarId__42E1EEFE] FOREIGN KEY([CarId])
REFERENCES [dbo].[Cars] ([Id])
GO
ALTER TABLE [dbo].[CarSafetyOptions] CHECK CONSTRAINT [FK__CarSafety__CarId__42E1EEFE]
GO
ALTER TABLE [dbo].[CarSafetyOptions]  WITH CHECK ADD  CONSTRAINT [FK__CarSafety__Safet__43D61337] FOREIGN KEY([SafetyOptionId])
REFERENCES [dbo].[SafetyOptions] ([Id])
GO
ALTER TABLE [dbo].[CarSafetyOptions] CHECK CONSTRAINT [FK__CarSafety__Safet__43D61337]
GO
ALTER TABLE [dbo].[CarSales]  WITH NOCHECK ADD  CONSTRAINT [FK__CarSales__Custom__55F4C372] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[CarSales] CHECK CONSTRAINT [FK__CarSales__Custom__55F4C372]
GO
ALTER TABLE [dbo].[CarSales]  WITH NOCHECK ADD  CONSTRAINT [FK_CarSales_DealershipCars] FOREIGN KEY([DealershipCarId])
REFERENCES [dbo].[DealershipCars] ([Id])
GO
ALTER TABLE [dbo].[CarSales] CHECK CONSTRAINT [FK_CarSales_DealershipCars]
GO
ALTER TABLE [dbo].[CarSales]  WITH NOCHECK ADD  CONSTRAINT [FK_CarSales_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[CarSales] CHECK CONSTRAINT [FK_CarSales_Employees]
GO
ALTER TABLE [dbo].[CarSales]  WITH NOCHECK ADD  CONSTRAINT [FK_CarSales_PaymentMethods] FOREIGN KEY([PaymentMethodId])
REFERENCES [dbo].[PaymentMethods] ([Id])
GO
ALTER TABLE [dbo].[CarSales] CHECK CONSTRAINT [FK_CarSales_PaymentMethods]
GO
ALTER TABLE [dbo].[CarSales]  WITH NOCHECK ADD  CONSTRAINT [FK_CarSales_SaleStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[SaleStatus] ([Id])
GO
ALTER TABLE [dbo].[CarSales] CHECK CONSTRAINT [FK_CarSales_SaleStatus]
GO
ALTER TABLE [dbo].[Cities]  WITH NOCHECK ADD  CONSTRAINT [FK_Cities_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_Countries]
GO
ALTER TABLE [dbo].[DealershipCars]  WITH NOCHECK ADD  CONSTRAINT [FK_DealershipCars_AutoDealerships] FOREIGN KEY([DealershipId])
REFERENCES [dbo].[AutoDealerships] ([Id])
GO
ALTER TABLE [dbo].[DealershipCars] CHECK CONSTRAINT [FK_DealershipCars_AutoDealerships]
GO
ALTER TABLE [dbo].[DealershipCars]  WITH NOCHECK ADD  CONSTRAINT [FK_DealershipCars_DealershipCarStatuses] FOREIGN KEY([CarStatusId])
REFERENCES [dbo].[DealershipCarStatuses] ([Id])
GO
ALTER TABLE [dbo].[DealershipCars] CHECK CONSTRAINT [FK_DealershipCars_DealershipCarStatuses]
GO
ALTER TABLE [dbo].[Employees]  WITH NOCHECK ADD  CONSTRAINT [FK_Employees_AutoDealerships] FOREIGN KEY([DealershipId])
REFERENCES [dbo].[AutoDealerships] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_AutoDealerships]
GO
ALTER TABLE [dbo].[Engines]  WITH NOCHECK ADD  CONSTRAINT [FK_Engines_Brands] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brands] ([Id])
GO
ALTER TABLE [dbo].[Engines] CHECK CONSTRAINT [FK_Engines_Brands]
GO
ALTER TABLE [dbo].[Engines]  WITH NOCHECK ADD  CONSTRAINT [FK_Engines_EngineTypes] FOREIGN KEY([EngineTypeId])
REFERENCES [dbo].[EngineTypes] ([Id])
GO
ALTER TABLE [dbo].[Engines] CHECK CONSTRAINT [FK_Engines_EngineTypes]
GO
ALTER TABLE [dbo].[LeaseProposalConditions]  WITH NOCHECK ADD  CONSTRAINT [FK_LeaseProposalConditions_Conditions1] FOREIGN KEY([ConditionId])
REFERENCES [dbo].[Conditions] ([Id])
GO
ALTER TABLE [dbo].[LeaseProposalConditions] CHECK CONSTRAINT [FK_LeaseProposalConditions_Conditions1]
GO
ALTER TABLE [dbo].[LeaseProposalConditions]  WITH NOCHECK ADD  CONSTRAINT [FK_LeaseProposalConditions_LeaseProposals] FOREIGN KEY([LeaseProposalId])
REFERENCES [dbo].[LeaseProposals] ([Id])
GO
ALTER TABLE [dbo].[LeaseProposalConditions] CHECK CONSTRAINT [FK_LeaseProposalConditions_LeaseProposals]
GO
ALTER TABLE [dbo].[LeaseProposals]  WITH NOCHECK ADD  CONSTRAINT [FK_LeaseProposals_LeaseTypes] FOREIGN KEY([LeaseTypeId])
REFERENCES [dbo].[LeaseTypes] ([Id])
GO
ALTER TABLE [dbo].[LeaseProposals] CHECK CONSTRAINT [FK_LeaseProposals_LeaseTypes]
GO
ALTER TABLE [dbo].[Leases]  WITH NOCHECK ADD  CONSTRAINT [FK__Lease__CustomerI__489AC854] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Leases] CHECK CONSTRAINT [FK__Lease__CustomerI__489AC854]
GO
ALTER TABLE [dbo].[Leases]  WITH NOCHECK ADD  CONSTRAINT [FK_Leases_DealershipCars] FOREIGN KEY([DealershipCarId])
REFERENCES [dbo].[DealershipCars] ([Id])
GO
ALTER TABLE [dbo].[Leases] CHECK CONSTRAINT [FK_Leases_DealershipCars]
GO
ALTER TABLE [dbo].[Leases]  WITH NOCHECK ADD  CONSTRAINT [FK_Leases_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[Leases] CHECK CONSTRAINT [FK_Leases_Employees]
GO
ALTER TABLE [dbo].[Leases]  WITH NOCHECK ADD  CONSTRAINT [FK_Leases_LeaseProposals] FOREIGN KEY([ProposalId])
REFERENCES [dbo].[LeaseProposals] ([Id])
GO
ALTER TABLE [dbo].[Leases] CHECK CONSTRAINT [FK_Leases_LeaseProposals]
GO
USE [master]
GO
ALTER DATABASE [AutoDealership] SET  READ_WRITE 
GO
