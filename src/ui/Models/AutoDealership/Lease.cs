using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("Leases", Schema = "dbo")]
    public partial class Lease
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public int? ProposalId { get; set; }

        public LeaseProposal LeaseProposal { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public int DealershipCarId { get; set; }

        public DealershipCar DealershipCar { get; set; }

        [Required]
        public DateTime LeaseSignDate { get; set; }

        [Required]
        public DateTime LeaseStartDate { get; set; }

        [Required]
        public DateTime LeaseEndDate { get; set; }

        public string LeaseUniqueNumber { get; set; }

        public decimal? TotalPrice { get; set; }

        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}