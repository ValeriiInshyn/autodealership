DECLARE @CurrentMaxId INT;
SELECT @CurrentMaxId = ISNULL(MAX(Id), 0) FROM AutoDealershipOLAP.dbo.Cars;

SET IDENTITY_INSERT AutoDealershipStaging.dbo.Cars ON;
INSERT INTO AutoDealershipStaging.dbo.Cars
([Id]
      ,[CarTypeId]
      ,[Doors]
      ,[Seats]
      ,[Year]
      ,[Price]
      ,[ColorId]
      ,[BodyTypeId]
      ,[EngineId]
      ,[BrandId]
      ,[Model]
      ,[Generation]
      ,[Weight]
      ,[MaxSpeed]
      ,[GearsCount]
      ,[Height]
      ,[Width]
      ,[Length]
      ,[GearBoxId]
      ,[FuelTankCapacity]
      ,[WheelsCount]
      ,[CreateDate]
      ,[UpdateDate])
SELECT [Id]
      ,[CarTypeId]
      ,[Doors]
      ,[Seats]
      ,[Year]
      ,[Price]
      ,[ColorId]
      ,[BodyTypeId]
      ,[EngineId]
      ,[BrandId]
      ,[Model]
      ,[Generation]
      ,[Weight]
      ,[MaxSpeed]
      ,[GearsCount]
      ,[Height]
      ,[Width]
      ,[Length]
      ,[GearBoxId]
      ,[FuelTankCapacity]
      ,[WheelsCount]
      ,[CreateDate]
      ,[UpdateDate] FROM AutoDealership.dbo.Cars;
SET IDENTITY_INSERT AutoDealershipStaging.dbo.Cars OFF;


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

