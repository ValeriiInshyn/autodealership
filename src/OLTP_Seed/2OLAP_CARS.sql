DECLARE @CurrentMaxId INT;
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAPTmp.dbo.Cars;

INSERT INTO AutoDealershipOLAPTmp.dbo.Cars(
    Id,
    Model,
    BrandId,
    Generation
)
SELECT
    @CurrentMaxId + ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    ISNULL(ds.Model,'NULL'),
    (SELECT TOP 1 id FROM AutoDealershipOLAPTmp.dbo.Brands d WHERE d.Name=b.Name) AS BrandId,
    ds.Generation
FROM 
    AutoDealership.dbo.Cars ds JOIN
    AutoDealership.dbo.Brands b on b.Id=ds.BrandId

INSERT INTO AutoDealershipOLAP.dbo.Cars
SELECT * FROM AutoDealershipOLAPTmp.dbo.Cars;