using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SqlCoreContext;

public partial class Company
{
    public int Id { get; set; }

    public string? OldCode { get; set; }

    public string Name { get; set; } = null!;

    public string? SocialName { get; set; }

    public DateTime? LastSearched { get; set; }

    public string Language { get; set; } = null!;

    public string? TypeRegister { get; set; }

    public string? YearFundation { get; set; }

    public DateTime? ConstitutionDate { get; set; }

    public string? Quality { get; set; }

    public int? IdLegalPersonType { get; set; }

    public string? TaxTypeName { get; set; }

    public string? TaxTypeCode { get; set; }

    public int? IdLegalRegisterSituation { get; set; }

    public string? Address { get; set; }

    public string? Duration { get; set; }

    public string? Place { get; set; }

    public int? IdCountry { get; set; }

    public string? SubTelephone { get; set; }

    public string? Tellphone { get; set; }

    public string? Cellphone { get; set; }

    public string? Telephone { get; set; }

    public string? PostalCode { get; set; }

    public string? WhatsappPhone { get; set; }

    public string? Email { get; set; }

    public string? WebPage { get; set; }

    public int? IdCreditRisk { get; set; }

    public int? IdPaymentPolicy { get; set; }

    public int? IdReputation { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? LastUpdaterUser { get; set; }

    public DateTime? DeleteDate { get; set; }

    public bool? Enable { get; set; }

    public string? ReputationComentary { get; set; }

    public string? NewsComentary { get; set; }

    public string? IdentificacionCommentary { get; set; }

    public virtual ICollection<CompanyBackground> CompanyBackgrounds { get; set; } = new List<CompanyBackground>();

    public virtual ICollection<CompanyBranch> CompanyBranches { get; set; } = new List<CompanyBranch>();

    public virtual Country? IdCountryNavigation { get; set; }

    public virtual CreditRisk? IdCreditRiskNavigation { get; set; }

    public virtual LegalPersonType? IdLegalPersonTypeNavigation { get; set; }

    public virtual LegalRegisterSituation? IdLegalRegisterSituationNavigation { get; set; }

    public virtual PaymentPolicy? IdPaymentPolicyNavigation { get; set; }

    public virtual Reputation? IdReputationNavigation { get; set; }

    public virtual ICollection<Traduction> Traductions { get; set; } = new List<Traduction>();
}
