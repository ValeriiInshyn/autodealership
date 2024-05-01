TRUNCATE TABLE AutoDealershipOLAP.dbo.CarSalesFact
DBCC CHECKIDENT('AutoDealershipOLAP.dbo.CarSalesFact',RESEED,0)
TRUNCATE TABLE AutoDealershipOLAP.dbo.LeasesFact
DBCC CHECKIDENT('AutoDealershipOLAP.dbo.LeasesFact',RESEED,0)
delete from AutoDealershipOLAP.dbo.DatesDim
DBCC CHECKIDENT('AutoDealershipOLAP.dbo.DatesDim',RESEED,0)
delete from AutoDealershipOLAP.dbo.CarsDim
DBCC CHECKIDENT('AutoDealershipOLAP.dbo.CarsDim',RESEED,0)
delete from AutoDealershipOLAP.dbo.AutoDealershipsDim
DBCC CHECKIDENT('AutoDealershipOLAP.dbo.AutoDealershipsDim',RESEED,0)
delete from AutoDealershipOLAP.dbo.BrandsDim
DBCC CHECKIDENT('AutoDealershipOLAP.dbo.BrandsDim',RESEED,0)

-- Job variables
DECLARE @rowcount int;
SET @rowcount = 0;
DECLARE @dwtablecount int;
SET @dwtablecount = 0;
DECLARE @dbtablecount int;
SET @dbtablecount = 0;
DECLARE @tempcount int;
SET @tempcount = 0;
DECLARE @startdate datetime;
SET @startdate = GETDATE();
DECLARE @ident int;
SET @ident = IDENT_CURRENT('AutoDealershipMetadata.dbo.data_load_history') + 1;
DECLARE @metastring varchar(MAX);
SET @metastring = '';

--Car brands
INSERT INTO AutoDealershipOLAP.dbo.BrandsDim(
    Name,
	bk_brand_id
)
SELECT
    b.Name,
	b.Id
FROM 
    AutoDealershipStaging.dbo.Brands b
GROUP BY b.Name, b.Id
SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+3;
        SET @metastring = @metastring + 'insert into AutoDealershipMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(2,' + CONVERT(varchar(MAX),@ident) + ');'
    END

--Cars
INSERT INTO AutoDealershipOLAP.dbo.CarsDim(
    Model,
    BrandId,
    Generation,
	bk_car_id
)
SELECT
    ISNULL(ds.Model,'NULL'),
    (SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.BrandsDim d WHERE d.Name=b.Name) AS BrandId,
    ds.Generation,
	ds.Id
FROM 
    AutoDealershipStaging.dbo.Cars ds JOIN
    AutoDealershipStaging.dbo.Brands b on b.Id=ds.BrandId

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+3;
        SET @metastring = @metastring + 'insert into AutoDealershipMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(4,' + CONVERT(varchar(MAX),@ident) + ');'
    END

--Dates
INSERT INTO AutoDealershipOLAP.dbo.DatesDim(
    [Year],
    [Month],
    [Day]
)
SELECT
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

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+3;
        SET @metastring = @metastring + 'insert into AutoDealershipMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(5,' + CONVERT(varchar(MAX),@ident) + ');'
    END

--Dealerships
INSERT INTO AutoDealershipOLAP.dbo.AutoDealershipsDim(
    Name,
	bk_dealership_id
)
SELECT
    ds.Name,
	ds.Id
FROM 
    AutoDealershipStaging.dbo.AutoDealerships ds

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+3;
        SET @metastring = @metastring + 'insert into AutoDealershipMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(1,' + CONVERT(varchar(MAX),@ident) + ');'
    END

--Leases
DECLARE @iterator INT;
SET @iterator = 0;
-- Insert data into the Leases table
INSERT INTO AutoDealershipOLAP.dbo.LeasesFact (
    CarId, 
    Price, 
    PreviousLeaseModifyPercent, 
    LeaseSignDateId, 
    LeaseStartDateId, 
    LeaseEndDateId, 
    LastLeaseId
)
SELECT 
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
AutoDealershipStaging.dbo.Leases
GROUP BY 
LeaseSignDate,
DealershipCarId,
TotalPrice,
LeaseEndDate,
LeaseStartDate
ORDER BY
DealershipCarId,LeaseSignDate

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+3;
        SET @metastring = @metastring + 'insert into AutoDealershipMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(6,' + CONVERT(varchar(MAX),@ident) + ');'
    END

--Sales
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
INSERT INTO AutoDealershipOLAP.dbo.CarSalesFact (
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

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+3;
        SET @metastring = @metastring + 'insert into AutoDealershipMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(3,' + CONVERT(varchar(MAX),@ident) + ');'
    END

--Load to Meta DB
insert into AutoDealershipMetadata.dbo.data_load_history(load_datetime,load_time,load_rows,affected_table_count,source_table_count)
values(GETDATE(), CONVERT(TIME(7), GETDATE()),@rowcount,@dwtablecount,@dbtablecount)
EXEC(@metastring)
GO

TRUNCATE TABLE [AutoDealershipStaging].[dbo].[AutoDealerships]
TRUNCATE TABLE [AutoDealershipStaging].[dbo].[Brands]
TRUNCATE TABLE [AutoDealershipStaging].[dbo].[Cars]
TRUNCATE TABLE [AutoDealershipStaging].[dbo].[CarSales]
TRUNCATE TABLE [AutoDealershipStaging].[dbo].[DealershipCars]
TRUNCATE TABLE [AutoDealershipStaging].[dbo].[Leases]