using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("LeaseProposals", Schema = "dbo")]
    public partial class LeaseProposal
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int LeaseTypeId { get; set; }

        public LeaseType LeaseType { get; set; }

        public bool InsuranceRequired { get; set; }

        [Required]
        public decimal MonthlyPayment { get; set; }

        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public ICollection<LeaseProposalCondition> LeaseProposalConditions { get; set; }

        public ICollection<Lease> Leases { get; set; }

    }
}