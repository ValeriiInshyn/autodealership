DECLARE @CurrentMaxId INT;
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAPTmp.dbo.Brands;

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
    

