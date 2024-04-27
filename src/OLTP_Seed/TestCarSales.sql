DECLARE @CurrentMaxId INT;
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAPTmp.dbo.CarSales;

;WITH SalesData AS (
    SELECT 
        ds.Id AS DealershipId,
        c.BrandId,
        FORMAT(cs.SaleDate, 'yyyy-MM') as SaleDate,
        SUM(c.Price) AS TotalPrice
    FROM
        AutoDealership.dbo.AutoDealerships ds
        JOIN AutoDealership.dbo.DealershipCars dc ON dc.DealershipId = ds.Id
        JOIN AutoDealership.dbo.CarSales cs ON cs.DealershipCarId = dc.CarId
        JOIN AutoDealership.dbo.Cars c ON dc.CarId = c.Id
    GROUP BY
        ds.Id,
        c.BrandId,
        FORMAT(cs.SaleDate, 'yyyy-MM')
        
),

RankedSalesData AS (
    SELECT
        DealershipId,
        BrandId,
        SaleDate,
        TotalPrice,
        ISNULL(LAG(TotalPrice, 1) OVER (PARTITION BY DealershipId ORDER BY SaleDate),0) AS TotalIncomeLastMonth
    FROM
        SalesData
)
SELECT * FROM RankedSalesData order by SaleDate asc

SELECT SUM(c.Price)
    FROM
        AutoDealership.dbo.AutoDealerships ds
        JOIN AutoDealership.dbo.DealershipCars dc ON dc.DealershipId = ds.Id
        JOIN AutoDealership.dbo.CarSales cs ON cs.DealershipCarId = dc.CarId
        JOIN AutoDealership.dbo.Cars c ON dc.CarId = c.Id
        where ds.Id=6 and c.BrandId=32 and SaleDate BETWEEN '2021-04-01' AND '2021-04-30'

-- SELECT
--     @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
--     DealershipId,
--     BrandId,
--     TotalIncomeLastMonth,
--     (SELECT TOP 1 id FROM AutoDealershipOLAPTmp.dbo.Dates d WHERE d.Year=YEAR(SaleDate) AND d.Month=MONTH(SaleDate) AND d.[Day]=1) AS StartDateId,
--     (SELECT TOP 1 id FROM AutoDealershipOLAPTmp.dbo.Dates d WHERE d.Year=YEAR(SaleDate) AND d.Month=MONTH(SaleDate) AND d.[Day]=DAY(EOMONTH(SaleDate))) AS EndDateId,
--     TotalPrice AS TotalIncomeForCurrentMonth,
--     (TotalPrice - TotalIncomeLastMonth) / ((TotalPrice + TotalIncomeLastMonth) /2 )* 100 - 100 AS MonthTotalIncomeModifyPercent,
--     COUNT(TotalIncomeLastMonth) AS SalesCountForLastMonth,
--     COUNT(TotalPrice) AS SalesCountForCurrentMonth,
--     COUNT(TotalPrice) - COUNT(TotalIncomeLastMonth) AS SalesCountChangeForMonth
-- FROM
--     RankedSalesData
-- GROUP BY
--     DealershipId,
--     BrandId,
--     TotalIncomeLastMonth,
--     TotalPrice,
--     SaleDate;