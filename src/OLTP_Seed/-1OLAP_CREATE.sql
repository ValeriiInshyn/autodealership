USE [master]
GO
/****** Object:  Database [AutoDealershipOLAP]    Script Date: 4/19/2024 3:21:05 PM ******/
CREATE DATABASE [AutoDealershipOLAP]
GO
USE [AutoDealershipOLAP]
GO
/****** Object:  Table [dbo].[AutoDealerships]    Script Date: 4/25/2024 10:37:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AutoDealerships](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_AutoDealerships] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 4/25/2024 10:37:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 4/25/2024 10:37:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[Id] [int] NOT NULL,
	[Model] [nvarchar](100) NOT NULL,
	[BrandId] [int] NOT NULL,
	[Generation] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarSales]    Script Date: 4/25/2024 10:37:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarSales](
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
 CONSTRAINT [PK_CarSales] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dates]    Script Date: 4/25/2024 10:37:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dates](
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
/****** Object:  Table [dbo].[Leases]    Script Date: 4/25/2024 10:37:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leases](
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
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [FK_Cars_Brands] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brands] ([Id])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_Brands]
GO
ALTER TABLE [dbo].[CarSales]  WITH CHECK ADD  CONSTRAINT [FK_CarSales_AutoDealerships] FOREIGN KEY([AutoDealershipId])
REFERENCES [dbo].[AutoDealerships] ([Id])
GO
ALTER TABLE [dbo].[CarSales] CHECK CONSTRAINT [FK_CarSales_AutoDealerships]
GO
ALTER TABLE [dbo].[CarSales]  WITH CHECK ADD  CONSTRAINT [FK_CarSales_Brands] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brands] ([Id])
GO
ALTER TABLE [dbo].[CarSales] CHECK CONSTRAINT [FK_CarSales_Brands]
GO
ALTER TABLE [dbo].[CarSales]  WITH CHECK ADD  CONSTRAINT [FK_CarSales_Dates2] FOREIGN KEY([EndDateId])
REFERENCES [dbo].[Dates] ([Id])
GO
ALTER TABLE [dbo].[CarSales] CHECK CONSTRAINT [FK_CarSales_Dates2]
GO
ALTER TABLE [dbo].[CarSales]  WITH CHECK ADD  CONSTRAINT [FK_CarSales_Dates3] FOREIGN KEY([StartDateId])
REFERENCES [dbo].[Dates] ([Id])
GO
ALTER TABLE [dbo].[CarSales] CHECK CONSTRAINT [FK_CarSales_Dates3]
GO
ALTER TABLE [dbo].[Leases]  WITH CHECK ADD  CONSTRAINT [FK_Leases_Cars] FOREIGN KEY([CarId])
REFERENCES [dbo].[Cars] ([Id])
GO
ALTER TABLE [dbo].[Leases] CHECK CONSTRAINT [FK_Leases_Cars]
GO
ALTER TABLE [dbo].[Leases]  WITH CHECK ADD  CONSTRAINT [FK_Leases_Dates1] FOREIGN KEY([LeaseSignDateId])
REFERENCES [dbo].[Dates] ([Id])
GO
ALTER TABLE [dbo].[Leases] CHECK CONSTRAINT [FK_Leases_Dates1]
GO
ALTER TABLE [dbo].[Leases]  WITH CHECK ADD  CONSTRAINT [FK_Leases_Dates2] FOREIGN KEY([LeaseStartDateId])
REFERENCES [dbo].[Dates] ([Id])
GO
ALTER TABLE [dbo].[Leases] CHECK CONSTRAINT [FK_Leases_Dates2]
GO
ALTER TABLE [dbo].[Leases]  WITH CHECK ADD  CONSTRAINT [FK_Leases_Dates3] FOREIGN KEY([LeaseEndDateId])
REFERENCES [dbo].[Dates] ([Id])
GO
ALTER TABLE [dbo].[Leases] CHECK CONSTRAINT [FK_Leases_Dates3]
GO
USE [master]
GO
ALTER DATABASE [AutoDealershipOLAP] SET  READ_WRITE 
GO
