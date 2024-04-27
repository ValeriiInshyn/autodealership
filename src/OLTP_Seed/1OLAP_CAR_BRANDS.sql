DECLARE @CurrentMaxId INT;
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAPTmp.dbo.Brands;

INSERT INTO AutoDealershipOLAPTmp.dbo.Brands(
    Id,
    Name
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    b.Name
FROM 
    AutoDealership.dbo.Brands b
    
INSERT INTO AutoDealershipOLAP.dbo.Brands
SELECT * FROM AutoDealershipOLAPTmp.dbo.Brands;