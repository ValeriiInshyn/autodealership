using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTP_Seed.Dtos
{
    public class CarSalesCsvDto
    {
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public string Body { get; set; }
        public string Transmissive { get; set; }
        public string Vin { get; set; } 
        public string State { get; set; }
        public string Condition { get; set; }
        public string Odometer { get; set; }
        public string Color { get; set; }
        public string Interior { get; set; }
        public string Seller { get; set; }
        public string Mmr { get; set; }
        public string SellingPrice { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
