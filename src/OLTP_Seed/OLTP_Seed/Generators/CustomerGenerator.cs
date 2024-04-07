using OLTP_Seed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTP_Seed.Generators
{
    public class CustomerGenerator
    {
        private static Random random = new Random();
        private static List<string> firstNames = new List<string> { "John", "Jane", "Robert", "Emily", "Michael", "Susan", "William", "Jennifer", "David", "Mary" };
        private static List<string> lastNames = new List<string> { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor" };
        private static List<string> domains = new List<string> { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com" };

        public static Customer GenerateCustomer(int id)
        {
            Customer customer = new Customer();
            customer.Id = id;
            customer.FirstName = firstNames[random.Next(firstNames.Count)];
            customer.LastName = lastNames[random.Next(lastNames.Count)];
            customer.Email = $"{customer.FirstName.ToLower()}.{customer.LastName.ToLower()}@{domains[random.Next(domains.Count)]}";
            customer.Phone = GenerateRandomPhoneNumber();
            customer.Address = GenerateRandomAddress();
            customer.CreateDate = DateOnly.FromDateTime(DateTime.Now);
            customer.UpdateDate = DateOnly.FromDateTime(DateTime.Now);
            return customer;
        }

        private static string GenerateRandomPhoneNumber()
        {
            return $"{random.Next(100, 999)}-{random.Next(100, 999)}-{random.Next(1000, 9999)}";
        }

        public static string GenerateRandomAddress()
        {
            return $"{random.Next(100, 999)}-{GetRandomStreet()}-{random.Next(1, 100)}-{GetRandomCity()}-{GetRandomState()}-{random.Next(10000, 99999)}";
        }

        private static string GetRandomStreet()
        {
            List<string> streets = new List<string> { "Main St", "Broadway", "Elm St", "Park Ave", "Maple St", "Oak St", "Cedar St", "Washington St", "Pine St", "High St" };
            return streets[random.Next(streets.Count)];
        }

        private static string GetRandomCity()
        {
            List<string> cities = new List<string> { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose" };
            return cities[random.Next(cities.Count)];
        }

        private static string GetRandomState()
        {
            List<string> states = new List<string> { "NY", "CA", "IL", "TX", "AZ", "PA", "TX", "CA", "TX", "CA" };
            return states[random.Next(states.Count)];
        }
    }
}
