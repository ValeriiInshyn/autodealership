﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace OLTP_Seed.Models;

public partial class CarSafetyOption
{
    public int CarId { get; set; }

    public int SafetyOptionId { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public virtual Car Car { get; set; }

    public virtual SafetyOption SafetyOption { get; set; }
}