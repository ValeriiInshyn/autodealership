DECLARE @CurrentMaxId INT;
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
