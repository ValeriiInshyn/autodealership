using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("Cars", Schema = "dbo")]
    public partial class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CarTypeId { get; set; }

        [Required]
        public int Doors { get; set; }

        [Required]
        public int Seats { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int ColorId { get; set; }

        public Color Color { get; set; }

        [Required]
        public int BodyTypeId { get; set; }

        public CarBodyType CarBodyType { get; set; }

        [Required]
        public int EngineId { get; set; }

        public Engine Engine { get; set; }

        public int? BrandId { get; set; }

        public Brand Brand { get; set; }

        public string Model { get; set; }

        public string Generation { get; set; }

        public int? Weight { get; set; }

        public int? MaxSpeed { get; set; }

        public int? GearsCount { get; set; }

        public double? Height { get; set; }

        public double? Width { get; set; }

        public double? Length { get; set; }

        public int? GearBoxId { get; set; }

        public GearBoxType GearBoxType { get; set; }

        public int? FuelTankCapacity { get; set; }

        public int? WheelsCount { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
        [NotMapped] 
        public string FullName => $"{Brand.Name} {Model}";

        public ICollection<CarComfortOption> CarComfortOptions { get; set; }

        public ICollection<CarMultimediaOption> CarMultimediaOptions { get; set; }

        public ICollection<CarSafetyOption> CarSafetyOptions { get; set; }

        public ICollection<DealershipCar> DealershipCars { get; set; }
  

    }
}