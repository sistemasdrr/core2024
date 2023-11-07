using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Domain.Entities.SqlCoreContext;

public partial class SqlCoreContext : DbContext
{
    public SqlCoreContext()
    {
    }

    public SqlCoreContext(DbContextOptions<SqlCoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Continent> Continents { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobDepartment> JobDepartments { get; set; }

    public virtual DbSet<Process> Processes { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserProcess> UserProcesses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=sql5103.site4now.net;Initial Catalog=db_a9ccf0_eecore;User ID=db_a9ccf0_eecore_admin;Password=drrti2023;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Continent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Continen__3213E83FF93989B2");

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
            entity.HasKey(e => e.Id).HasName("PK__Country__3213E83F32DA54E3");

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
                .HasConstraintName("FK__Country__idConti__3552E9B6");
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
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83F128680C1");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Code, "UQ__Employee__357D4CF9046937AA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Birthday)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("birthday");
            entity.Property(e => e.Children)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("children");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.CivilStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("civilStatus");
            entity.Property(e => e.Code)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("code");
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
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Enable)
                .HasDefaultValueSql("((1))")
                .HasColumnName("enable");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.Father)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("father");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.Grains)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("grains");
            entity.Property(e => e.IdCountry).HasColumnName("idCountry");
            entity.Property(e => e.IdDocumentType).HasColumnName("idDocumentType");
            entity.Property(e => e.IdJob).HasColumnName("idJob");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.LastWork)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lastWork");
            entity.Property(e => e.Mother)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mother");
            entity.Property(e => e.Observation)
                .IsUnicode(false)
                .HasColumnName("observation");
            entity.Property(e => e.Photo)
                .IsUnicode(false)
                .HasColumnName("photo");
            entity.Property(e => e.ShortName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("shortName");
            entity.Property(e => e.Spouse)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("spouse");
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

            entity.HasOne(d => d.IdCountryNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdCountry)
                .HasConstraintName("FK__Employee__idCoun__4589517F");

            entity.HasOne(d => d.IdDocumentTypeNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdDocumentType)
                .HasConstraintName("FK__Employee__idDocu__43A1090D");

            entity.HasOne(d => d.IdJobNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdJob)
                .HasConstraintName("FK__Employee__idJob__44952D46");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Job__3213E83FCEDFB1AD");

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
                .HasConstraintName("FK__Job__idJobDepart__3DE82FB7");
        });

        modelBuilder.Entity<JobDepartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobDepar__3213E83FADBB879A");

            entity.ToTable("JobDepartment");

            entity.HasIndex(e => e.Name, "UQ__JobDepar__72E12F1B7A64BBE9").IsUnique();

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

        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Process__3213E83F6336FD4B");

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

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserLogi__3213E83FEFFED43B");

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
                .HasConstraintName("FK__UserLogin__idEmp__4A4E069C");
        });

        modelBuilder.Entity<UserProcess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserProc__3213E83FC8C29933");

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
                .HasConstraintName("FK__UserProce__idPro__4F12BBB9");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserProcesses)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__UserProce__idUse__5006DFF2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
