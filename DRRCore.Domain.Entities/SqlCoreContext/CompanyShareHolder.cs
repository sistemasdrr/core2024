using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SqlCoreContext;

public partial class CompanyShareHolder
{
    public int Id { get; set; }

    public int? IdCompany { get; set; }

    public int? IdCompanyShareHolder { get; set; }

    public string? Relation { get; set; }

    public int? Participation { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? LastUpdateUser { get; set; }

    public DateTime? DeleteDate { get; set; }

    public bool? Enable { get; set; }

    public virtual Company? IdCompanyNavigation { get; set; }

    public virtual Company? IdCompanyShareHolderNavigation { get; set; }
}
