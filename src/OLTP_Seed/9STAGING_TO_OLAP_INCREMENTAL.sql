use AutoDealershipOLAP;
Go
create or alter procedure dbo.StagingToOlapIncremental
as
begin
-- Job variables
DECLARE @lastloaddate datetime;
SET @lastloaddate = (select MAX(load_datetime) from AutoDealershipMetadata.dbo.data_load_history)
SELECT @lastloaddate AS lastloaddate;
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

    SELECT LeaseSignDate AS Date FROM AutoDealershipStaging.dbo.Leases
    UNION
    SELECT LeaseStartDate AS Date FROM AutoDealershipStaging.dbo.Leases
    UNION
    SELECT LeaseEndDate AS Date FROM AutoDealershipStaging.dbo.Leases
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
                FROM AutoDealershipStaging.dbo.CarSales
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
) AS CombinedDates
WHERE NOT EXISTS (
    -- Check if the date already exists in the DatesDim table
    SELECT 1
    FROM AutoDealershipOLAP.dbo.DatesDim dd
    WHERE
        dd.Year = YEAR(CombinedDates.Date)
        AND dd.Month = MONTH(CombinedDates.Date)
        AND dd.Day = DAY(CombinedDates.Date)
);

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+3;
        SET @metastring = @metastring + 'insert into AutoDealershipMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(5,' + CONVERT(varchar(MAX),@ident) + ');'
    END

--Brands
merge AutoDealershipOLAP.dbo.BrandsDim as trg
using 
	(SELECT
    b.Name,
	b.Id as bk_brand_id
FROM 
    AutoDealershipStaging.dbo.Brands b
GROUP BY b.Name, b.Id) as src
on
	(src.bk_brand_id = trg.bk_brand_id)
when matched then
	update set 
	Name = src.Name
when not matched then
	insert(Name, bk_brand_id)
	values(src.Name,src.bk_brand_id);

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+3;
        SET @metastring = @metastring + 'insert into AutoDealershipMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(2,' + CONVERT(varchar(MAX),@ident) + ');'
    END
--Cars
merge AutoDealershipOLAP.dbo.CarsDim as trg
using 
	(SELECT
    ISNULL(ds.Model,'NULL') AS Model,
    (SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.BrandsDim d WHERE d.Name=b.Name) AS BrandId,
    ds.Generation,
	ds.Id as bk_car_id
FROM 
    AutoDealershipStaging.dbo.Cars ds JOIN
    AutoDealershipStaging.dbo.Brands b on b.Id=ds.BrandId) as src
on
	(src.bk_car_id = trg.bk_car_id)
when matched then
	update set 
	Model = src.Model,
	BrandId = src.BrandId,
	Generation = src.Generation
when not matched then
	insert(Model,BrandId,Generation, bk_car_id)
	values(src.Model,src.BrandId,src.Generation, src.bk_car_id);

SET @tempcount = @@ROWCOUNT
IF @tempcount<>0
    BEGIN
        SET @rowcount = @rowcount+@tempcount;
        SET @dwtablecount = @dwtablecount+1;
        SET @dbtablecount = @dbtablecount+3;
        SET @metastring = @metastring + 'insert into AutoDealershipMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(4,' + CONVERT(varchar(MAX),@ident) + ');'
    END
--Dealerships
merge AutoDealershipOLAP.dbo.AutoDealershipsDim as trg
using 
	(SELECT
    ds.Name,
	ds.Id as bk_dealership_id
FROM 
    AutoDealershipStaging.dbo.AutoDealerships ds) as src
on
	(src.bk_dealership_id = trg.bk_dealership_id)
when matched then
	update set 
	Name = src.Name
when not matched then
	insert(Name, bk_dealership_id)
	values(src.Name,src.bk_dealership_id);


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
LeaseStartDate,
CreateDate,
UpdateDate
HAVING CreateDate > @lastloaddate or UpdateDate > @lastloaddate
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
-- ;WITH SalesData AS (
--     SELECT 
--         dc.DealershipId AS DealershipId,
--         c.BrandId,
--         FORMAT(cs.SaleDate, 'yyyy-MM') as SaleDate,
--         SUM(c.Price) AS TotalPrice,
--         COUNT(c.Price) as SalesCount
--     FROM
--         AutoDealershipStaging.dbo.CarSales cs 
--         JOIN AutoDealershipStaging.dbo.DealershipCars dc ON cs.DealershipCarId = dc.Id
--         JOIN AutoDealershipStaging.dbo.Cars c ON dc.CarId = c.Id
--     GROUP BY
--         dc.DealershipId,
--         c.BrandId,
--         FORMAT(cs.SaleDate, 'yyyy-MM')
-- ),

