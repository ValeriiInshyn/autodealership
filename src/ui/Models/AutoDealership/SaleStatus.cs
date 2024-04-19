using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("SaleStatus", Schema = "dbo")]
    public partial class SaleStatus
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public ICollection<CarSale> CarSales { get; set; }

    }
}