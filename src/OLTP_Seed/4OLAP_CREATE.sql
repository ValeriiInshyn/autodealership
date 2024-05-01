USE [master]
GO
/****** Object:  Database [AutoDealershipOLAP]    Script Date: 5/1/2024 11:54:02 PM ******/
CREATE DATABASE [AutoDealershipOLAP]
GO
USE [AutoDealershipOLAP]
GO
CREATE TABLE [dbo].[AutoDealershipsDim](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[bk_dealership_id] [int] NOT NULL,
 CONSTRAINT [PK_AutoDealershipsDim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BrandsDim]    Script Date: 5/1/2024 11:54:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrandsDim](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[bk_brand_id] [int] NOT NULL,
 CONSTRAINT [PK_BrandsDim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarSalesFact]    Script Date: 5/1/2024 11:54:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarSalesFact](
	[Id] [int] NOT NULL,
	[AutoDealershipId] [int] NOT NULL,
	[BrandId] [int] NOT NULL,
	[TotalIncomeLastMonth] [decimal](10, 2) NOT NULL,
	[StartDateId] [int] NOT NULL,
	[EndDateId] [int] NOT NULL,
	[TotalIncomeForCurrentMonth] [decimal](10, 2) NOT NULL,
	[MonthTotalIncomeModifyPercent] [int] NOT NULL,
	[SalesCountForLastMonth] [int] NOT NULL,
	[SalesCountForCurrentMonth] [int] NOT NULL,
	[SalesCountChangeForMonth] [int] NOT NULL,
 CONSTRAINT [PK_CarSalesFact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarsDim]    Script Date: 5/1/2024 11:54:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarsDim](
	[Id] [int] NOT NULL,
	[Model] [nvarchar](100) NOT NULL,
	[BrandId] [int] NOT NULL,
	[Generation] [nvarchar](100) NOT NULL,
	[bk_car_id] [int] NOT NULL,
 CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DatesDim]    Script Date: 5/1/2024 11:54:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatesDim](
	[Id] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Day] [int] NOT NULL,
 CONSTRAINT [PK_Dates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeasesFact]    Script Date: 5/1/2024 11:54:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeasesFact](
	[Id] [int] NOT NULL,
	[CarId] [int] NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[PreviousLeaseModifyPercent] [int] NOT NULL,
	[LeaseSignDateId] [int] NOT NULL,
	[LeaseStartDateId] [int] NOT NULL,
	[LeaseEndDateId] [int] NOT NULL,
	[LastLeaseId] [int] NULL,
 CONSTRAINT [PK_Leases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CarSalesFact]  WITH CHECK ADD  CONSTRAINT [FK_CarSalesFact_AutoDealershipsDim] FOREIGN KEY([AutoDealershipId])
REFERENCES [dbo].[AutoDealershipsDim] ([Id])
GO
ALTER TABLE [dbo].[CarSalesFact] CHECK CONSTRAINT [FK_CarSalesFact_AutoDealershipsDim]
GO
ALTER TABLE [dbo].[CarSalesFact]  WITH CHECK ADD  CONSTRAINT [FK_CarSalesFact_BrandsDim] FOREIGN KEY([BrandId])
REFERENCES [dbo].[BrandsDim] ([Id])
GO
ALTER TABLE [dbo].[CarSalesFact] CHECK CONSTRAINT [FK_CarSalesFact_BrandsDim]
GO
ALTER TABLE [dbo].[CarSalesFact]  WITH CHECK ADD  CONSTRAINT [FK_CarSalesFact_Dates2] FOREIGN KEY([EndDateId])
REFERENCES [dbo].[DatesDim] ([Id])
GO
ALTER TABLE [dbo].[CarSalesFact] CHECK CONSTRAINT [FK_CarSalesFact_Dates2]
GO
ALTER TABLE [dbo].[CarSalesFact]  WITH CHECK ADD  CONSTRAINT [FK_CarSalesFact_Dates3] FOREIGN KEY([StartDateId])
REFERENCES [dbo].[DatesDim] ([Id])
GO
ALTER TABLE [dbo].[CarSalesFact] CHECK CONSTRAINT [FK_CarSalesFact_Dates3]
GO
ALTER TABLE [dbo].[CarsDim]  WITH CHECK ADD  CONSTRAINT [FK_Cars_BrandsDim] FOREIGN KEY([BrandId])
REFERENCES [dbo].[BrandsDim] ([Id])
GO
ALTER TABLE [dbo].[CarsDim] CHECK CONSTRAINT [FK_Cars_BrandsDim]
GO
ALTER TABLE [dbo].[LeasesFact]  WITH CHECK ADD  CONSTRAINT [FK_Leases_Cars] FOREIGN KEY([CarId])
REFERENCES [dbo].[CarsDim] ([Id])
GO
ALTER TABLE [dbo].[LeasesFact] CHECK CONSTRAINT [FK_Leases_Cars]
GO
ALTER TABLE [dbo].[LeasesFact]  WITH CHECK ADD  CONSTRAINT [FK_Leases_Dates1] FOREIGN KEY([LeaseSignDateId])
REFERENCES [dbo].[DatesDim] ([Id])
GO
ALTER TABLE [dbo].[LeasesFact] CHECK CONSTRAINT [FK_Leases_Dates1]
GO
ALTER TABLE [dbo].[LeasesFact]  WITH CHECK ADD  CONSTRAINT [FK_Leases_Dates2] FOREIGN KEY([LeaseStartDateId])
REFERENCES [dbo].[DatesDim] ([Id])
GO
ALTER TABLE [dbo].[LeasesFact] CHECK CONSTRAINT [FK_Leases_Dates2]
GO
ALTER TABLE [dbo].[LeasesFact]  WITH CHECK ADD  CONSTRAINT [FK_Leases_Dates3] FOREIGN KEY([LeaseEndDateId])
REFERENCES [dbo].[DatesDim] ([Id])
GO
ALTER TABLE [dbo].[LeasesFact] CHECK CONSTRAINT [FK_Leases_Dates3]
GO