-- RankedSalesData AS (
--     SELECT
--         DealershipId,
--         BrandId,
--         CONVERT(DATE, SaleDate + '-01') AS SaleDate,
--         TotalPrice as TotalIncomeForCurrentMonth,
--         ISNULL(LAG(TotalPrice, 1) OVER (PARTITION BY DealershipId, BrandId ORDER BY SaleDate), 0) AS TotalIncomeLastMonth,
--         ISNULL(LAG(SalesCount, 1) OVER (PARTITION BY DealershipId, BrandId ORDER BY SaleDate), 0) AS SalesCountForLastMonth,
--         SalesCount AS SalesCountForCurrentMonth
--     FROM
--         SalesData
-- )
-- merge AutoDealershipOLAP.dbo.CarSalesFact as trg
-- using 
-- 	(SELECT
--     DealershipId,
--     BrandId,
--     TotalIncomeLastMonth,
--     (SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.DatesDim d WHERE d.Year=YEAR(SaleDate) AND d.Month=MONTH(SaleDate) AND d.[Day]=1) AS StartDateId,
--     (SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.DatesDim d WHERE d.Year=YEAR(SaleDate) AND d.Month=MONTH(SaleDate) AND d.[Day]=DAY(EOMONTH(SaleDate))) AS EndDateId,
--     TotalIncomeForCurrentMonth,
--     (TotalIncomeForCurrentMonth - TotalIncomeLastMonth) / ((TotalIncomeForCurrentMonth + TotalIncomeLastMonth) /2 ) * 100 - 100 AS MonthTotalIncomeModifyPercent,
--     SalesCountForLastMonth,
--     SalesCountForCurrentMonth,
--     SalesCountForCurrentMonth - SalesCountForLastMonth AS SalesCountChangeForMonth,
--     SaleDate
-- FROM
--     RankedSalesData
-- GROUP BY
--     DealershipId,
--     BrandId,
--     SaleDate,
--     TotalIncomeLastMonth,
--     TotalIncomeForCurrentMonth,
--     SalesCountForCurrentMonth,
--     SalesCountForLastMonth) as src

-- on
-- 	(YEAR(src.SaleDate)=YEAR(@lastloaddate) AND MONTH(src.SaleDate)=MONTH(@lastloaddate))
-- when matched then
-- 	update set 
-- 	AutoDealershipId = src.DealershipId,
--     BrandId=src.BrandId,
--     TotalIncomeLastMonth=src.TotalIncomeLastMonth,
--     StartDateId=src.StartDateId,
--     EndDateId=src.EndDateId,
--     TotalIncomeForCurrentMonth=src.TotalIncomeForCurrentMonth,
--     MonthTotalIncomeModifyPercent=src.MonthTotalIncomeModifyPercent,
--     SalesCountForLastMonth=src.SalesCountForLastMonth,
--     SalesCountForCurrentMonth=src.SalesCountForCurrentMonth,
--     SalesCountChangeForMonth=src.SalesCountChangeForMonth
-- when not matched then
-- 	insert( BrandId,
--     TotalIncomeLastMonth,
--     StartDateId,
--     EndDateId,
--     TotalIncomeForCurrentMonth,
--     MonthTotalIncomeModifyPercent,
--     SalesCountForLastMonth,
--     SalesCountForCurrentMonth,
--     SalesCountChangeForMonth)
-- 	values(src.DealershipId,
--     src.BrandId,
--     src.TotalIncomeLastMonth,
--     src.StartDateId,
--     src.EndDateId,
--     src.TotalIncomeForCurrentMonth,
--     src.MonthTotalIncomeModifyPercent,
--     src.SalesCountForLastMonth,
--     src.SalesCountForCurrentMonth,
--     src.SalesCountChangeForMonth);

-- SET @tempcount = @@ROWCOUNT
-- IF @tempcount<>0
--     BEGIN
--         SET @rowcount = @rowcount+@tempcount;
--         SET @dwtablecount = @dwtablecount+1;
--         SET @dbtablecount = @dbtablecount+3;
--         SET @metastring = @metastring + 'insert into AutoDealershipMetadata.dbo.dw_table_data_load_history(dw_table_id,data_load_history_id) values(3,' + CONVERT(varchar(MAX),@ident) + ');'
--     END

--Load to Meta DB
insert into AutoDealershipMetadata.dbo.data_load_history(load_datetime,load_time,load_rows,affected_table_count,source_table_count)
values(GETDATE(), CONVERT(TIME(7), GETDATE()),@rowcount,@dwtablecount,@dbtablecount)
EXEC(@metastring)
end;