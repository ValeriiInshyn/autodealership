-- Create database
USE [master]
GO
/****** Object:  Database [AutoDealershipStaging]    Script Date: 4/29/2024 11:04:05 PM ******/
CREATE DATABASE [AutoDealershipStaging]
GO
USE [AutoDealershipStaging]
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
/****** Object:  Table [dbo].[Brands]    Script Date: 4/29/2024 11:04:05 PM ******/
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
/****** Object:  Table [dbo].[Cars]    Script Date: 4/29/2024 11:04:05 PM ******/
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
/****** Object:  Table [dbo].[CarSales]    Script Date: 4/29/2024 11:04:05 PM ******/
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
/****** Object:  Table [dbo].[DealershipCars]    Script Date: 4/29/2024 11:04:05 PM ******/
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
/****** Object:  Table [dbo].[Leases]    Script Date: 4/29/2024 11:04:05 PM ******/
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
USE [master]
GO
ALTER DATABASE [AutoDealershipStaging] SET  READ_WRITE 
GO


DECLARE @CurrentMaxId INT;
--Car brands
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.Brands;

INSERT INTO AutoDealershipStaging.dbo.Brands
SELECT * FROM AutoDealership.dbo.Brands;

INSERT INTO AutoDealershipOLAP.dbo.Brands(
    Id,
    Name
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    b.Name
FROM 
    AutoDealershipStaging.dbo.Brands b
GROUP BY b.Name




-- Cars
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.Cars;

SET IDENTITY_INSERT AutoDealershipStaging.dbo.Cars ON;
INSERT INTO AutoDealershipStaging.dbo.Cars
([Id]
      ,[CarTypeId]
      ,[Doors]
      ,[Seats]
      ,[Year]
      ,[Price]
      ,[ColorId]
      ,[BodyTypeId]
      ,[EngineId]
      ,[BrandId]
      ,[Model]
      ,[Generation]
      ,[Weight]
      ,[MaxSpeed]
      ,[GearsCount]
      ,[Height]
      ,[Width]
      ,[Length]
      ,[GearBoxId]
      ,[FuelTankCapacity]
      ,[WheelsCount]
      ,[CreateDate]
      ,[UpdateDate])
SELECT [Id]
      ,[CarTypeId]
      ,[Doors]
      ,[Seats]
      ,[Year]
      ,[Price]
      ,[ColorId]
      ,[BodyTypeId]
      ,[EngineId]
      ,[BrandId]
      ,[Model]
      ,[Generation]
      ,[Weight]
      ,[MaxSpeed]
      ,[GearsCount]
      ,[Height]
      ,[Width]
      ,[Length]
      ,[GearBoxId]
      ,[FuelTankCapacity]
      ,[WheelsCount]
      ,[CreateDate]
      ,[UpdateDate] FROM AutoDealership.dbo.Cars;
SET IDENTITY_INSERT AutoDealershipStaging.dbo.Cars OFF;


INSERT INTO AutoDealershipOLAP.dbo.Cars(
    Id,
    Model,
    BrandId,
    Generation
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    ISNULL(ds.Model,'NULL'),
    (SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.Brands d WHERE d.Name=b.Name) AS BrandId,
    ds.Generation
FROM 
    AutoDealershipStaging.dbo.Cars ds JOIN
    AutoDealershipStaging.dbo.Brands b on b.Id=ds.BrandId

--Dates
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.Dates;

INSERT INTO AutoDealershipOLAP.dbo.Dates(
    Id,
    [Year],
    [Month],
    [Day]
)

SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    YEAR(CombinedDates.Date) AS [Year],
    MONTH(CombinedDates.Date) AS [Month],
    DAY(CombinedDates.Date) AS [Day]
FROM (

    SELECT LeaseSignDate AS Date FROM AutoDealership.dbo.Leases
    UNION
    SELECT LeaseStartDate AS Date FROM AutoDealership.dbo.Leases
    UNION
    SELECT LeaseEndDate AS Date FROM AutoDealership.dbo.Leases
    UNION
  SELECT Date FROM (
        SELECT FirstDayOfMonth AS Date 
        FROM (
            SELECT 
                DATEADD(MONTH, v.number, DATEADD(DAY, 1 - DAY(MinDate), MinDate)) AS FirstDayOfMonth,
                EOMONTH(DATEADD(MONTH, v.number, DATEADD(DAY, 1 - DAY(MinDate), MinDate))) AS LastDayOfMonth
            FROM (
                SELECT
                    MIN(CAST(SaleDate AS DATE)) AS MinDate,
                    MAX(CAST(SaleDate AS DATE)) AS MaxDate
                FROM AutoDealership.dbo.CarSales
            ) AS DateRange
            CROSS JOIN master.dbo.spt_values v
            WHERE
                v.type = 'P'
                AND v.number <= DATEDIFF(MONTH, MinDate, MaxDate)
        ) AS Months
        UNION
        SELECT LastDayOfMonth AS Date
        FROM (
            SELECT 
                DATEADD(MONTH, v.number, DATEADD(DAY, 1 - DAY(MinDate), MinDate)) AS FirstDayOfMonth,
                EOMONTH(DATEADD(MONTH, v.number, DATEADD(DAY, 1 - DAY(MinDate), MinDate))) AS LastDayOfMonth
            FROM (
                SELECT
                    MIN(CAST(SaleDate AS DATE)) AS MinDate,
                    MAX(CAST(SaleDate AS DATE)) AS MaxDate
                FROM AutoDealership.dbo.CarSales
            ) AS DateRange
            CROSS JOIN master.dbo.spt_values v
            WHERE
                v.type = 'P'
                AND v.number <= DATEDIFF(MONTH, MinDate, MaxDate)
        ) AS Months
    ) AS MonthFirstLastDays
) AS CombinedDates;


--Dealerships
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.AutoDealerships;

INSERT INTO AutoDealershipStaging.dbo.AutoDealerships
SELECT * FROM AutoDealership.dbo.AutoDealerships;

INSERT INTO AutoDealershipOLAP.dbo.AutoDealerships(
    Id,
    Name
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    ds.Name
FROM 
    AutoDealershipStaging.dbo.AutoDealerships ds


--Leases
INSERT INTO AutoDealershipStaging.dbo.Leases
([Id]
      ,[EmployeeId]
      ,[ProposalId]
      ,[CustomerId]
      ,[DealershipCarId]
      ,[LeaseSignDate]
      ,[LeaseStartDate]
      ,[LeaseEndDate]
      ,[LeaseUniqueNumber]
      ,[TotalPrice]
      ,[Description]
      ,[CreateDate]
      ,[UpdateDate])
SELECT [Id]
      ,[EmployeeId]
      ,[ProposalId]
      ,[CustomerId]
      ,[DealershipCarId]
      ,[LeaseSignDate]
      ,[LeaseStartDate]
      ,[LeaseEndDate]
      ,[LeaseUniqueNumber]
      ,[TotalPrice]
      ,[Description]
      ,[CreateDate]
      ,[UpdateDate] FROM AutoDealership.dbo.Leases;
SET IDENTITY_INSERT AutoDealershipStaging.dbo.Leases OFF
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.Leases;
DECLARE @iterator INT;
SET @iterator = 0;
-- Insert data into the Leases table
INSERT INTO AutoDealershipOLAP.dbo.Leases (
    Id, 
    CarId, 
    Price, 
    PreviousLeaseModifyPercent, 
    LeaseSignDateId, 
    LeaseStartDateId, 
    LeaseEndDateId, 
    LastLeaseId
)
SELECT 
@CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT LeaseSignDate)) AS Id,
DealershipCarId as CarId,
ISNULL(TotalPrice,0) AS Price,
ISNULL(TotalPrice/(LAG(TotalPrice, 1) OVER (PARTITION BY DealershipCarId ORDER BY LeaseSignDate))*100-100,0) as PreviousLeaseModifyPercent, 
(SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.Dates d WHERE d.Year=YEAR(LeaseSignDate) AND d.Month=MONTH(LeaseSignDate) AND d.[Day]=DAY(LeaseSignDate)) as LeaseSignDateId,
(SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.Dates d WHERE d.Year=YEAR(LeaseStartDate) AND d.Month=MONTH(LeaseStartDate) AND d.[Day]=DAY(LeaseStartDate)) as LeaseStartDateId,
(SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.Dates d WHERE d.Year=YEAR(LeaseEndDate) AND d.Month=MONTH(LeaseEndDate) AND d.[Day]=DAY(LeaseEndDate)) as LeaseEndDateId,
CASE 
    WHEN ISNULL(TotalPrice - (LAG(TotalPrice, 1) OVER (PARTITION BY DealershipCarId ORDER BY LeaseSignDate)),0) = 0 THEN NULL
        ELSE @iterator
    END AS LastLeaseId
FROM
AutoDealershipStaging.dbo.Leases
GROUP BY 
LeaseSignDate,
DealershipCarId,
TotalPrice,
LeaseEndDate,
LeaseStartDate
ORDER BY
DealershipCarId,LeaseSignDate
-- Sales
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.CarSales;

SET IDENTITY_INSERT AutoDealershipStaging.dbo.CarSales ON
INSERT INTO AutoDealershipStaging.dbo.CarSales([Id]
      ,[DealershipCarId]
      ,[CustomerId]
      ,[SaleDate]
      ,[StatusId]
      ,[ExpectedDeliveryDate]
      ,[EmployeeId]
      ,[PaymentMethodId]
      ,[CreateDate]
      ,[UpdateDate])
SELECT [Id]
      ,[DealershipCarId]
      ,[CustomerId]
      ,[SaleDate]
      ,[StatusId]
      ,[ExpectedDeliveryDate]
      ,[EmployeeId]
      ,[PaymentMethodId]
      ,[CreateDate]
      ,[UpdateDate] FROM AutoDealership.dbo.CarSales;
	  SET IDENTITY_INSERT AutoDealershipStaging.dbo.CarSales OFF
;WITH SalesData AS (
    SELECT 
        ds.Id AS DealershipId,
        c.BrandId,
        FORMAT(cs.SaleDate, 'yyyy-MM') as SaleDate,
        SUM(c.Price) AS TotalPrice,
        COUNT(c.Price) as SalesCount
    FROM
        AutoDealershipStaging.dbo.AutoDealerships ds
        JOIN AutoDealershipStaging.dbo.DealershipCars dc ON dc.DealershipId = ds.Id
        JOIN AutoDealershipStaging.dbo.CarSales cs ON cs.DealershipCarId = dc.CarId
        JOIN AutoDealershipStaging.dbo.Cars c ON dc.CarId = c.Id
    GROUP BY
        ds.Id,
        c.BrandId,
        FORMAT(cs.SaleDate, 'yyyy-MM')
        
),

RankedSalesData AS (
    SELECT
        DealershipId,
        BrandId,
        CONVERT(DATE, SaleDate + '-01') AS SaleDate,
        TotalPrice as TotalIncomeForCurrentMonth,
        ISNULL(LAG(TotalPrice, 1) OVER (PARTITION BY DealershipId, BrandId ORDER BY SaleDate), 0) AS TotalIncomeLastMonth,
        ISNULL(LAG(SalesCount, 1) OVER (PARTITION BY DealershipId, BrandId ORDER BY SaleDate), 0) AS SalesCountForLastMonth,
        SalesCount AS SalesCountForCurrentMonth
    FROM
        SalesData
)
INSERT INTO AutoDealershipOLAP.dbo.CarSales (
    Id,
    AutoDealershipId,
    BrandId,
    TotalIncomeLastMonth,
    StartDateId,
    EndDateId,
    TotalIncomeForCurrentMonth,
    MonthTotalIncomeModifyPercent,
    SalesCountForLastMonth,
    SalesCountForCurrentMonth,
    SalesCountChangeForMonth
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    DealershipId,
    BrandId,
    TotalIncomeLastMonth,
    (SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.Dates d WHERE d.Year=YEAR(SaleDate) AND d.Month=MONTH(SaleDate) AND d.[Day]=1) AS StartDateId,
    (SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.Dates d WHERE d.Year=YEAR(SaleDate) AND d.Month=MONTH(SaleDate) AND d.[Day]=DAY(EOMONTH(SaleDate))) AS EndDateId,
    TotalIncomeForCurrentMonth,
    (TotalIncomeForCurrentMonth - TotalIncomeLastMonth) / ((TotalIncomeForCurrentMonth + TotalIncomeLastMonth) /2 ) * 100 - 100 AS MonthTotalIncomeModifyPercent,
    SalesCountForLastMonth,
    SalesCountForCurrentMonth,
    SalesCountForCurrentMonth - SalesCountForLastMonth AS SalesCountChangeForMonth
FROM
    RankedSalesData
GROUP BY
    DealershipId,
    BrandId,
    SaleDate,
    TotalIncomeLastMonth,
    TotalIncomeForCurrentMonth,
    SalesCountForCurrentMonth,
    SalesCountForLastMonth

-- Drop Database
USE [master]
GO
ALTER DATABASE [AutoDealershipStaging] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
/****** Object:  Database [AutoDealershipStaging]    Script Date: 4/26/2024 10:15:17 PM ******/
DROP DATABASE [AutoDealershipStaging]
GO
EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'AutoDealershipStaging'
GO
