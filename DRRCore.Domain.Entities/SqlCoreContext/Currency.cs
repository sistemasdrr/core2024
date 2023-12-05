using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SqlCoreContext;

public partial class Currency
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Abreviation { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public int? Level { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public bool? Enable { get; set; }

    public virtual ICollection<SubscriberPrice> SubscriberPrices { get; set; } = new List<SubscriberPrice>();

    public virtual ICollection<Subscriber> Subscribers { get; set; } = new List<Subscriber>();
}
