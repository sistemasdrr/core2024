using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SqlCoreContext;

public partial class TicketHistory
{
    public int Id { get; set; }

    public int? IdTicket { get; set; }

    public int? Status { get; set; }

    public string? UserFrom { get; set; }

    public string? UserTo { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public bool? Enable { get; set; }

    public virtual Ticket? IdTicketNavigation { get; set; }
}
