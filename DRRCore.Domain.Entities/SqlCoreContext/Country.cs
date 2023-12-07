﻿using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SqlCoreContext;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string EnglishName { get; set; } = null!;

    public string? Iso { get; set; }

    public string? FlagIso { get; set; }

    public bool? Flag { get; set; }

    public int? IdContinent { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public bool? Enable { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual Continent? IdContinentNavigation { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    public virtual ICollection<SubscriberPrice> SubscriberPrices { get; set; } = new List<SubscriberPrice>();

    public virtual ICollection<Subscriber> Subscribers { get; set; } = new List<Subscriber>();
}
