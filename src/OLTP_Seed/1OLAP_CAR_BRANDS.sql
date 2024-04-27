DECLARE @CurrentMaxId INT;
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.Brands;

INSERT INTO AutoDealershipOLAP.dbo.Brands(
    Id,
    Name
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    b.Name
FROM 
    AutoDealership.dbo.Brands b
GROUP BY b.Name
    
INSERT INTO AutoDealershipOLAP.dbo.Brands
SELECT * FROM AutoDealershipOLAP.dbo.Brands;
