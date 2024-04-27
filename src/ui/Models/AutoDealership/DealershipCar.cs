using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("DealershipCars", Schema = "dbo")]
    public partial class DealershipCar
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        public Car Car { get; set; }

        public int? DealershipId { get; set; }

        public AutoDealership AutoDealership { get; set; }

        public int? CarsCount { get; set; }

        public int? CarStatusId { get; set; }

        public DealershipCarStatus DealershipCarStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public ICollection<CarSale> CarSales { get; set; }

        public ICollection<Lease> Leases { get; set; }

    }
}