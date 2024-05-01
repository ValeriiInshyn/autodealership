USE [master]
GO
/****** Object:  Database [AutoDealershipOLAP]    Script Date: 4/30/2024 1:43:57 PM ******/
CREATE DATABASE [AutoDealershipOLAP]
GO
USE [AutoDealershipOLAP]
GO
/****** Object:  Table [dbo].[AutoDealershipsDim]    Script Date: 4/30/2024 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AutoDealershipsDim](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_AutoDealershipsDim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BrandsDim]    Script Date: 4/30/2024 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrandsDim](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_BrandsDim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarsDim]    Script Date: 4/30/2024 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarsDim](
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
/****** Object:  Table [dbo].[CarSalesFact]    Script Date: 4/30/2024 1:43:57 PM ******/
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
/****** Object:  Table [dbo].[DatesDim]    Script Date: 4/30/2024 1:43:57 PM ******/
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
/****** Object:  Table [dbo].[LeasesFact]    Script Date: 4/30/2024 1:43:57 PM ******/
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
ALTER TABLE [dbo].[CarsDim]  WITH CHECK ADD  CONSTRAINT [FK_Cars_BrandsDim] FOREIGN KEY([BrandId])
REFERENCES [dbo].[BrandsDim] ([Id])
GO
ALTER TABLE [dbo].[CarsDim] CHECK CONSTRAINT [FK_Cars_BrandsDim]
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
/****** Object:  StoredProcedure [dbo].[UpdateFromMainFull]    Script Date: 4/30/2024 1:43:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateFromMainFull]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

TRUNCATE TABLE AutoDealershipsDimtaging.dbo.AutoDealershipsDim
TRUNCATE TABLE AutoDealershipsDimtaging.dbo.BrandsDim
TRUNCATE TABLE AutoDealershipsDimtaging.dbo.CarsDim
TRUNCATE TABLE AutoDealershipsDimtaging.dbo.CarSalesFact
TRUNCATE TABLE AutoDealershipsDimtaging.dbo.DealershipCars
TRUNCATE TABLE AutoDealershipsDimtaging.dbo.LeasesFact

DELETE FROM AutoDealershipOlap.dbo.CarSalesFact
DELETE FROM AutoDealershipOlap.dbo.LeasesFact
DELETE FROM AutoDealershipOlap.dbo.AutoDealershipsDim
DELETE FROM AutoDealershipOlap.dbo.CarsDim
DELETE FROM AutoDealershipOlap.dbo.DatesDim
DELETE FROM AutoDealershipOlap.dbo.BrandsDim



DECLARE @CurrentMaxId INT;
--Car BrandsDim
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.BrandsDim;

INSERT INTO AutoDealershipsDimtaging.dbo.BrandsDim
SELECT * FROM AutoDealership.dbo.BrandsDim;

INSERT INTO AutoDealershipOLAP.dbo.BrandsDim(
    Id,
    Name
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    b.Name
FROM 
    AutoDealershipsDimtaging.dbo.BrandsDim b
GROUP BY b.Name
-- CarsDim
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.CarsDim;

SET IDENTITY_INSERT AutoDealershipsDimtaging.dbo.CarsDim ON;
INSERT INTO AutoDealershipsDimtaging.dbo.CarsDim
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
      ,[UpdateDate] FROM AutoDealership.dbo.CarsDim;
SET IDENTITY_INSERT AutoDealershipsDimtaging.dbo.CarsDim OFF;


INSERT INTO AutoDealershipOLAP.dbo.CarsDim(
    Id,
    Model,
    BrandId,
    Generation
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    ISNULL(ds.Model,'NULL'),
    (SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.BrandsDim d WHERE d.Name=b.Name) AS BrandId,
    ds.Generation
FROM 
    AutoDealershipsDimtaging.dbo.CarsDim ds JOIN
    AutoDealershipsDimtaging.dbo.BrandsDim b on b.Id=ds.BrandId

--DatesDim
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.DatesDim;

INSERT INTO AutoDealershipOLAP.dbo.DatesDim(
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

    SELECT LeaseSignDate AS Date FROM AutoDealership.dbo.LeasesFact
    UNION
    SELECT LeaseStartDate AS Date FROM AutoDealership.dbo.LeasesFact
    UNION
    SELECT LeaseEndDate AS Date FROM AutoDealership.dbo.LeasesFact
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
                FROM AutoDealership.dbo.CarSalesFact
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
                FROM AutoDealership.dbo.CarSalesFact
            ) AS DateRange
            CROSS JOIN master.dbo.spt_values v
            WHERE
                v.type = 'P'
                AND v.number <= DATEDIFF(MONTH, MinDate, MaxDate)
        ) AS Months
    ) AS MonthFirstLastDays
) AS CombinedDates;


--Dealerships
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.AutoDealershipsDim;

INSERT INTO AutoDealershipsDimtaging.dbo.AutoDealershipsDim
SELECT * FROM AutoDealership.dbo.AutoDealershipsDim;

INSERT INTO AutoDealershipOLAP.dbo.AutoDealershipsDim(
    Id,
    Name
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    ds.Name
FROM 
    AutoDealershipsDimtaging.dbo.AutoDealershipsDim ds


--LeasesFact

SET IDENTITY_INSERT AutoDealershipsDimtaging.dbo.LeasesFact ON
INSERT INTO AutoDealershipsDimtaging.dbo.LeasesFact
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
      ,[UpdateDate] FROM AutoDealership.dbo.LeasesFact;
SET IDENTITY_INSERT AutoDealershipsDimtaging.dbo.LeasesFact OFF
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.LeasesFact;
DECLARE @iterator INT;
SET @iterator = 0;
-- Insert data into the LeasesFact table
INSERT INTO AutoDealershipOLAP.dbo.LeasesFact (
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
(SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.DatesDim d WHERE d.Year=YEAR(LeaseSignDate) AND d.Month=MONTH(LeaseSignDate) AND d.[Day]=DAY(LeaseSignDate)) as LeaseSignDateId,
(SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.DatesDim d WHERE d.Year=YEAR(LeaseStartDate) AND d.Month=MONTH(LeaseStartDate) AND d.[Day]=DAY(LeaseStartDate)) as LeaseStartDateId,
(SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.DatesDim d WHERE d.Year=YEAR(LeaseEndDate) AND d.Month=MONTH(LeaseEndDate) AND d.[Day]=DAY(LeaseEndDate)) as LeaseEndDateId,
CASE 
    WHEN ISNULL(TotalPrice - (LAG(TotalPrice, 1) OVER (PARTITION BY DealershipCarId ORDER BY LeaseSignDate)),0) = 0 THEN NULL
        ELSE @iterator
    END AS LastLeaseId
FROM
AutoDealershipsDimtaging.dbo.LeasesFact
GROUP BY 
LeaseSignDate,
DealershipCarId,
TotalPrice,
LeaseEndDate,
LeaseStartDate
ORDER BY
DealershipCarId,LeaseSignDate


-- Sales

INSERT INTO AutoDealershipsDimtaging.dbo.DealershipCars([Id]
      ,[CarId]
      ,[DealershipId]
      ,[CarsCount]
      ,[CarStatusId]
      ,[CreateDate]
      ,[UpdateDate])
SELECT [Id]
      ,[CarId]
      ,[DealershipId]
      ,[CarsCount]
      ,[CarStatusId]
      ,[CreateDate]
      ,[UpdateDate]
  FROM [AutoDealership].[dbo].[DealershipCars]

SET IDENTITY_INSERT AutoDealershipsDimtaging.dbo.CarSalesFact ON
INSERT INTO AutoDealershipsDimtaging.dbo.CarSalesFact([Id]
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
      ,[UpdateDate]
  FROM [AutoDealership].[dbo].[CarSalesFact]
SET IDENTITY_INSERT AutoDealershipsDimtaging.dbo.CarSalesFact OFF
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.CarSalesFact;
;WITH SalesData AS (
    SELECT 
        dc.DealershipId AS DealershipId,
        c.BrandId,
        FORMAT(cs.SaleDate, 'yyyy-MM') as SaleDate,
        SUM(c.Price) AS TotalPrice,
        COUNT(c.Price) as SalesCount
    FROM
        AutoDealershipsDimtaging.dbo.CarSalesFact cs 
        JOIN AutoDealershipsDimtaging.dbo.DealershipCars dc ON cs.DealershipCarId = dc.Id
        JOIN AutoDealershipsDimtaging.dbo.CarsDim c ON dc.CarId = c.Id
    GROUP BY
        dc.DealershipId,
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
INSERT INTO AutoDealershipOLAP.dbo.CarSalesFact (
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
    (SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.DatesDim d WHERE d.Year=YEAR(SaleDate) AND d.Month=MONTH(SaleDate) AND d.[Day]=1) AS StartDateId,
    (SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.DatesDim d WHERE d.Year=YEAR(SaleDate) AND d.Month=MONTH(SaleDate) AND d.[Day]=DAY(EOMONTH(SaleDate))) AS EndDateId,
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
END
GO
USE [master]
GO
ALTER DATABASE [AutoDealershipOLAP] SET  READ_WRITE 
GO
