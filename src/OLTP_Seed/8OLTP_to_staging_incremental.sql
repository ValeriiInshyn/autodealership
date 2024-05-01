use AutoDealershipStaging;
Go
create or alter procedure dbo.OLTPToStagingIncremental
as
begin
DECLARE @lastloaddate datetime;
SET @lastloaddate = (select MAX(load_datetime) from AutoDealershipMetadata.dbo.data_load_history)
SELECT @lastloaddate AS lastloaddate;
TRUNCATE TABLE [AutoDealershipStaging].[dbo].[AutoDealerships]
TRUNCATE TABLE [AutoDealershipStaging].[dbo].[Brands]
TRUNCATE TABLE [AutoDealershipStaging].[dbo].[Cars]
TRUNCATE TABLE [AutoDealershipStaging].[dbo].[CarSales]
TRUNCATE TABLE [AutoDealershipStaging].[dbo].[DealershipCars]
TRUNCATE TABLE [AutoDealershipStaging].[dbo].[Leases]



INSERT INTO [AutoDealershipStaging].[dbo].[AutoDealerships]
SELECT * FROM [AutoDealership].[dbo].[AutoDealerships]
WHERE CreateDate > @lastloaddate or UpdateDate > @lastloaddate

INSERT INTO [AutoDealershipStaging].[dbo].[Brands]
SELECT * FROM [AutoDealership].[dbo].[Brands]
WHERE CreateDate > @lastloaddate or UpdateDate > @lastloaddate

INSERT INTO [AutoDealershipStaging].[dbo].[DealershipCars]
SELECT * FROM [AutoDealership].[dbo].[DealershipCars]
WHERE CreateDate > @lastloaddate or UpdateDate > @lastloaddate

SET IDENTITY_INSERT [AutoDealershipStaging].[dbo].[Cars] ON
INSERT INTO [AutoDealershipStaging].[dbo].[Cars]
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
SELECT[Id]
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
      ,[UpdateDate] FROM [AutoDealership].[dbo].[Cars]
WHERE CreateDate > @lastloaddate or UpdateDate > @lastloaddate

SET IDENTITY_INSERT [AutoDealershipStaging].[dbo].[Cars] OFF

SET IDENTITY_INSERT [AutoDealershipStaging].[dbo].[CarSales] ON
INSERT INTO [AutoDealershipStaging].[dbo].[CarSales]
([Id]
      ,[DealershipCarId]
      ,[CustomerId]
      ,[SaleDate]
      ,[StatusId]
      ,[ExpectedDeliveryDate]
      ,[EmployeeId]
      ,[PaymentMethodId]
      ,[CreateDate]
      ,[UpdateDate])
SELECT [Id]
      ,[DealershipCarId]
      ,[CustomerId]
      ,[SaleDate]
      ,[StatusId]
      ,[ExpectedDeliveryDate]
      ,[EmployeeId]
      ,[PaymentMethodId]
      ,[CreateDate]
      ,[UpdateDate] FROM [AutoDealership].[dbo].[CarSales]
WHERE CreateDate > @lastloaddate or UpdateDate > @lastloaddate

SET IDENTITY_INSERT [AutoDealershipStaging].[dbo].[CarSales] OFF

SET IDENTITY_INSERT [AutoDealershipStaging].[dbo].[Leases] ON
INSERT INTO [AutoDealershipStaging].[dbo].[Leases]
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
      ,[UpdateDate] FROM [AutoDealership].[dbo].[Leases]
WHERE CreateDate > @lastloaddate or UpdateDate > @lastloaddate

SET IDENTITY_INSERT [AutoDealershipStaging].[dbo].[Leases] OFF
end;