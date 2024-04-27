DECLARE @CurrentMaxId INT;
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAPTmp.dbo.CarSales;

;WITH SalesData AS (
    SELECT 
        ds.Id AS DealershipId,
        c.BrandId,
        FORMAT(cs.SaleDate, 'yyyy-MM') as SaleDate,
        SUM(c.Price) AS TotalPrice,
        COUNT(c.Price) as SalesCount
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
        CONVERT(DATE, SaleDate + '-01') AS SaleDate,
        TotalPrice as TotalIncomeForCurrentMonth,
        ISNULL(LAG(TotalPrice, 1) OVER (PARTITION BY DealershipId, BrandId ORDER BY SaleDate), 0) AS TotalIncomeLastMonth,
        ISNULL(LAG(SalesCount, 1) OVER (PARTITION BY DealershipId, BrandId ORDER BY SaleDate), 0) AS SalesCountForLastMonth,
        SalesCount AS SalesCountForCurrentMonth
    FROM
        SalesData
)
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
    DealershipId,
    BrandId,
    TotalIncomeLastMonth,
    (SELECT TOP 1 id FROM AutoDealershipOLAPTmp.dbo.Dates d WHERE d.Year=YEAR(SaleDate) AND d.Month=MONTH(SaleDate) AND d.[Day]=1) AS StartDateId,
    (SELECT TOP 1 id FROM AutoDealershipOLAPTmp.dbo.Dates d WHERE d.Year=YEAR(SaleDate) AND d.Month=MONTH(SaleDate) AND d.[Day]=DAY(EOMONTH(SaleDate))) AS EndDateId,
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

INSERT INTO AutoDealershipOLAP.dbo.CarSales
SELECT * FROM AutoDealershipOLAPTmp.dbo.CarSales;