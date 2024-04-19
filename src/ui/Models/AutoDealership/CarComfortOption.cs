using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("CarComfortOptions", Schema = "dbo")]
    public partial class CarComfortOption
    {
        [Key]
        [Required]
        public int CarId { get; set; }

        public Car Car { get; set; }

        [Key]
        [Required]
        public int ComfortOptionId { get; set; }

        public ComfortOption ComfortOption { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}