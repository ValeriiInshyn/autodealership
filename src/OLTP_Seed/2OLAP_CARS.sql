DECLARE @CurrentMaxId INT;
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAPTmp.dbo.Cars;

INSERT INTO AutoDealershipStaging.dbo.Cars
SELECT * FROM AutoDealership.dbo.Cars;

INSERT INTO AutoDealershipOLAP.dbo.Cars(
    Id,
    Model,
    BrandId,
    Generation
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    ISNULL(ds.Model,'NULL'),
    (SELECT TOP 1 id FROM AutoDealershipOLAP.dbo.Brands d WHERE d.Name=b.Name) AS BrandId,
    ds.Generation
FROM 
    AutoDealershipStaging.dbo.Cars ds JOIN
    AutoDealershipStaging.dbo.Brands b on b.Id=ds.BrandId

