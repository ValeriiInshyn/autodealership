using CsvHelper;
using Microsoft.EntityFrameworkCore;
using OLTP_Seed.Generators;
using OLTP_Seed.Helpers;
using OLTP_Seed.Models;
using System;
using System.Globalization;
using CsvReader = OLTP_Seed.Helpers.CsvReader;

using (var context = new AutoDealershipContext())
{
    // Deletes and creates db | Using for cleaning all data before initial seed
    //context.Database.EnsureDeleted();
    //context.Database.EnsureCreated();
    List<Color> colorsForCsv = new List<Color>();


    var carSalesFromCSV = CsvReader.ReadCarSalesFromCsv();

    //#region ColorsInit
    //var colorsToAdd = carSalesFromCSV.Select(e => e.Color).Distinct().ToList();

    //for (i = 0; i < colorsToAdd.Count(); i++)
    //{
    //    if (colorsToAdd[i].Length > 0 && (char)colorsToAdd[i][0] > 65 && (char)colorsToAdd[i][0] < 123)
    //    {
    //        colorsForCsv.Add(new Color
    //        {
    //            Id = i + 1,
    //            Name = colorsToAdd[i],
    //            CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //            UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //        });
    //    }
    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\colors.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(colorsForCsv);
    //}
    //#endregion

    //#region CountriesInit

    //var countriesCsv = new List<Country>();
    //int i = 0;
    //foreach (var countryName in CountryGenerator.GenerateCountriesList())
    //{
    //    countriesCsv.Add(new Country()
    //    {
    //        Id = ++i,
    //        Name = countryName,
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\countries.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(countriesCsv);
    //}
    //#endregion

    //#region CitiesInit

    //var citiesCsv = new List<City>();
    //int i = 0;
    //foreach (var cityByCountry in CitiesGenerator.GenerateCities())
    //{
    //    var countryForCity = context.Countries.FirstOrDefault(e => e.Name == cityByCountry.Key);

    //    if (countryForCity is not null)
    //    {
    //        foreach (var city in cityByCountry.Value)
    //        {
    //            citiesCsv.Add(new City()
    //            {
    //                Id = ++i,
    //                Name = city,
    //                CountryId = countryForCity.Id,
    //                CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //                UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //            });
    //        }
    //    }
    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\cities.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(citiesCsv);
    //}
    //#endregion

    //#region BrandsInit

    //int i = 0;
    //var brandsToAdd = BrandDtoGenerator.GenerateBrandDtos();
    //var brandsCsv = new List<Brand>();
    //foreach (var brand in brandsToAdd)
    //{
    //    var countryForBrand = context.Countries.FirstOrDefault(e => e.Name == brand.CountryName);

    //    if (countryForBrand is not null)
    //    {
    //        brandsCsv.Add(new Brand()
    //            {
    //                Id = ++i,
    //                Name = brand.Name,
    //                Country = countryForBrand,
    //                CountryId = countryForBrand.Id,
    //                CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //                UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //            });
    //    }
    //}

    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\brands.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(brandsCsv);
    //}

    //#endregion

    //#region CarBodyTypesInit

    //int i = 0;
    //var carBodyTypesToAdd = carSalesFromCSV.Select(e => e.Body).Distinct();
    //var bodyTypesCsv = new List<CarBodyType>();
    //foreach (var carBodyType in carBodyTypesToAdd)
    //{
    //    bodyTypesCsv.Add(new CarBodyType
    //    {
    //        Id = ++i,
    //        Name = carBodyType,
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //}

    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\carbodytypes.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(bodyTypesCsv);
    //}
    //#endregion

    //#region CarTypesInit

    //i = 0;
    //var carTypesToAdd = carSalesFromCSV.Select(e => e.Body).Distinct();

    //foreach (var carType in carTypesToAdd)
    //{
    //    context.CarTypes.Add(new CarType()
    //    {
    //        Id = ++i,
    //        Name = carType,
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //}

    //context.SaveChanges();
    //#endregion

    //#region AutoDealershipsInit

    //int i = 0;
    //var autoDealershipsToAdd = carSalesFromCSV.Select(e => e.Seller).Distinct();
    //var dealershipsCsv = new List<AutoDealership>();
    //foreach (var autoDealership in autoDealershipsToAdd)
    //{
    //    dealershipsCsv.Add(new AutoDealership
    //    {
    //        Id = ++i,
    //        Name = autoDealership,
    //        CityId = Random.Shared.Next(1, context.Cities.Count()),
    //        Street = StreetNameGenerator.GenerateStreetName(),
    //        Number = Random.Shared.Next(1, 50).ToString(),
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //}

    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\autodealerships.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(dealershipsCsv);
    //}
    //#endregion

    // #region EngineTypesInit

    //int i = 0;
    //var engineTypesCsv = new List<EngineType>();
    // foreach (var enigneType in EngineTypeGenerator.GenerateEngineTypesList())
    // {
    //     engineTypesCsv.Add(new EngineType()
    //     {
    //         Id = ++i,
    //         Name = enigneType,
    //         CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //         UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //     });
    // }

    // using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\enginetypes.csv"))

    // using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    // {
    //     csv.WriteRecords(engineTypesCsv);
    // }
    // #endregion

    //#region EnginesInit

    //int i = 0;

    //var enginesToAdd = context.Brands;
    //var enginesCsv = new List<Engine>();
    //foreach (var engine in enginesToAdd)
    //{
    //    enginesCsv.Add(new Engine()
    //    {
    //        Id = ++i,
    //        Brand = engine,
    //        BrandId = engine.Id,
    //        EngineTypeId = Random.Shared.Next(2, 3),
    //        EnginePower = Random.Shared.Next(100, 500),
    //        EngineVolume = (decimal)Random.Shared.Next(1, 4),
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\engines.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(enginesCsv);
    //}

    //#endregion

    //#region EmployeesInit

    //int i = 0;
    //var employeesCsv = new List<Employee>();
    //for (int j = 1; j < context.AutoDealerships.Count() + 1; j++)
    //{
    //    employeesCsv.Add(
    //        EmployeeGenerator.GenerateEmployee(j, j));
    //}

    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\employees.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(employeesCsv);
    //}
    //#endregion

    //    #region GearBoxTypesInit

    //    var gearBoxTypesCsv = new List<GearBoxType>();
    //    gearBoxTypesCsv.Add(new GearBoxType
    //    {
    //        Id = 1,
    //        Name = "automatic",
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //    gearBoxTypesCsv.Add(new GearBoxType
    //    {
    //        Id = 2,
    //        Name = "manual",
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //    gearBoxTypesCsv.Add(new GearBoxType
    //    {
    //        Id = 3,
    //        Name = "robot",
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });

    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\gearboxtypes.csv"))

    //    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //    {
    //        csv.WriteRecords(gearBoxTypesCsv);
    //    }
    //    #endregion




    //#region SaleStatusInit

    //var saleStatusCsv = new List<SaleStatus>();

    //saleStatusCsv.Add(new SaleStatus
    //{
    //    Id = 1,
    //    Status = "Completed",
    //    CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //    UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //});

    //saleStatusCsv.Add(new SaleStatus
    //{
    //    Id = 2,
    //    Status = "Delivering",
    //    CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //    UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //});
    //saleStatusCsv.Add(new SaleStatus
    //{
    //    Id = 3,
    //    Status = "Requested",
    //    CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //    UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //});
    //saleStatusCsv.Add(new SaleStatus
    //{
    //    Id = 4,
    //    Status = "Rejected",
    //    CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //    UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //});

    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\salestatuses.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(saleStatusCsv);
    //}
    //#endregion

    //#region LeaseTypesInit

    //var leaseTypessCsv = new List<LeaseType>();

    //leaseTypessCsv.Add(new LeaseType
    //{
    //    Id = 1,
    //    Name = "Direct",
    //    CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //    UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //});

    //leaseTypessCsv.Add(new LeaseType
    //{
    //    Id = 2,
    //    Name = "Indirect",
    //    CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //    UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //});

    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\leasetypes.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(leaseTypessCsv);
    //}
    //#endregion

    //#region CustomersInit

    //var customersCsv = new List<Customer>();

    //for (int i = 1; i < carSalesFromCSV.Count() + 1; i++)
    //{
    //    customersCsv.Add(CustomerGenerator.GenerateCustomer(i));

    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\customers.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(customersCsv);
    //}
    //#endregion

    //#region ComfortOptionsInit

    //var comfortOptionsCsv = new List<ComfortOption>();

    //string[] coptArray = new string[]
    //{
    //    "Leather Seats", "Heated Seats", "Ventilated Seats", "Dual-Zone Climate Control", "Power Adjustable Seats",
    //    "Sunroof/Moonroof", "Keyless Entry", "Push Button Start", "Heated Steering Wheel"
    //};

    //for (int i = 0; i < coptArray.Length; i++)
    //{
    //    comfortOptionsCsv.Add(new ComfortOption
    //    {
    //        Id = i + 1,
    //        OptionName = coptArray[i],
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //}

    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\comfortoptions.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(comfortOptionsCsv);
    //}

    //#endregion

    //#region SafetyOptionsInit

    //var safetyOptionsCsv = new List<SafetyOption>();

    //string[] soptArray = new string[]
    //{
    //    "ABS", "Airbags", "Blind Spot Monitoring", "Lane Departure Warning", "Forward Collision Warning", "Adaptive Cruise Control", "Parking Sensors", "Traction Control", "Stability Control"
    //};

    //for (int i = 0; i < soptArray.Length; i++)
    //{
    //    safetyOptionsCsv.Add(new SafetyOption()
    //    {
    //        Id = i + 1,
    //        OptionName = soptArray[i],
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //}

    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\safetyoptions.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(safetyOptionsCsv);
    //}

    //#endregion

    //#region MultimediaOptionsInit

    //var multimediaOptionsCsv = new List<MultimediaOption>();

    //string[] moptArray = new string[]
    //{
    //    "Bluetooth", "Navigation System", "USB Port", "Satellite Radio", "Apple CarPlay", "Android Auto", "Wireless Charging", "DVD Player", "Backup Camera"
    //};

    //for (int i = 0; i < moptArray.Length; i++)
    //{
    //    multimediaOptionsCsv.Add(new MultimediaOption
    //    {
    //        Id = i + 1,
    //        OptionName = moptArray[i],
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //}

    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\multimediaoptions.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(multimediaOptionsCsv);
    //}

    //#endregion

    //#region PeymentMethodsInit

    //var paymentMethodsCsv = new List<PaymentMethod>();
    //string[] paymentMethods = { "Credit Card", "Debit Card", "PayPal", "Bank Transfer", "Cash", "Cryptocurrency" };
    //for (int i = 0; i < paymentMethods.Length; i++)
    //{
    //    paymentMethodsCsv.Add(new PaymentMethod
    //    {
    //        Id = i+1, Name = paymentMethods[i],
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //            UpdateDate = DateOnly.FromDateTime(DateTime.Now)

    //    });
    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\paymentmethods.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(paymentMethodsCsv);
    //}
    //#endregion

    //#region DealershipCarStatusesInit

    //var DealershipCarStatusesCsv = new List<DealershipCarStatus>();
    //List<string> statusNames = new List<string> { "Available", "Sold", "Reserved", "Pending Inspection", "Out for Repair" };

    //for (int i = 0; i < statusNames.Count; i++)
    //{
    //    DealershipCarStatusesCsv.Add(new DealershipCarStatus
    //    {
    //        Id = i + 1,
    //        Name = statusNames[i],
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\dealershipcarstatuses.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(DealershipCarStatusesCsv);
    //}
    //#endregion

    //#region MyRegion

    //List<string> conditionNames = new List<string> { "Minimum Credit Score", "Maximum Mileage", "Maximum Term Length", "Down Payment Percentage", "Interest Rate" };
    //var conditionsCsv = new List<Condition>();


    //for (int i = 0; i < conditionNames.Count; i++)
    //{
    //    conditionsCsv.Add(new Condition()
    //    {
    //        Id = i + 1,
    //        Name = conditionNames[i],
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\conditions.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(conditionsCsv);
    //}
    //#endregion

    //#region LeaseProposalInit

    //List<LeaseProposal> leaseProposals = new List<LeaseProposal>();
    //List<LeaseProposalCondition> proposalConditions = new List<LeaseProposalCondition>();

    //for (int i = 1; i <= 2000; i++)
    //{
    //    LeaseProposal proposal = LeaseProposalGenerator.GenerateLeaseProposal(i);
    //    leaseProposals.Add(proposal);

    //    // Generate conditions for each proposal
    //    for (int j = 1; j <= 3; j++) // Assuming each proposal has 3 conditions
    //    {
    //        proposalConditions.Add(LeaseProposalGenerator.GenerateLeaseProposalCondition(proposalConditions.Count + 1, proposal.Id));
    //    }
    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\leaseproposals.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(leaseProposals);
    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\leaseproposalconditions.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(proposalConditions);
    //}

    //#endregion

    //#region CarsInit

    //var distinctCars = carSalesFromCSV
    //    .GroupBy(c => new { c.Make, c.Model, c.Year, c.Body, c.Color })
    //    .Select(g => g.First())
    //    .ToList();
    //var carsCsv = new List<Car>();
    //int i = 0;
    //foreach (var car in distinctCars)
    //{
    //    int price = 0;
    //    bool flag = int.TryParse(car.Mmr, out price);
    //    var carType = context.CarTypes.FirstOrDefault(e => e.Name == car.Body);
    //    var bodyType = context.CarBodyTypes.FirstOrDefault(e => e.Name == car.Body);
    //    var brand = context.Brands.FirstOrDefault(e => e.Name == car.Make);
    //    if (brand is not null && bodyType is not null && carType is not null)
    //        carsCsv.Add(new Car()
    //        {
    //            Id = ++i,
    //            CarTypeId = carType.Id,
    //            Doors = 4,
    //            Seats = 5,
    //            Year = car.Year,
    //            CarComfortOptions = new List<CarComfortOption>(),
    //            BodyType = bodyType,
    //            BodyTypeId = bodyType.Id,
    //            Brand = brand,
    //            BrandId = brand.Id,
    //            CarMultimediaOptions = new List<CarMultimediaOption>(),
    //            CarSafetyOptions = new List<CarSafetyOption>(),
    //            Color = context.Colors.FirstOrDefault(e => e.Name == car.Color) ?? context.Colors.FirstOrDefault(),
    //            Weight = Random.Shared.Next(1500, 5000),
    //            MaxSpeed = Random.Shared.Next(220, 350),
    //            GearsCount = Random.Shared.Next(6, 7),
    //            WheelsCount = 4,
    //            Model = car.Model,
    //            Price = flag ? price : 40000,
    //            Generation = Random.Shared.Next(1, 4).ToString(),
    //            Width = 1.5 + Random.Shared.NextDouble() / 10,
    //            Length = 4 + Random.Shared.NextDouble(),
    //            Height = 1.2 + Random.Shared.NextDouble(),
    //            FuelTankCapacity = Random.Shared.Next(40, 60),
    //            GearBoxId = (context.GearBoxTypes.FirstOrDefault(e => e.Name == car.Transmissive) ?? context.GearBoxTypes.FirstOrDefault()).Id,
    //            CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //            UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //        });
    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\cars.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(carsCsv);
    //}
    //#endregion

    //#region CarsInit


    //var dealershipcarsCsv = new List<DealershipCar>();
    //foreach (var car in context.Cars.ToList())
    //{
    //    dealershipcarsCsv.Add(new DealershipCar
    //    {
    //        Id = car.Id,
    //        CarsCount = 1,
    //        CarId = car.Id,
    //        CarStatusId = 2,
    //        DealershipId = Random.Shared.Next(1, context.AutoDealerships.Count()),
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\dealershipcars.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(dealershipcarsCsv);
    //}
    //#endregion

    //#region CarSalesInit

    //var carSalesCsv = new List<CarSale>();
    //var dealershipCars = context.DealershipCars.ToList();
    //int i = 0;

    //foreach (var car in dealershipCars)
    //{
    //    DateTime currentDate = DateTime.Today;

    //    // Get a random number of days between 0 and 3 years (365 days/year)
    //    int randomDays = Random.Shared.Next(0, 3 * 365);

    //    // Subtract the random number of days from the current date to get a random date in the last 3 years
    //    DateTime randomDate = currentDate.AddDays(-randomDays);
    //    var employeeId = car.DealershipId;
    //    carSalesCsv.Add(new CarSale()
    //    {
    //        Id = ++i,
    //        CustomerId = ++i,
    //        DealershipCarId = car.Id,
    //        EmployeeId = employeeId,
    //        SaleDate = DateOnly.FromDateTime(randomDate),
    //        StatusId = 1,
    //        ExpectedDeliveryDate = DateOnly.FromDateTime(randomDate),
    //        PaymentMethodId = 1,
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\carsales.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(carSalesCsv);
    //}
    //#endregion

    //#region DistributorsInit

    //int i = 0;
    //var brands = context.Brands.ToList();
    //var distributorsCsv = new List<Distributor>();

    //foreach (var brand in brands)
    //{
    //    distributorsCsv.Add(new Distributor
    //    {
    //        Id = brand.Id,
    //        DistributorName = brand.Name,
    //        DistributorAddress = CustomerGenerator.GenerateRandomAddress(),
    //        DistributorIdentifier = Guid.NewGuid().ToString(),
    //        CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //        UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //    });
    //}
    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\distributors.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(distributorsCsv);
    //}
    //#endregion

    //#region CarDeliveryInit

    //int i = 0;
    //var carSales = context.CarSales.Include(e => e.DealershipCar).ToList();
    //var carDeliveryCsv = new List<CarDelivery>();

    //foreach (var carSale in carSales)
    //{
    //    try
    //    {
    //        var car = context.Cars.Include(e => e.Brand).FirstOrDefault(e=> e.Id == carSale.DealershipCar.CarId);
    //        carDeliveryCsv.Add(new CarDelivery
    //        {
    //            Id = ++i,
    //            DeliveryCost = car.Price / 20,
    //            DeliveryDate = carSale.SaleDate,
    //            DistributorId = car.Brand.Id,
    //            SaleId = carSale.Id,
    //            CreateDate = DateOnly.FromDateTime(DateTime.Now),
    //            UpdateDate = DateOnly.FromDateTime(DateTime.Now)
    //        });
    //    }
    //    catch (Exception e)
    //    {
    //        continue;
    //    }

    //}

    //using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\cardeliveries.csv"))

    //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    //{
    //    csv.WriteRecords(carDeliveryCsv);
    //}

    //#endregion

    #region LeasesInit

    var leasesCsv = new List<Lease>();

  
    int maxCustomerId = 30000; // Assuming there are 100 customers
    int maxDealershipCarId = 10000; // Assuming there are 500 dealership cars

    for (int i = 1; i <= 2000; i++)
    {
        leasesCsv.Add(LeaseGenerator.GenerateLease(i, maxCustomerId, maxDealershipCarId));
    }

    using (var writer = new StreamWriter(@"C:\\Users\\Work\\Desktop\\csvFiles\\leases.csv"))

    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    {
        csv.WriteRecords(leasesCsv);
    }

    #endregion
}