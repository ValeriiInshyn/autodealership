using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealershipOLAP
{
    [Table("Cars", Schema = "dbo")]
    public partial class Car
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        [Required]
        public string Generation { get; set; }
        [NotMapped]
        public string FullName => $"{Brand.Name} {Model}";

        public ICollection<Lease> Leases { get; set; }

    }
}