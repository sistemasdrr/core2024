using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SqlCoreContext;

public partial class Employee
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Cellphone { get; set; }

    public string? Email { get; set; }

    public string? Birthday { get; set; }

    public int? IdDocumentType { get; set; }

    public string DocumentNumber { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Photo { get; set; }

    public int? IdJob { get; set; }

    public int? IdCountry { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public bool? Enable { get; set; }

    public virtual Country? IdCountryNavigation { get; set; }

    public virtual DocumentType? IdDocumentTypeNavigation { get; set; }

    public virtual Job? IdJobNavigation { get; set; }

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
