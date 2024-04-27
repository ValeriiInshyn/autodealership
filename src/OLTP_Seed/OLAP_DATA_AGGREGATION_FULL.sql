-- Create database
USE [master]
GO
/****** Object:  Database [AutoDealershipOLAPTmp]    Script Date: 4/19/2024 3:21:05 PM ******/
CREATE DATABASE [AutoDealershipOLAPTmp]
GO
USE [AutoDealershipOLAPTmp]
GO
/****** Object:  Table [dbo].[AutoDealerships]    Script Date: 4/25/2024 10:24:11 PM ******/
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
/****** Object:  Table [dbo].[Brands]    Script Date: 4/25/2024 10:24:11 PM ******/
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
/****** Object:  Table [dbo].[Cars]    Script Date: 4/25/2024 10:24:11 PM ******/
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
/****** Object:  Table [dbo].[CarSales]    Script Date: 4/25/2024 10:24:11 PM ******/
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
/****** Object:  Table [dbo].[Dates]    Script Date: 4/25/2024 10:24:11 PM ******/
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
/****** Object:  Table [dbo].[Leases]    Script Date: 4/25/2024 10:24:11 PM ******/
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
USE [master]
GO
ALTER DATABASE [AutoDealershipOLAPTmp] SET  READ_WRITE 
GO

DECLARE @CurrentMaxId INT;
--Car brands
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.Brands;

