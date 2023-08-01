using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SQLContext;

public partial class EmailUser
{
    public int Id { get; set; }

    public string UserCode { get; set; } = null!;

    public Guid? Identifier { get; set; }

    public string UserName { get; set; } = null!;

    public string LoginName { get; set; } = null!;
}
