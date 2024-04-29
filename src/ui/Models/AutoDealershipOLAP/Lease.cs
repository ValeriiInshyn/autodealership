using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealershipOLAP
{
    [Table("Leases", Schema = "dbo")]
    public partial class Lease
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        public Car Car { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int PreviousLeaseModifyPercent { get; set; }

        [Required]
        public int LeaseSignDateId { get; set; }

        public Date Date1 { get; set; }

        [Required]
        public int LeaseStartDateId { get; set; }

        public Date Date2 { get; set; }

        [Required]
        public int LeaseEndDateId { get; set; }

        public Date Date { get; set; }

        public int? LastLeaseId { get; set; }

    }
}