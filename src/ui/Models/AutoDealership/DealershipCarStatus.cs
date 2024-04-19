using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("DealershipCarStatuses", Schema = "dbo")]
    public partial class DealershipCarStatus
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public ICollection<DealershipCar> DealershipCars { get; set; }

    }
}