INSERT INTO AutoDealershipOLAP.dbo.Brands(
    Id,
    Name
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    b.Name
FROM 
    AutoDealership.dbo.Brands b
GROUP BY b.Name
    
INSERT INTO AutoDealershipOLAP.dbo.Brands
SELECT * FROM AutoDealershipOLAP.dbo.Brands;


-- Cars
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAPTmp.dbo.Cars;

INSERT INTO AutoDealershipOLAPTmp.dbo.Cars(
    Id,
    Model,
    BrandId,
    Generation
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    ISNULL(ds.Model,'NULL'),
    (SELECT TOP 1 id FROM AutoDealershipOLAPTmp.dbo.Brands d WHERE d.Name=b.Name) AS BrandId,
    ds.Generation
FROM 
    AutoDealership.dbo.Cars ds JOIN
    AutoDealership.dbo.Brands b on b.Id=ds.BrandId

INSERT INTO AutoDealershipOLAP.dbo.Cars
SELECT * FROM AutoDealershipOLAPTmp.dbo.Cars;

--Dates
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAPTmp.dbo.Dates;

INSERT INTO AutoDealershipOLAPTmp.dbo.Dates(
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

INSERT INTO AutoDealershipOLAP.dbo.Dates
SELECT * FROM AutoDealershipOLAPTmp.dbo.Dates;
--Dealerships
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAPTmp.dbo.AutoDealerships;

INSERT INTO AutoDealershipOLAPTmp.dbo.AutoDealerships(
    Id,
    Name
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    ds.Name
FROM 
    AutoDealership.dbo.AutoDealerships ds

INSERT INTO AutoDealershipOLAP.dbo.AutoDealerships
SELECT * FROM AutoDealershipOLAPTmp.dbo.AutoDealerships;
--Leases
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAPTmp.dbo.Leases;
DECLARE @iterator INT;
SET @iterator = 0;
-- Insert data into the Leases table
INSERT INTO AutoDealershipOLAPTmp.dbo.Leases (
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
(SELECT TOP 1 id FROM AutoDealershipOLAPTmp.dbo.Dates d WHERE d.Year=YEAR(LeaseSignDate) AND d.Month=MONTH(LeaseSignDate) AND d.[Day]=DAY(LeaseSignDate)) as LeaseSignDateId,
(SELECT TOP 1 id FROM AutoDealershipOLAPTmp.dbo.Dates d WHERE d.Year=YEAR(LeaseStartDate) AND d.Month=MONTH(LeaseStartDate) AND d.[Day]=DAY(LeaseStartDate)) as LeaseStartDateId,
(SELECT TOP 1 id FROM AutoDealershipOLAPTmp.dbo.Dates d WHERE d.Year=YEAR(LeaseEndDate) AND d.Month=MONTH(LeaseEndDate) AND d.[Day]=DAY(LeaseEndDate)) as LeaseEndDateId,
CASE 
    WHEN ISNULL(TotalPrice - (LAG(TotalPrice, 1) OVER (PARTITION BY DealershipCarId ORDER BY LeaseSignDate)),0) = 0 THEN NULL
        ELSE @iterator
    END AS LastLeaseId
FROM
AutoDealership.dbo.Leases
GROUP BY 
LeaseSignDate,
DealershipCarId,
TotalPrice,
LeaseEndDate,
LeaseStartDate
ORDER BY
DealershipCarId,LeaseSignDate

INSERT INTO AutoDealershipOLAP.dbo.Leases
SELECT * FROM AutoDealershipOLAPTmp.dbo.Leases;
-- Sales
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAPTmp.dbo.CarSales;

INSERT INTO AutoDealershipOLAPTmp.dbo.CarSales (
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
    ds.Id,
    c.BrandId,
    SUM(CASE WHEN cs.SaleDate BETWEEN DATEADD(MONTH, -2, GETDATE()) AND DATEADD(MONTH, -1, GETDATE()) THEN c.Price ELSE 0 END) AS TotalIncomeLastMonth,
    -- Assuming StartDateId and EndDateId refer to monthly period identifiers
    (YEAR(GETDATE()) * 100 + MONTH(GETDATE()) - 1) AS StartDateId,
    (YEAR(GETDATE()) * 100 + MONTH(GETDATE())) AS EndDateId,
    SUM(CASE WHEN cs.SaleDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE() THEN c.Price ELSE 0 END) AS TotalIncomeForCurrentMonth,
    -- Calculating percent change
    (SUM(CASE WHEN cs.SaleDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE() THEN c.Price ELSE 0 END) - 
    SUM(CASE WHEN cs.SaleDate BETWEEN DATEADD(MONTH, -2, GETDATE()) AND DATEADD(MONTH, -1, GETDATE()) THEN c.Price ELSE 0 END)) /
    ISNULL(
        (SUM(CASE WHEN cs.SaleDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE() THEN c.Price ELSE 0 END) - 
         SUM(CASE WHEN cs.SaleDate BETWEEN DATEADD(MONTH, -2, GETDATE()) AND DATEADD(MONTH, -1, GETDATE()) THEN c.Price ELSE 0 END)) / 
        NULLIF((SUM(CASE WHEN cs.SaleDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE() THEN c.Price ELSE 0 END) + 
                SUM(CASE WHEN cs.SaleDate BETWEEN DATEADD(MONTH, -2, GETDATE()) AND DATEADD(MONTH, -1, GETDATE()) THEN c.Price ELSE 0 END)) / 2, 0)
        * 100,
        0
    ) AS MonthTotalIncomeModifyPercent,
    COUNT(CASE WHEN cs.SaleDate BETWEEN DATEADD(MONTH, -2, GETDATE()) AND DATEADD(MONTH, -1, GETDATE()) THEN 1 ELSE NULL END) AS SalesCountForLastMonth,
    COUNT(CASE WHEN cs.SaleDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE() THEN 1 ELSE NULL END) AS SalesCountForCurrentMonth,
    COUNT(CASE WHEN cs.SaleDate BETWEEN DATEADD(MONTH, -1, GETDATE()) AND GETDATE() THEN 1 ELSE NULL END) -
    COUNT(CASE WHEN cs.SaleDate BETWEEN DATEADD(MONTH, -2, GETDATE()) AND DATEADD(MONTH, -1, GETDATE()) THEN 1 ELSE NULL END) AS SalesCountChangeForMonth
FROM
    AutoDealership.dbo.AutoDealerships ds
    JOIN AutoDealership.dbo.DealershipCars dc ON dc.DealershipId = ds.Id
    JOIN AutoDealership.dbo.CarSales cs ON cs.DealershipCarId = dc.CarId
    JOIN AutoDealership.dbo.Cars c ON dc.CarId = c.Id
WHERE
    cs.SaleDate >= DATEADD(MONTH, -2, GETDATE())
GROUP BY
    ds.Id,
    c.BrandId;


INSERT INTO AutoDealershipOLAP.dbo.CarSales
SELECT * FROM AutoDealershipOLAPTmp.dbo.CarSales;
-- Drop Database
USE [master]
GO
ALTER DATABASE [AutoDealershipOLAPTmp] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
/****** Object:  Database [AutoDealershipOLAPTmp]    Script Date: 4/26/2024 10:15:17 PM ******/
DROP DATABASE [AutoDealershipOLAPTmp]
GO
EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'AutoDealershipOLAPTmp'
GO
