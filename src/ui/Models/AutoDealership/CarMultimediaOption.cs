using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("CarMultimediaOptions", Schema = "dbo")]
    public partial class CarMultimediaOption
    {
        [Key]
        [Required]
        public int CarId { get; set; }

        public Car Car { get; set; }

        [Key]
        [Required]
        public int MultimediaOptionId { get; set; }

        public MultimediaOption MultimediaOption { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}