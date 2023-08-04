using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SQLContext;

public partial class EmailConfiguration
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Value { get; set; } = null!;

    public DateTime? InsertDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string InsertUser { get; set; } = null!;

    public string? UpdateUser { get; set; }

    public bool? Enable { get; set; }
}
