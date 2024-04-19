
--DELETE FROM Cars;

--DELETE FROM CarBodyTypes;
--DELETE FROM CarComfortOptions;
--DELETE FROM CarDelivery;
--DELETE FROM CarMultimediaOptions;

--DELETE FROM CarSafetyOptions;
--DELETE FROM CarSales;
--DELETE FROM CarTypes;
--DELETE FROM ComfortOptions;
--DELETE FROM Customers;
--DELETE FROM DealershipCars;
--DELETE FROM DealershipCarStatuses;
--DELETE FROM Distributors;
--DELETE FROM Employees;
--DELETE FROM Engines;
--DELETE FROM EngineTypes;
--DELETE FROM GearBoxTypes;

--DELETE FROM MultimediaOptions;
--DELETE FROM PaymentMethods;
--DELETE FROM SafetyOptions;
--DELETE FROM SaleStatus;

--DELETE FROM Leases;
--DELETE FROM LeaseProposalConditions;
--DELETE FROM LeaseProposals;
--DELETE FROM Conditions;
--DELETE FROM LeaseTypes;


--DELETE FROM Brands;
--DELETE FROM AutoDealerships;
--DELETE FROM Cities;
--DELETE FROM Countries;
--DELETE FROM Colors;

--BULK INSERT Colors
--FROM 'C:\Users\Work\Desktop\csvFiles\colors.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT Countries
--FROM 'C:\Users\Work\Desktop\csvFiles\countries.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT Cities
--FROM 'C:\Users\Work\Desktop\csvFiles\cities.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT Brands
--FROM 'C:\Users\Work\Desktop\csvFiles\brands.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT CarBodyTypes
--FROM 'C:\Users\Work\Desktop\csvFiles\carbodytypes.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT CarTypes
--FROM 'C:\Users\Work\Desktop\csvFiles\carbodytypes.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT AutoDealerships
--FROM 'C:\Users\Work\Desktop\csvFiles\autodealerships.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT EngineTypes
--FROM 'C:\Users\Work\Desktop\csvFiles\enginetypes.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT Engines
--FROM 'C:\Users\Work\Desktop\csvFiles\engines.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT Employees
--FROM 'C:\Users\Work\Desktop\csvFiles\employees.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT GearBoxTypes
--FROM 'C:\Users\Work\Desktop\csvFiles\gearboxtypes.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT SaleStatus
--FROM 'C:\Users\Work\Desktop\csvFiles\salestatuses.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT LeaseTypes
--FROM 'C:\Users\Work\Desktop\csvFiles\leasetypes.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT Customers
--FROM 'C:\Users\Work\Desktop\csvFiles\customers.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT MultimediaOptions
--FROM 'C:\Users\Work\Desktop\csvFiles\multimediaoptions.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT ComfortOptions
--FROM 'C:\Users\Work\Desktop\csvFiles\comfortoptions.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--SET IDENTITY_INSERT SafetyOptions ON;
--BULK INSERT SafetyOptions
--FROM 'C:\Users\Work\Desktop\csvFiles\safetyoptions.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);
--SET IDENTITY_INSERT SafetyOptions OFF;
--BULK INSERT PaymentMethods
--FROM 'C:\Users\Work\Desktop\csvFiles\paymentmethods.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT DealershipCarStatuses
--FROM 'C:\Users\Work\Desktop\csvFiles\dealershipcarstatuses.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT Conditions
--FROM 'C:\Users\Work\Desktop\csvFiles\conditions.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT LeaseProposals
--FROM 'C:\Users\Work\Desktop\csvFiles\leaseproposals.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT LeaseProposalConditions
--FROM 'C:\Users\Work\Desktop\csvFiles\leaseproposalconditions.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT Cars
--FROM 'C:\Users\Work\Desktop\csvFiles\cars.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT DealershipCars
--FROM 'C:\Users\Work\Desktop\csvFiles\dealershipcars.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT CarSales
--FROM 'C:\Users\Work\Desktop\csvFiles\carsales.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT Distributors
--FROM 'C:\Users\Work\Desktop\csvFiles\distributors.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT CarDelivery
--FROM 'C:\Users\Work\Desktop\csvFiles\cardeliveries.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);

--BULK INSERT Leases
--FROM 'C:\Users\Work\Desktop\csvFiles\leases.csv'
--WITH (
--    FIELDTERMINATOR = ',', -- specify the delimiter used in your CSV file
--    ROWTERMINATOR = '\n', -- specify the line terminator used in your CSV file
--    FIRSTROW = 2 -- specify if you want to skip the header row
--);
