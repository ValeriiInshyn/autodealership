using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("Brands", Schema = "dbo")]
    public partial class Brand
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? CountryId { get; set; }

        public Country Country { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public ICollection<Car> Cars { get; set; }

        public ICollection<Engine> Engines { get; set; }

    }
}