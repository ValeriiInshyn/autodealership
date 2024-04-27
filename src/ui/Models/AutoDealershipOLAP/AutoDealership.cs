using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealershipOLAP
{
    [Table("AutoDealerships", Schema = "dbo")]
    public partial class AutoDealership
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<CarSale> CarSales { get; set; }

    }
}