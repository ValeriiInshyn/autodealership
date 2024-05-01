DECLARE @CurrentMaxId INT;
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

