TRUNCATE TABLE AutoDealershipStaging.dbo.AutoDealerships
TRUNCATE TABLE AutoDealershipStaging.dbo.Brands
TRUNCATE TABLE AutoDealershipStaging.dbo.Cars
TRUNCATE TABLE AutoDealershipStaging.dbo.CarSales
TRUNCATE TABLE AutoDealershipStaging.dbo.DealershipCars
TRUNCATE TABLE AutoDealershipStaging.dbo.Leases

DELETE FROM AutoDealershipOlap.dbo.CarSales
DELETE FROM AutoDealershipOlap.dbo.Leases
DELETE FROM AutoDealershipOlap.dbo.AutoDealerships
DELETE FROM AutoDealershipOlap.dbo.Cars
DELETE FROM AutoDealershipOlap.dbo.Dates
DELETE FROM AutoDealershipOlap.dbo.Brands



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

SET IDENTITY_INSERT AutoDealershipStaging.dbo.Leases ON
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

INSERT INTO AutoDealershipStaging.dbo.DealershipCars([Id]
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
      ,[UpdateDate]
  FROM [AutoDealership].[dbo].[CarSales]
SET IDENTITY_INSERT AutoDealershipStaging.dbo.CarSales OFF
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.CarSales;
;WITH SalesData AS (
    SELECT 
        dc.DealershipId AS DealershipId,
        c.BrandId,
        FORMAT(cs.SaleDate, 'yyyy-MM') as SaleDate,
        SUM(c.Price) AS TotalPrice,
        COUNT(c.Price) as SalesCount
    FROM
        AutoDealershipStaging.dbo.CarSales cs 
        JOIN AutoDealershipStaging.dbo.DealershipCars dc ON cs.DealershipCarId = dc.Id
        JOIN AutoDealershipStaging.dbo.Cars c ON dc.CarId = c.Id
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
