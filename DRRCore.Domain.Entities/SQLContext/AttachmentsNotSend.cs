using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SQLContext;

public partial class AttachmentsNotSend
{
    public int Id { get; set; }

    public int IdEmailHistory { get; set; }

    public string AttachmentsUrl { get; set; } = null!;

    public virtual EmailHistory IdEmailHistoryNavigation { get; set; } = null!;
}
