﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace OLTP_Seed.Models;

public partial class CarType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}