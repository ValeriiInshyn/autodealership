DECLARE @CurrentMaxId INT;
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