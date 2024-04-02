﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace OLTP_Seed.Models;

public partial class Engine
{
    public int Id { get; set; }

    public int BrandId { get; set; }

    public int EngineTypeId { get; set; }

    public decimal EngineVolume { get; set; }

    public int EnginePower { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public virtual Brand Brand { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual EngineType EngineType { get; set; }
}