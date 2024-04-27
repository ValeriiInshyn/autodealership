using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealershipOLAP
{
    [Table("Dates", Schema = "dbo")]
    public partial class Date
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Day { get; set; }

        public ICollection<CarSale> CarSales { get; set; }

        public ICollection<CarSale> CarSales1 { get; set; }

        public ICollection<Lease> Leases { get; set; }

        public ICollection<Lease> Leases1 { get; set; }

        public ICollection<Lease> Leases2 { get; set; }

    }
}