using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("CarSales", Schema = "dbo")]
    public partial class CarSale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int DealershipCarId { get; set; }

        public DealershipCar DealershipCar { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        public int? StatusId { get; set; }

        public SaleStatus SaleStatus { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public int? EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public int? PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public ICollection<CarDelivery> CarDeliveries { get; set; }


    }
}