using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("Engines", Schema = "dbo")]
    public partial class Engine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        [Required]
        public int EngineTypeId { get; set; }

        public EngineType EngineType { get; set; }

        [Required]
        public decimal EngineVolume { get; set; }

        [Required]
        public int EnginePower { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public ICollection<Car> Cars { get; set; }

    }
}