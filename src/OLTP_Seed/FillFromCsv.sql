USE [AutoDealership]
GO
DELETE FROM Cars;

DELETE FROM CarBodyTypes;
DELETE FROM CarComfortOptions;
DELETE FROM CarDelivery;
DELETE FROM CarMultimediaOptions;

DELETE FROM CarSafetyOptions;
DELETE FROM CarSales;
DELETE FROM CarTypes;
DELETE FROM ComfortOptions;
DELETE FROM Customers;
DELETE FROM DealershipCars;
DELETE FROM DealershipCarStatuses;
DELETE FROM Distributors;
DELETE FROM Employees;
DELETE FROM Engines;
DELETE FROM EngineTypes;
DELETE FROM GearBoxTypes;

DELETE FROM MultimediaOptions;
DELETE FROM PaymentMethods;
DELETE FROM SafetyOptions;
DELETE FROM SaleStatus;

DELETE FROM Leases;
DELETE FROM LeaseProposalConditions;
DELETE FROM LeaseProposals;
DELETE FROM Conditions;
DELETE FROM LeaseTypes;


DELETE FROM Brands;
DELETE FROM AutoDealerships;
DELETE FROM Cities;
DELETE FROM Countries;
DELETE FROM Colors;

BULK INSERT Colors
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\colors.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT Countries
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\countries.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT Cities
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\cities.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT Brands
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\brands.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT CarBodyTypes
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\carbodytypes.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT CarTypes
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\carbodytypes.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT AutoDealerships
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\autodealerships.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT EngineTypes
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\enginetypes.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT Engines
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\engines.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT Employees
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\employees.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT GearBoxTypes
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\gearboxtypes.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT SaleStatus
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\salestatuses.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT LeaseTypes
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\leasetypes.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT Customers
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\customers.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT MultimediaOptions
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\multimediaoptions.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT ComfortOptions
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\comfortoptions.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT SafetyOptions
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\safetyoptions.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);
BULK INSERT PaymentMethods
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\paymentmethods.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT DealershipCarStatuses
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\dealershipcarstatuses.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT Conditions
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\conditions.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT LeaseProposals
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\leaseproposals.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT LeaseProposalConditions
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\leaseproposalconditions.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT Cars
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\cars.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT DealershipCars
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\dealershipcars.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT CarSales
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\carsales.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT Distributors
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\distributors.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT CarDelivery
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\cardeliveries.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);

BULK INSERT Leases
FROM 'C:\Projects\autodealership\src\OLTP_Seed\csv_files\leases.csv'
WITH (
   FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
   ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
   FIRSTROW = 2 -- specify if you want to skip the header row
);
