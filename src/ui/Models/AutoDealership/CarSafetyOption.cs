using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("CarSafetyOptions", Schema = "dbo")]
    public partial class CarSafetyOption
    {
        [Key]
        [Required]
        public int CarId { get; set; }

        public Car Car { get; set; }

        [Key]
        [Required]
        public int SafetyOptionId { get; set; }

        public SafetyOption SafetyOption { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}