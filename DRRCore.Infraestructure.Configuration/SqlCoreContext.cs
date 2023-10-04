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

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobDepartment> JobDepartments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Process> Processes { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=sql5103.site4now.net;Initial Catalog=db_a9ccf0_eecore;User ID=db_a9ccf0_eecore_admin;Password=drrti2023;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3213E83F0354AE0C");

            entity.ToTable("Country");

            entity.HasIndex(e => e.Name, "UQ__Country__72E12F1B14C5EF2B").IsUnique();

            entity.HasIndex(e => e.EnglishName, "UQ__Country__A124FA46587436C1").IsUnique();

            entity.HasIndex(e => e.Iso, "UQ__Country__DC509075CA3B2293").IsUnique();

            entity.HasIndex(e => e.FlagIso, "UQ__Country__ED15D6F32E0E806C").IsUnique();

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
            entity.Property(e => e.Flag)
                .HasDefaultValueSql("((0))")
                .HasColumnName("flag");
            entity.Property(e => e.FlagIso)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flagIso");
            entity.Property(e => e.Iso)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("iso");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
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
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83F317A8138");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Code, "UQ__Employee__357D4CF9FF0B743F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Birthday)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("birthday");
            entity.Property(e => e.Cellphone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cellphone");
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
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.IdCountry).HasColumnName("idCountry");
            entity.Property(e => e.IdDocumentType).HasColumnName("idDocumentType");
            entity.Property(e => e.IdJob).HasColumnName("idJob");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.Photo)
                .IsUnicode(false)
                .HasColumnName("photo");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdCountryNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdCountry)
                .HasConstraintName("FK__Employee__idCoun__70DDC3D8");

            entity.HasOne(d => d.IdDocumentTypeNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdDocumentType)
                .HasConstraintName("FK__Employee__idDocu__6EF57B66");

            entity.HasOne(d => d.IdJobNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdJob)
                .HasConstraintName("FK__Employee__idJob__6FE99F9F");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Job__3213E83FD43C5CBC");

            entity.ToTable("Job");

            entity.HasIndex(e => e.Name, "UQ__Job__72E12F1B13485DA5").IsUnique();

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
                .HasConstraintName("FK__Job__idJobDepart__46E78A0C");
        });

        modelBuilder.Entity<JobDepartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobDepar__3213E83F8284DB3C");

            entity.ToTable("JobDepartment");

            entity.HasIndex(e => e.Name, "UQ__JobDepar__72E12F1BA2CA8812").IsUnique();

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

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3213E83F2649FEE1");

            entity.ToTable("Permission");

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
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdProcessNavigation).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.IdProcess)
                .HasConstraintName("FK__Permissio__idPro__4CA06362");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Permissio__idRol__4BAC3F29");
        });

        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Process__3213E83F04891FD1");

            entity.ToTable("Process");

            entity.HasIndex(e => e.Name, "UQ__Process__72E12F1BD270B4D3").IsUnique();

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

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3213E83F94448855");

            entity.ToTable("Rol");

            entity.HasIndex(e => e.Name, "UQ__Rol__72E12F1B5131A1CD").IsUnique();

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

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserLogi__3213E83FA7238118");

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
                .HasConstraintName("FK__UserLogin__idEmp__75A278F5");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserPerm__3213E83FB81072F3");

            entity.ToTable("UserPermission");

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
            entity.Property(e => e.IdPermission).HasColumnName("idPermission");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.IdPermissionNavigation).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.IdPermission)
                .HasConstraintName("FK__UserPermi__idPer__7A672E12");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserPermissions)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__UserPermi__idUse__7B5B524B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
