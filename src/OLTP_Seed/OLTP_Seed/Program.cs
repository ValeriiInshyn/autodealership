using Microsoft.EntityFrameworkCore;
using OLTP_Seed.Generators;
using OLTP_Seed.Helpers;
using OLTP_Seed.Models;

using (var context = new AutoDealershipContext())
{
    // Deletes and creates db | Using for cleaning all data before initial seed
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    int i = 0;

    var carSalesFromCSV = CsvReader.ReadCarSalesFromCsv();

    #region ColorsInit
    var colorsToAdd = carSalesFromCSV.Select(e => e.Color).Distinct().ToList();

    for (i = 0; i < colorsToAdd.Count(); i++)
    {
        if (colorsToAdd[i].Length > 0 && (char)colorsToAdd[i][0] > 65 && (char)colorsToAdd[i][0] < 123)
        {
            context.Colors.Add(new Color
            {
                Id = i + 1,
                Name = colorsToAdd[i],
                CreateDate = DateOnly.FromDateTime(DateTime.Now),
                UpdateDate = DateOnly.FromDateTime(DateTime.Now)
            });
        }
    }
    context.SaveChanges();
    #endregion

    #region CountriesInit

    i = 0;
    foreach (var countryName in CountryGenerator.GenerateCountriesList())
    {
        context.Countries.Add(new Country()
        {
            Id = ++i,
            Name = countryName,
            CreateDate = DateOnly.FromDateTime(DateTime.Now),
            UpdateDate = DateOnly.FromDateTime(DateTime.Now)
        });
    }
    context.SaveChanges();
    #endregion

    #region CitiesInit

    i = 0;
    foreach (var cityByCountry in CitiesGenerator.GenerateCities())
    {
        var countryForCity = context.Countries.FirstOrDefault(e => e.Name == cityByCountry.Key);

        if (countryForCity is not null)
        {
            foreach (var city in cityByCountry.Value)
            {
                context.Cities.Add(new City()
                {
                    Id = ++i,
                    Name = city,
                    CountryId = countryForCity.Id,
                    Country = countryForCity,
                    CreateDate = DateOnly.FromDateTime(DateTime.Now),
                    UpdateDate = DateOnly.FromDateTime(DateTime.Now)
                });
            }
        }
    }
    context.SaveChanges();
    #endregion

    #region BrandsInit

    i = 0;
    var brandsToAdd = BrandDtoGenerator.GenerateBrandDtos();

    foreach (var brand in brandsToAdd)
    {
        var countryForBrand = context.Countries.FirstOrDefault(e => e.Name == brand.CountryName);

        if (countryForBrand is not null)
        {
            if (context.Brands.FirstOrDefault(e => e.Name == brand.Name) is null)
                context.Brands.Add(new Brand()
                {
                    Id = ++i,
                    Name = brand.Name,
                    Country = countryForBrand,
                    CountryId = countryForBrand.Id,
                    CreateDate = DateOnly.FromDateTime(DateTime.Now),
                    UpdateDate = DateOnly.FromDateTime(DateTime.Now)
                });
        }
    }

    context.SaveChanges();

    #endregion
}