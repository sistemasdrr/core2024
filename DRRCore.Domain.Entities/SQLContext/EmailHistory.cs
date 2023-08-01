using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SQLContext;

public partial class EmailHistory
{
    public int Id { get; set; }

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime? Date { get; set; }

    public string FromMails { get; set; } = null!;

    public string ToMails { get; set; } = null!;

    public string? CcMails { get; set; }

    public string? CcoMails { get; set; }

    public string Domain { get; set; } = null!;

    public bool? WasSent { get; set; }

    public DateTime? InsertDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public Guid InsertUser { get; set; }

    public Guid? UpdateUser { get; set; }

    public bool? Enable { get; set; }
}
