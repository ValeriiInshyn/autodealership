using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork.Models.AutoDealership
{
    [Table("LeaseProposalConditions", Schema = "dbo")]
    public partial class LeaseProposalCondition
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int LeaseProposalId { get; set; }

        public LeaseProposal LeaseProposal { get; set; }

        [Required]
        public int ConditionId { get; set; }

        public Condition Condition { get; set; }

        public bool Value { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}