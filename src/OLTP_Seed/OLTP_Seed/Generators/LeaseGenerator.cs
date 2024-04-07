using OLTP_Seed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTP_Seed.Generators
{
    public static class LeaseGenerator
    {
        private static Random random = new Random();

        public static Lease GenerateLease(int id, int maxCustomerId, int maxDealershipCarId)
        {
            Lease lease = new Lease();
            lease.Id = id;
            lease.EmployeeId = random.Next(1, 11); // Assuming there are 10 employees
            lease.ProposalId = random.Next(1, 11); // Assuming there are 10 proposals
            lease.CustomerId = random.Next(1, maxCustomerId + 1);
            lease.DealershipCarId = random.Next(1, maxDealershipCarId + 1);
            lease.LeaseSignDate = GetRandomDate();
            lease.LeaseStartDate = lease.LeaseSignDate.AddDays(random.Next(1, 30)); // Lease starts 1 to 30 days after sign date
            lease.LeaseEndDate = lease.LeaseStartDate.AddDays(random.Next(90, 365)); // Lease duration 3 months to 1 year
            lease.LeaseUniqueNumber =id.ToString(); // Generating a unique lease number
            lease.TotalPrice = Math.Round((decimal)(random.NextDouble() * 50000), 2); // Random total price up to $50,000
            lease.Description = "Empty Description "; // Sample description
            lease.CreateDate = GetRandomDate();
            lease.UpdateDate = lease.CreateDate; // Assuming update date is same as create date
            return lease;
        }

        private static DateOnly GetRandomDate()
        {
            DateTime start = DateTime.Now.AddYears(-3); // Start date is 3 years ago
            int range = (DateTime.Today - start).Days; // Range in days
            return DateOnly.FromDateTime(start.AddDays(random.Next(range))); // Random date within the last 3 years
        }
    }
}
