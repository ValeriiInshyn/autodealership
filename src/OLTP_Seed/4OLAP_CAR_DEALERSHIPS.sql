DECLARE @CurrentMaxId INT;
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAPTmp.dbo.AutoDealerships;

INSERT INTO AutoDealershipOLAPTmp.dbo.AutoDealerships(
    Id,
    Name
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    ds.Name
FROM 
    AutoDealership.dbo.AutoDealerships ds

INSERT INTO AutoDealershipOLAP.dbo.AutoDealerships
SELECT * FROM AutoDealershipOLAPTmp.dbo.AutoDealerships;