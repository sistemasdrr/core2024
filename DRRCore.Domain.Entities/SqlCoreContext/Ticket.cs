﻿using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SqlCoreContext;

public partial class Ticket
{
    public int Id { get; set; }

    public int Number { get; set; }

    public int? IdSubscriber { get; set; }

    public bool? RevealName { get; set; }

    public string? NameRevealed { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? Language { get; set; }

    public string? QueryCredit { get; set; }

    public string? TimeLimit { get; set; }

    public string? AditionalData { get; set; }

    public string About { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public DateTime ExpireDate { get; set; }

    public DateTime RealExpireDate { get; set; }

    public int? IdContinent { get; set; }

    public int? IdCountry { get; set; }

    public string ReportType { get; set; } = null!;

    public string ProcedureType { get; set; } = null!;

    public int? IdCompany { get; set; }

    public int? IdPerson { get; set; }

    public string? BusineesName { get; set; }

    public string? ComercialName { get; set; }

    public string? TaxType { get; set; }

    public string? TaxCode { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Telephone { get; set; }

    public int? Creditrisk { get; set; }

    public int? Status { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public bool? Enable { get; set; }

    public virtual Subscriber? IdSubscriberNavigation { get; set; }

    public virtual ICollection<TicketHistory> TicketHistories { get; set; } = new List<TicketHistory>();
}
