using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLTP_Seed.Dtos;
using OLTP_Seed.Generators;

namespace OLTP_Seed.Helpers
{
    public static class CsvReader
    {
        public static List<CarSalesCsvDto> ReadCarSalesFromCsv()
        {
            List<CarSalesCsvDto> carSalesFromCsvResult = new();
            bool header = true;
            using (var reader = new StreamReader("car_prices.csv"))
            {
                while (!reader.EndOfStream)
                {
                    
                    var line = reader.ReadLine();
                    if (header)
                    {
                        header = false;
                        continue;
                    }

                    var values = line.Split(',');

                    string dateString;
                    DateTime carSaleDate;
                    try
                    {
                        dateString = values[15].Substring(4, 11);
                        carSaleDate = DateTime.ParseExact(dateString, "MMM dd yyyy", CultureInfo.InvariantCulture);
                    }
                    catch (Exception e)
                    {
                        carSaleDate =
                            DateGenerator.GenerateDateBetweenTwoDates(new DateTime(2014, 1, 1),
                                new DateTime(2016, 1, 1));
                    }


                    var carSale = new CarSalesCsvDto
                    {
                        Year = int.Parse(values[0]),
                        Make = values[1],
                        Model = values[2],
                        Trim = values[3],
                        Body = values[4],
                        Transmissive = values[5],
                        Vin = values[6],
                        State = values[7],
                        Condition = values[8],
                        Odometer = values[9],
                        Color = values[10],
                        Interior = values[11],
                        Seller = values[12],
                        Mmr = values[13],
                        SellingPrice = values[14],
                        SaleDate = carSaleDate
                    };

                    carSalesFromCsvResult.Add(carSale);
                }
            }

            return carSalesFromCsvResult;
        }
    }
}
