using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SqlCoreContext;

public partial class CompanyPartner
{
    public int Id { get; set; }

    public int? IdCompany { get; set; }

    public int? IdPerson { get; set; }

    public bool? MainExecutive { get; set; }

    public int? IdProfession { get; set; }

    public int? Participation { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? LastUpdateUser { get; set; }

    public DateTime? DeleteDate { get; set; }

    public bool? Enable { get; set; }

    public virtual Company? IdCompanyNavigation { get; set; }

    public virtual Person? IdPersonNavigation { get; set; }

    public virtual Profession? IdProfessionNavigation { get; set; }
}
