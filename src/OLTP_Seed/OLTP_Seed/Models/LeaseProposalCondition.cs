﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace OLTP_Seed.Models;

public partial class LeaseProposalCondition
{
    public int LeaseProposalId { get; set; }

    public int ConditionId { get; set; }

    public bool Value { get; set; }

    public int Id { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public virtual Condition Condition { get; set; }

    public virtual LeaseProposal LeaseProposal { get; set; }
}