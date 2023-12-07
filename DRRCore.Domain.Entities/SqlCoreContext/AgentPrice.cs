﻿using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SqlCoreContext;

public partial class AgentPrice
{
    public int Id { get; set; }

    public int? IdAgent { get; set; }

    public DateTime? Date { get; set; }

    public int? IdContinent { get; set; }

    public int? IdCountry { get; set; }

    public int? IdCurrency { get; set; }

    public int? PriceT1 { get; set; }

    public int? DayT1 { get; set; }

    public int? PriceT2 { get; set; }

    public int? DayT2 { get; set; }

    public int? PriceT3 { get; set; }

    public int? DayT3 { get; set; }

    public int? PricePn { get; set; }

    public int? DayPn { get; set; }

    public int? PriceBd { get; set; }

    public int? DayBd { get; set; }

    public int? PriceRp { get; set; }

    public int? DayRp { get; set; }

    public int? PriceCr { get; set; }

    public int? DayCr { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public int? LastUpdateUser { get; set; }

    public bool? Enable { get; set; }

    public virtual Agent? IdAgentNavigation { get; set; }

    public virtual Continent? IdContinentNavigation { get; set; }

    public virtual Country? IdCountryNavigation { get; set; }

    public virtual Currency? IdCurrencyNavigation { get; set; }
}
