﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace OLTP_Seed.Models;

public partial class MultimediaOption
{
    public int Id { get; set; }

    public string OptionName { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public virtual ICollection<CarMultimediaOption> CarMultimediaOptions { get; set; } = new List<CarMultimediaOption>();
}