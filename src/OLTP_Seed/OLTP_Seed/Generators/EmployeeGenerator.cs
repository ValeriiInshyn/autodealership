using OLTP_Seed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTP_Seed.Generators
{
    public static class EmployeeGenerator
    {
        private static Random random = new Random();

        private static string[] firstNames = { "John", "Jane", "David", "Emily", "Michael", "Sarah", "Matthew", "Jennifer", "Christopher", "Jessica" };
        private static string[] lastNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor" };
        private static string[] domains = { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com" };

        public static Employee GenerateEmployee(int id, int dealershipId)
        {
            Employee employee = new Employee();
            employee.Id = id;
            employee.FirstName = firstNames[random.Next(firstNames.Length)];
            employee.LastName = lastNames[random.Next(lastNames.Length)];
            employee.Email = $"{employee.FirstName.ToLower()}.{employee.LastName.ToLower()}@{domains[random.Next(domains.Length)]}";
            employee.Phone = GenerateRandomPhoneNumber();
            employee.DealershipId = dealershipId;
            employee.CreateDate = DateOnly.FromDateTime(DateTime.Now);
            employee.UpdateDate = DateOnly.FromDateTime(DateTime.Now);
            return employee;
        }

        private static string GenerateRandomPhoneNumber()
        {
            return $"{random.Next(100, 999)}-{random.Next(100, 999)}-{random.Next(1000, 9999)}";
        }
    }
}
