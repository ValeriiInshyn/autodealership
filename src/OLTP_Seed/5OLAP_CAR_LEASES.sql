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
DECLARE @CurrentMaxId INT;
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

