﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Domain.Entities.SqlCoreContext;

public partial class DbA9ccf0EecoreContext : DbContext
{
    public DbA9ccf0EecoreContext()
    {
    }

    public DbA9ccf0EecoreContext(DbContextOptions<DbA9ccf0EecoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anniversary> Anniversaries { get; set; }

    public virtual DbSet<BankAccountType> BankAccountTypes { get; set; }

    public virtual DbSet<BranchSector> BranchSectors { get; set; }

    public virtual DbSet<BusineesActivity> BusineesActivities { get; set; }

    public virtual DbSet<BusinessBranch> BusinessBranches { get; set; }

    public virtual DbSet<CivilStatus> CivilStatuses { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyBackground> CompanyBackgrounds { get; set; }

    public virtual DbSet<CompanyBranch> CompanyBranches { get; set; }

    public virtual DbSet<CompanyBusineesActivity> CompanyBusineesActivities { get; set; }

    public virtual DbSet<Continent> Continents { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CreditRisk> CreditRisks { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<FamilyBondType> FamilyBondTypes { get; set; }

    public virtual DbSet<HealthInsurance> HealthInsurances { get; set; }

    public virtual DbSet<HistoryInfoChange> HistoryInfoChanges { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobDepartment> JobDepartments { get; set; }

    public virtual DbSet<LandOwnership> LandOwnerships { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<LegalPersonType> LegalPersonTypes { get; set; }

    public virtual DbSet<LegalRegisterSituation> LegalRegisterSituations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PaymentPolicy> PaymentPolicies { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Process> Processes { get; set; }

    public virtual DbSet<Reputation> Reputations { get; set; }

    public virtual DbSet<Subscriber> Subscribers { get; set; }

    public virtual DbSet<SubscriberPrice> SubscriberPrices { get; set; }

    public virtual DbSet<Traduction> Traductions { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserProcess> UserProcesses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=sql5103.site4now.net;Initial Catalog=db_a9ccf0_eecore;User ID=db_a9ccf0_eecore_admin;Password=drrti2023;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anniversary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Annivers__3213E83FEB9CDB75");

            entity.ToTable("Anniversary");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<BankAccountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BankAcco__3213E83F30664FC1");

            entity.ToTable("BankAccountType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<BranchSector>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BranchSe__3213E83FD65301CA");

            entity.ToTable("BranchSector");

            entity.HasIndex(e => e.Name, "UQ__BranchSe__72E12F1BC914DA1A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("englishName");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<BusineesActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Businees__3213E83F3B6533B9");

            entity.ToTable("BusineesActivity");

            entity.HasIndex(e => e.Name, "UQ__Businees__72E12F1B1F5F2F87").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("englishName");
            entity.Property(e => e.IdBusinessBranch).HasColumnName("idBusinessBranch");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdBusinessBranchNavigation).WithMany(p => p.BusineesActivities)
                .HasForeignKey(d => d.IdBusinessBranch)
                .HasConstraintName("FK__BusineesA__idBus__0CA5D9DE");
        });

        modelBuilder.Entity<BusinessBranch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Business__3213E83F36843515");

            entity.ToTable("BusinessBranch");

            entity.HasIndex(e => e.Name, "UQ__Business__72E12F1B16C7234A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("englishName");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<CivilStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CivilSta__3213E83F2E066409");

            entity.ToTable("CivilStatus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Company__3213E83F837E910E");

            entity.ToTable("Company");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Cellphone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cellphone");
            entity.Property(e => e.ConstitutionDate)
                .HasColumnType("datetime")
                .HasColumnName("constitutionDate");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Duration)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("duration");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.IdCountry).HasColumnName("idCountry");
            entity.Property(e => e.IdCreditRisk).HasColumnName("idCreditRisk");
            entity.Property(e => e.IdLegalPersonType).HasColumnName("idLegalPersonType");
            entity.Property(e => e.IdLegalRegisterSituation).HasColumnName("idLegalRegisterSituation");
            entity.Property(e => e.IdPaymentPolicy).HasColumnName("idPaymentPolicy");
            entity.Property(e => e.IdReputation).HasColumnName("idReputation");
            entity.Property(e => e.IdentificacionCommentary)
                .IsUnicode(false)
                .HasColumnName("identificacionCommentary");
            entity.Property(e => e.Language)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("language");
            entity.Property(e => e.LastSearched)
                .HasColumnType("datetime")
                .HasColumnName("lastSearched");
            entity.Property(e => e.LastUpdaterUser).HasColumnName("lastUpdaterUser");
            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NewsComentary)
                .IsUnicode(false)
                .HasColumnName("newsComentary");
            entity.Property(e => e.OldCode)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("oldCode");
            entity.Property(e => e.OnWeb)
                .HasDefaultValueSql("((1))")
                .HasColumnName("onWeb");
            entity.Property(e => e.Place)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("place");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("postalCode");
            entity.Property(e => e.Quality)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("quality");
            entity.Property(e => e.ReputationComentary)
                .IsUnicode(false)
                .HasColumnName("reputationComentary");
            entity.Property(e => e.SocialName)
                .IsUnicode(false)
                .HasColumnName("socialName");
            entity.Property(e => e.SubTelephone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("subTelephone");
            entity.Property(e => e.TaxTypeCode)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("taxTypeCode");
            entity.Property(e => e.TaxTypeName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("taxTypeName");
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telephone");
            entity.Property(e => e.Tellphone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tellphone");
            entity.Property(e => e.TypeRegister)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("typeRegister");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
            entity.Property(e => e.WebPage)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("webPage");
            entity.Property(e => e.WhatsappPhone)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("whatsappPhone");
            entity.Property(e => e.YearFundation)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("yearFundation");

            entity.HasOne(d => d.IdCountryNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.IdCountry)
                .HasConstraintName("FK__Company__idCount__60C757A0");

            entity.HasOne(d => d.IdCreditRiskNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.IdCreditRisk)
                .HasConstraintName("FK__Company__idCredi__61BB7BD9");

            entity.HasOne(d => d.IdLegalPersonTypeNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.IdLegalPersonType)
                .HasConstraintName("FK__Company__idLegal__5EDF0F2E");

            entity.HasOne(d => d.IdLegalRegisterSituationNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.IdLegalRegisterSituation)
                .HasConstraintName("FK__Company__idLegal__5FD33367");

            entity.HasOne(d => d.IdPaymentPolicyNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.IdPaymentPolicy)
                .HasConstraintName("FK__Company__idPayme__62AFA012");

            entity.HasOne(d => d.IdReputationNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.IdReputation)
                .HasConstraintName("FK__Company__idReput__63A3C44B");
        });

        modelBuilder.Entity<CompanyBackground>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompanyB__3213E83F6BF053B5");

            entity.ToTable("CompanyBackground");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Background)
                .IsUnicode(false)
                .HasColumnName("background");
            entity.Property(e => e.ConstitutionDate)
                .HasColumnType("datetime")
                .HasColumnName("constitutionDate");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.CurrentExchangeRate)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("currentExchangeRate");
            entity.Property(e => e.CurrentPaidCapital)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("currentPaidCapital");
            entity.Property(e => e.CurrentPaidCapitalComentary)
                .IsUnicode(false)
                .HasColumnName("currentPaidCapitalComentary");
            entity.Property(e => e.CurrentPaidCapitalCurrency).HasColumnName("currentPaidCapitalCurrency");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.History)
                .IsUnicode(false)
                .HasColumnName("history");
            entity.Property(e => e.IdCompany).HasColumnName("idCompany");
            entity.Property(e => e.IncreaceDateCapital)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("increaceDateCapital");
            entity.Property(e => e.LastQueryRrpp)
                .HasColumnType("datetime")
                .HasColumnName("lastQueryRrpp");
            entity.Property(e => e.LastQueryRrppBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastQueryRrppBy");
            entity.Property(e => e.NotaryRegister)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("notaryRegister");
            entity.Property(e => e.OperationDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("operationDuration");
            entity.Property(e => e.Origin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("origin");
            entity.Property(e => e.PublicRegister)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("publicRegister");
            entity.Property(e => e.RegisterPlace)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("registerPlace");
            entity.Property(e => e.StartFunctionYear)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("startFunctionYear");
            entity.Property(e => e.Traded).HasColumnName("traded");
            entity.Property(e => e.TradedBy)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tradedBy");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdCompanyNavigation).WithMany(p => p.CompanyBackgrounds)
                .HasForeignKey(d => d.IdCompany)
                .HasConstraintName("FK__CompanyBa__idCom__73DA2C14");
        });

        modelBuilder.Entity<CompanyBranch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompanyB__3213E83F25EE4BF5");

            entity.ToTable("CompanyBranch");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AbroadSaleComentary)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("abroadSaleComentary");
            entity.Property(e => e.AbroadSalePercentage)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("abroadSalePercentage");
            entity.Property(e => e.ActivityDetailCommentary)
                .IsUnicode(false)
                .HasColumnName("activityDetailCommentary");
            entity.Property(e => e.AditionalCommentary)
                .IsUnicode(false)
                .HasColumnName("aditionalCommentary");
            entity.Property(e => e.CashSaleComentary)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cashSaleComentary");
            entity.Property(e => e.CashSalePercentage)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cashSalePercentage");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.CreditSaleComentary)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("creditSaleComentary");
            entity.Property(e => e.CreditSalePercentage)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("creditSalePercentage");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.Export).HasColumnName("export");
            entity.Property(e => e.IdBranchSector).HasColumnName("idBranchSector");
            entity.Property(e => e.IdBusinessBranch).HasColumnName("idBusinessBranch");
            entity.Property(e => e.IdCompany).HasColumnName("idCompany");
            entity.Property(e => e.IdLandOwnership).HasColumnName("idLandOwnership");
            entity.Property(e => e.Import).HasColumnName("import");
            entity.Property(e => e.InternationalPurchasesComentary)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("internationalPurchasesComentary");
            entity.Property(e => e.InternationalPurchasesPercentage)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("internationalPurchasesPercentage");
            entity.Property(e => e.NationalPurchasesComentary)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nationalPurchasesComentary");
            entity.Property(e => e.NationalPurchasesPercentage)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("nationalPurchasesPercentage");
            entity.Property(e => e.OtherLocations)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("otherLocations");
            entity.Property(e => e.PreviousAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("previousAddress");
            entity.Property(e => e.TabCommentary)
                .IsUnicode(false)
                .HasColumnName("tabCommentary");
            entity.Property(e => e.TerritorySaleComentary)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("territorySaleComentary");
            entity.Property(e => e.TerritorySalePercentage)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("territorySalePercentage");
            entity.Property(e => e.TotalArea)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("totalArea");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
            entity.Property(e => e.WorkerNumber).HasColumnName("workerNumber");

            entity.HasOne(d => d.IdBranchSectorNavigation).WithMany(p => p.CompanyBranches)
                .HasForeignKey(d => d.IdBranchSector)
                .HasConstraintName("FK__CompanyBr__idBra__1AF3F935");

            entity.HasOne(d => d.IdBusinessBranchNavigation).WithMany(p => p.CompanyBranches)
                .HasForeignKey(d => d.IdBusinessBranch)
                .HasConstraintName("FK__CompanyBr__idBus__1BE81D6E");

            entity.HasOne(d => d.IdCompanyNavigation).WithMany(p => p.CompanyBranches)
                .HasForeignKey(d => d.IdCompany)
                .HasConstraintName("FK__CompanyBr__idCom__19FFD4FC");

            entity.HasOne(d => d.IdLandOwnershipNavigation).WithMany(p => p.CompanyBranches)
                .HasForeignKey(d => d.IdLandOwnership)
                .HasConstraintName("FK__CompanyBr__idLan__1CDC41A7");
        });

        modelBuilder.Entity<CompanyBusineesActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompanyB__3213E83F188717B4");

            entity.ToTable("CompanyBusineesActivity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.IdCompanyBranch).HasColumnName("idCompanyBranch");
            entity.Property(e => e.IdCountry).HasColumnName("idCountry");
            entity.Property(e => e.ImportOrExport)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("importOrExport");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdCompanyBranchNavigation).WithMany(p => p.CompanyBusineesActivities)
                .HasForeignKey(d => d.IdCompanyBranch)
                .HasConstraintName("FK__CompanyBu__idCom__21A0F6C4");
        });

        modelBuilder.Entity<Continent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Continen__3213E83F1D74F7BC");

            entity.ToTable("Continent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("englishName");
            entity.Property(e => e.Flag)
                .HasDefaultValueSql("((0))")
                .HasColumnName("flag");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3213E83F1BD5B3FF");

            entity.ToTable("Country");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("englishName");
            entity.Property(e => e.Flag)
                .HasDefaultValueSql("((0))")
                .HasColumnName("flag");
            entity.Property(e => e.FlagIso)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("flagIso");
            entity.Property(e => e.IdContinent).HasColumnName("idContinent");
            entity.Property(e => e.Iso)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("iso");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdContinentNavigation).WithMany(p => p.Countries)
                .HasForeignKey(d => d.IdContinent)
                .HasConstraintName("FK__Country__idConti__79C80F94");
        });

        modelBuilder.Entity<CreditRisk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CreditRi__3213E83F1FBB14F4");

            entity.ToTable("CreditRisk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abreviation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("abreviation");
            entity.Property(e => e.Color)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("englishName");
            entity.Property(e => e.Flag)
                .HasDefaultValueSql("((0))")
                .HasColumnName("flag");
            entity.Property(e => e.Identifier)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("identifier");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Currency__3213E83FC807D727");

            entity.ToTable("Currency");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abreviation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("abreviation");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Symbol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("symbol");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3213E83F64930AF5");

            entity.ToTable("DocumentType");

            entity.HasIndex(e => e.Name, "UQ__Document__72E12F1B5F98605B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abreviation)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("abreviation");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.Flag1)
                .HasDefaultValueSql("((0))")
                .HasColumnName("flag1");
            entity.Property(e => e.Flag2)
                .HasDefaultValueSql("((0))")
                .HasColumnName("flag2");
            entity.Property(e => e.Flag3)
                .HasDefaultValueSql("((0))")
                .HasColumnName("flag3");
            entity.Property(e => e.IsLegal)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isLegal");
            entity.Property(e => e.IsNational)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isNational");
            entity.Property(e => e.IsNatural)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isNatural");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83FE8D94EB9");

            entity.ToTable("Employee");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Birthday)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("birthday");
            entity.Property(e => e.BloodType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("bloodType");
            entity.Property(e => e.Cellphone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cellphone");
            entity.Property(e => e.ChildrenNumber).HasColumnName("childrenNumber");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.CtsBank)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ctsBank");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Distrit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("distrit");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("documentNumber");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EmergencyPhone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("emergencyPhone");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.IdBankAccountTypeCts).HasColumnName("idBankAccountTypeCts");
            entity.Property(e => e.IdBankAccountTypeSalary).HasColumnName("idBankAccountTypeSalary");
            entity.Property(e => e.IdCivilStatus).HasColumnName("idCivilStatus");
            entity.Property(e => e.IdCountry).HasColumnName("idCountry");
            entity.Property(e => e.IdCurrencyCts).HasColumnName("idCurrencyCts");
            entity.Property(e => e.IdCurrencySalary).HasColumnName("idCurrencySalary");
            entity.Property(e => e.IdDocumentType).HasColumnName("idDocumentType");
            entity.Property(e => e.IdJob).HasColumnName("idJob");
            entity.Property(e => e.IdJobDepartment).HasColumnName("idJobDepartment");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.NumberAccountCts)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("numberAccountCts");
            entity.Property(e => e.NumberAccountSalary)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("numberAccountSalary");
            entity.Property(e => e.PhotoPath)
                .IsUnicode(false)
                .HasColumnName("photoPath");
            entity.Property(e => e.Province)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("province");
            entity.Property(e => e.SalaryBank)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("salaryBank");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telephone");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
            entity.Property(e => e.WorkModality)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("workModality");

            entity.HasOne(d => d.IdDocumentTypeNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdDocumentType)
                .HasConstraintName("FK__Employee__idDocu__07220AB2");

            entity.HasOne(d => d.IdJobNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdJob)
                .HasConstraintName("FK__Employee__idJob__090A5324");

            entity.HasOne(d => d.IdJobDepartmentNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdJobDepartment)
                .HasConstraintName("FK__Employee__idJobD__08162EEB");
        });

        modelBuilder.Entity<FamilyBondType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FamilyBo__3213E83F51334919");

            entity.ToTable("FamilyBondType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<HealthInsurance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HealthIn__3213E83F6FEAA16F");

            entity.ToTable("HealthInsurance");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("documentNumber");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.IdEmployee).HasColumnName("idEmployee");
            entity.Property(e => e.IdFamilyBondType).HasColumnName("idFamilyBondType");
            entity.Property(e => e.NameHolder)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nameHolder");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.HealthInsurances)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK__HealthIns__idEmp__0DCF0841");

            entity.HasOne(d => d.IdFamilyBondTypeNavigation).WithMany(p => p.HealthInsurances)
                .HasForeignKey(d => d.IdFamilyBondType)
                .HasConstraintName("FK__HealthIns__idFam__0EC32C7A");
        });

        modelBuilder.Entity<HistoryInfoChange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HistoryI__3213E83F663C27F2");

            entity.ToTable("HistoryInfoChange");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("action");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.IdCompany).HasColumnName("idCompany");
            entity.Property(e => e.IdPerson).HasColumnName("idPerson");
            entity.Property(e => e.IdTicket).HasColumnName("idTicket");
            entity.Property(e => e.IdentifierTraduction)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identifierTraduction");
            entity.Property(e => e.LastUser).HasColumnName("lastUser");
            entity.Property(e => e.Module)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("module");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Job__3213E83F6467DF61");

            entity.ToTable("Job");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.IdJobDepartment).HasColumnName("idJobDepartment");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdJobDepartmentNavigation).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.IdJobDepartment)
                .HasConstraintName("FK__Job__idJobDepart__025D5595");
        });

        modelBuilder.Entity<JobDepartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobDepar__3213E83F24FE49F7");

            entity.ToTable("JobDepartment");

            entity.HasIndex(e => e.Name, "UQ__JobDepar__72E12F1BA279E52F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<LandOwnership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LandOwne__3213E83F9230256A");

            entity.ToTable("LandOwnership");

            entity.HasIndex(e => e.Name, "UQ__LandOwne__72E12F1B89EB8C1A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("englishName");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Language__3213E83F725F06AB");

            entity.ToTable("Language");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.FlagIso)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("flagIso");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<LegalPersonType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LegalPer__3213E83FD6827A44");

            entity.ToTable("LegalPersonType");

            entity.HasIndex(e => e.Name, "UQ__LegalPer__72E12F1B70DAC20F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("englishName");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Sigla)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("sigla");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<LegalRegisterSituation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LegalReg__3213E83FA67C6B82");

            entity.ToTable("LegalRegisterSituation");

            entity.HasIndex(e => e.Name, "UQ__LegalReg__72E12F1B64D96D4B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abreviation)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("abreviation");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("englishName");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3213E83FCBF19D24");

            entity.ToTable("Order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.CreditAmount)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("creditAmount");
            entity.Property(e => e.DateReport)
                .HasColumnType("datetime")
                .HasColumnName("dateReport");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("datetime")
                .HasColumnName("expirationDate");
            entity.Property(e => e.IdCompany).HasColumnName("idCompany");
            entity.Property(e => e.IdPerson).HasColumnName("idPerson");
            entity.Property(e => e.IdSubscriber).HasColumnName("idSubscriber");
            entity.Property(e => e.Language)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("language");
            entity.Property(e => e.LastUpdateUser).HasColumnName("lastUpdateUser");
            entity.Property(e => e.RealExpirationDate)
                .HasColumnType("datetime")
                .HasColumnName("realExpirationDate");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("referenceNumber");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.Terms)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("terms");
            entity.Property(e => e.TypeOrder)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("typeOrder");
            entity.Property(e => e.TypeReport)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("typeReport");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdCompanyNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdCompany)
                .HasConstraintName("FK__Order__idCompany__3F3159AB");

            entity.HasOne(d => d.IdPersonNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdPerson)
                .HasConstraintName("FK__Order__idPerson__40257DE4");

            entity.HasOne(d => d.IdSubscriberNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdSubscriber)
                .HasConstraintName("FK__Order__idSubscri__3E3D3572");
        });

        modelBuilder.Entity<PaymentPolicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentP__3213E83F0926C101");

            entity.ToTable("PaymentPolicy");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("englishName");
            entity.Property(e => e.Flag)
                .HasDefaultValueSql("((0))")
                .HasColumnName("flag");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Person_Id");

            entity.ToTable("Person");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate)
                .HasColumnType("datetime")
                .HasColumnName("birthDate");
            entity.Property(e => e.BirthPlace)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("birthPlace");
            entity.Property(e => e.Cellphone)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("cellphone");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.ClubMember)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("clubMember");
            entity.Property(e => e.CodeDocumentType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("codeDocumentType");
            entity.Property(e => e.CodePhone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codePhone");
            entity.Property(e => e.Cp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cp");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.FatherName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("fatherName");
            entity.Property(e => e.Fullname)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("fullname");
            entity.Property(e => e.IdCivilStatus).HasColumnName("idCivilStatus");
            entity.Property(e => e.IdCountry).HasColumnName("idCountry");
            entity.Property(e => e.IdCreditRisk).HasColumnName("idCreditRisk");
            entity.Property(e => e.IdDocumentType).HasColumnName("idDocumentType");
            entity.Property(e => e.IdLegalRegisterSituation).HasColumnName("idLegalRegisterSituation");
            entity.Property(e => e.IdPaymentPolicy).HasColumnName("idPaymentPolicy");
            entity.Property(e => e.IdProfession).HasColumnName("idProfession");
            entity.Property(e => e.IdReputation).HasColumnName("idReputation");
            entity.Property(e => e.Insurance)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("insurance");
            entity.Property(e => e.Language)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("language");
            entity.Property(e => e.LastSearched)
                .HasColumnType("datetime")
                .HasColumnName("lastSearched");
            entity.Property(e => e.LastUpdateUser).HasColumnName("lastUpdateUser");
            entity.Property(e => e.MotherName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("motherName");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nationality");
            entity.Property(e => e.NewsCommentary)
                .IsUnicode(false)
                .HasColumnName("newsCommentary");
            entity.Property(e => e.NumberPhone)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("numberPhone");
            entity.Property(e => e.OldCode)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("oldCode");
            entity.Property(e => e.OnWeb)
                .HasDefaultValueSql("((1))")
                .HasColumnName("onWeb");
            entity.Property(e => e.OtherDirecctions)
                .IsUnicode(false)
                .HasColumnName("otherDirecctions");
            entity.Property(e => e.PrintNewsCommentary).HasColumnName("printNewsCommentary");
            entity.Property(e => e.PrivateCommentary)
                .IsUnicode(false)
                .HasColumnName("privateCommentary");
            entity.Property(e => e.RelationshipCodeDocument)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("relationshipCodeDocument");
            entity.Property(e => e.RelationshipDocumentType).HasColumnName("relationshipDocumentType");
            entity.Property(e => e.RelationshipWith)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("relationshipWith");
            entity.Property(e => e.ReputationCommentary)
                .IsUnicode(false)
                .HasColumnName("reputationCommentary");
            entity.Property(e => e.TaxTypeCode)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("taxTypeCode");
            entity.Property(e => e.TaxTypeName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("taxTypeName");
            entity.Property(e => e.TradeName)
                .IsUnicode(false)
                .HasColumnName("tradeName");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdCivilStatusNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.IdCivilStatus)
                .HasConstraintName("FK__Person__idCivilS__2D12A970");

            entity.HasOne(d => d.IdCountryNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.IdCountry)
                .HasConstraintName("FK__Person__idCountr__2C1E8537");

            entity.HasOne(d => d.IdCreditRiskNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.IdCreditRisk)
                .HasConstraintName("FK__Person__idCredit__2EFAF1E2");

            entity.HasOne(d => d.IdDocumentTypeNavigation).WithMany(p => p.PersonIdDocumentTypeNavigations)
                .HasForeignKey(d => d.IdDocumentType)
                .HasConstraintName("FK__Person__idDocume__2A363CC5");

            entity.HasOne(d => d.IdLegalRegisterSituationNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.IdLegalRegisterSituation)
                .HasConstraintName("FK__Person__idLegalR__2B2A60FE");

            entity.HasOne(d => d.IdPaymentPolicyNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.IdPaymentPolicy)
                .HasConstraintName("FK__Person__idPaymen__2FEF161B");

            entity.HasOne(d => d.IdReputationNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.IdReputation)
                .HasConstraintName("FK__Person__idReputa__30E33A54");

            entity.HasOne(d => d.RelationshipDocumentTypeNavigation).WithMany(p => p.PersonRelationshipDocumentTypeNavigations)
                .HasForeignKey(d => d.RelationshipDocumentType)
                .HasConstraintName("FK__Person__relation__2E06CDA9");
        });

        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Process__3213E83FBF838E86");

            entity.ToTable("Process");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.Menu)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("menu");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.OrderItem).HasColumnName("orderItem");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<Reputation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reputati__3213E83FDDE4BDE3");

            entity.ToTable("Reputation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EnglishName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("englishName");
            entity.Property(e => e.Flag)
                .HasDefaultValueSql("((0))")
                .HasColumnName("flag");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.OldCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("oldCode");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("type");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subscrib__3213E83FAAFFFEEC");

            entity.ToTable("Subscriber");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acronym)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("acronym");
            entity.Property(e => e.AdditionalContactEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("additionalContactEmail");
            entity.Property(e => e.AdditionalContactName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("additionalContactName");
            entity.Property(e => e.AdditionalContactTelephone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("additionalContactTelephone");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.FacturationType)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("facturationType");
            entity.Property(e => e.Fax)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fax");
            entity.Property(e => e.IdContinent).HasColumnName("idContinent");
            entity.Property(e => e.IdCountry).HasColumnName("idCountry");
            entity.Property(e => e.IdCurrency).HasColumnName("idCurrency");
            entity.Property(e => e.IdRubro).HasColumnName("idRubro");
            entity.Property(e => e.Indications)
                .IsUnicode(false)
                .HasColumnName("indications");
            entity.Property(e => e.Language)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("language");
            entity.Property(e => e.LastUpdateUser).HasColumnName("lastUpdateUser");
            entity.Property(e => e.MaximumCredit).HasColumnName("maximumCredit");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NormalPrice).HasColumnName("normalPrice");
            entity.Property(e => e.Observations)
                .IsUnicode(false)
                .HasColumnName("observations");
            entity.Property(e => e.PrincipalContact)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("principalContact");
            entity.Property(e => e.RevealName).HasColumnName("revealName");
            entity.Property(e => e.SendInvoiceToEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sendInvoiceToEmail");
            entity.Property(e => e.SendInvoiceToName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sendInvoiceToName");
            entity.Property(e => e.SendInvoiceToTelephone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sendInvoiceToTelephone");
            entity.Property(e => e.SendReportToEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sendReportToEmail");
            entity.Property(e => e.SendReportToName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sendReportToName");
            entity.Property(e => e.SendReportToTelephone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sendReportToTelephone");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.SubscriberType)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("subscriberType");
            entity.Property(e => e.TaxRegistration)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("taxRegistration");
            entity.Property(e => e.Telephone)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("telephone");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
            entity.Property(e => e.WebPage)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("webPage");

            entity.HasOne(d => d.IdContinentNavigation).WithMany(p => p.Subscribers)
                .HasForeignKey(d => d.IdContinent)
                .HasConstraintName("FK__Subscribe__idCon__369C13AA");

            entity.HasOne(d => d.IdCountryNavigation).WithMany(p => p.Subscribers)
                .HasForeignKey(d => d.IdCountry)
                .HasConstraintName("FK__Subscribe__idCou__379037E3");

            entity.HasOne(d => d.IdCurrencyNavigation).WithMany(p => p.Subscribers)
                .HasForeignKey(d => d.IdCurrency)
                .HasConstraintName("FK__Subscribe__idCur__38845C1C");
        });

        modelBuilder.Entity<SubscriberPrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subscrib__3213E83F16E2D8E8");

            entity.ToTable("SubscriberPrice");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.DayT1).HasColumnName("dayT1");
            entity.Property(e => e.DayT2).HasColumnName("dayT2");
            entity.Property(e => e.DayT3).HasColumnName("dayT3");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.IdContinent).HasColumnName("idContinent");
            entity.Property(e => e.IdCountry).HasColumnName("idCountry");
            entity.Property(e => e.IdCurrency).HasColumnName("idCurrency");
            entity.Property(e => e.IdSubscriber).HasColumnName("idSubscriber");
            entity.Property(e => e.LastUpdateUser).HasColumnName("lastUpdateUser");
            entity.Property(e => e.PriceB).HasColumnName("priceB");
            entity.Property(e => e.PriceT1).HasColumnName("priceT1");
            entity.Property(e => e.PriceT2).HasColumnName("priceT2");
            entity.Property(e => e.PriceT3).HasColumnName("priceT3");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdContinentNavigation).WithMany(p => p.SubscriberPrices)
                .HasForeignKey(d => d.IdContinent)
                .HasConstraintName("FK__Subscribe__idCon__52442E1F");

            entity.HasOne(d => d.IdCountryNavigation).WithMany(p => p.SubscriberPrices)
                .HasForeignKey(d => d.IdCountry)
                .HasConstraintName("FK__Subscribe__idCou__53385258");

            entity.HasOne(d => d.IdCurrencyNavigation).WithMany(p => p.SubscriberPrices)
                .HasForeignKey(d => d.IdCurrency)
                .HasConstraintName("FK__Subscribe__idCur__542C7691");

            entity.HasOne(d => d.IdSubscriberNavigation).WithMany(p => p.SubscriberPrices)
                .HasForeignKey(d => d.IdSubscriber)
                .HasConstraintName("FK__Subscribe__idSub__515009E6");
        });

        modelBuilder.Entity<Traduction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Traducti__3213E83FA3EECE86");

            entity.ToTable("Traduction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.Flag1)
                .HasDefaultValueSql("((0))")
                .HasColumnName("flag1");
            entity.Property(e => e.Flag2)
                .HasDefaultValueSql("((0))")
                .HasColumnName("flag2");
            entity.Property(e => e.Flag3)
                .HasDefaultValueSql("((0))")
                .HasColumnName("flag3");
            entity.Property(e => e.IdCompany).HasColumnName("idCompany");
            entity.Property(e => e.IdLanguage).HasColumnName("idLanguage");
            entity.Property(e => e.IdPerson).HasColumnName("idPerson");
            entity.Property(e => e.Identifier)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identifier");
            entity.Property(e => e.IntValue).HasColumnName("intValue");
            entity.Property(e => e.LargeValue)
                .IsUnicode(false)
                .HasColumnName("largeValue");
            entity.Property(e => e.LastUpdaterUser).HasColumnName("lastUpdaterUser");
            entity.Property(e => e.NumberValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("numberValue");
            entity.Property(e => e.ShortValue)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("shortValue");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdCompanyNavigation).WithMany(p => p.Traductions)
                .HasForeignKey(d => d.IdCompany)
                .HasConstraintName("FK_CompanyTraductionNull");

            entity.HasOne(d => d.IdLanguageNavigation).WithMany(p => p.Traductions)
                .HasForeignKey(d => d.IdLanguage)
                .HasConstraintName("FK__Traductio__idLan__68687968");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserLogi__3213E83FECA40771");

            entity.ToTable("UserLogin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.IdEmployee).HasColumnName("idEmployee");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
            entity.Property(e => e.UserLogin1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("userLogin");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK__UserLogin__idEmp__1387E197");
        });

        modelBuilder.Entity<UserProcess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserProc__3213E83F1A12E4C8");

            entity.ToTable("UserProcess");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("deleteDate");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.IdProcess).HasColumnName("idProcess");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdProcessNavigation).WithMany(p => p.UserProcesses)
                .HasForeignKey(d => d.IdProcess)
                .HasConstraintName("FK__UserProce__idPro__184C96B4");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserProcesses)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__UserProce__idUse__1940BAED");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
