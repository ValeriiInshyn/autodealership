using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("CarDelivery", Schema = "dbo")]
    public partial class CarDelivery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int SaleId { get; set; }

        public CarSale CarSale { get; set; }

        [Required]
        public int DistributorId { get; set; }

        public Distributor Distributor { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public decimal DeliveryCost { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}