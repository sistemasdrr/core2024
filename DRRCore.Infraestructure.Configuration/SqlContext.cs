using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Domain.Entities.SQLContext;

public partial class SqlContext : DbContext
{
    public SqlContext()
    {
    }

    public SqlContext(DbContextOptions<SqlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AttachmentsNotSend> AttachmentsNotSends { get; set; }

    public virtual DbSet<EmailConfiguration> EmailConfigurations { get; set; }

    public virtual DbSet<EmailHistory> EmailHistories { get; set; }

    public virtual DbSet<WebQuery> WebQueries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=sql5063.site4now.net;Initial Catalog=db_a9ccf0_admindrr;User ID=db_a9ccf0_admindrr_admin;Password=drrti2023;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttachmentsNotSend>(entity =>
        {
            entity.HasKey(e => e.IdAttachmentsNotSend).HasName("PK__Attachme__7F41A4DD912F7FD4");

            entity.ToTable("AttachmentsNotSend");

            entity.Property(e => e.IdAttachmentsNotSend).HasColumnName("idAttachmentsNotSend");
            entity.Property(e => e.AttachmentsUrl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("attachmentsUrl");
            entity.Property(e => e.IdEmailHistory).HasColumnName("idEmailHistory");

            entity.HasOne(d => d.IdEmailHistoryNavigation).WithMany(p => p.AttachmentsNotSends)
                .HasForeignKey(d => d.IdEmailHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attachmen__idEma__44FF419A");
        });

        modelBuilder.Entity<EmailConfiguration>(entity =>
        {
            entity.HasKey(e => e.IdEmailConfiguration).HasName("PK__EmailCon__23D609DBA31BA0FE");

            entity.ToTable("EmailConfiguration");

            entity.Property(e => e.IdEmailConfiguration).HasColumnName("idEmailConfiguration");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Enable).HasColumnName("enable");
            entity.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasColumnName("insertDate");
            entity.Property(e => e.InsertUser)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("insertUser");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
            entity.Property(e => e.UpdateUser)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("updateUser");
            entity.Property(e => e.Value)
                .IsUnicode(false)
                .HasColumnName("value");
        });

        modelBuilder.Entity<EmailHistory>(entity =>
        {
            entity.HasKey(e => e.IdEmailHistory).HasName("PK__EmailHis__E0A96BD41B250C3E");

            entity.ToTable("EmailHistory");

            entity.Property(e => e.IdEmailHistory).HasColumnName("idEmailHistory");
            entity.Property(e => e.CcMails)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ccMails");
            entity.Property(e => e.CcoMails)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ccoMails");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Domain)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("domain");
            entity.Property(e => e.Enable).HasColumnName("enable");
            entity.Property(e => e.FromMails)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("fromMails");
            entity.Property(e => e.Htmlbody)
                .IsUnicode(false)
                .HasColumnName("htmlbody");
            entity.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasColumnName("insertDate");
            entity.Property(e => e.InsertUser)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("insertUser");
            entity.Property(e => e.Message)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.Subject)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("subject");
            entity.Property(e => e.Success).HasColumnName("success");
            entity.Property(e => e.ToMails)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("toMails");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
            entity.Property(e => e.UpdateUser)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("updateUser");
        });

        modelBuilder.Entity<WebQuery>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CodigoEmpresa }).HasName("PK__WebQuery__499C40374042DC7A");

            entity.ToTable("WebQuery");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.CodigoEmpresa)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigoEmpresa");
            entity.Property(e => e.Actividad)
                .IsUnicode(false)
                .HasColumnName("actividad");
            entity.Property(e => e.ActividadIngles)
                .IsUnicode(false)
                .HasColumnName("actividadIngles");
            entity.Property(e => e.AnoFundacion)
                .IsUnicode(false)
                .HasColumnName("anoFundacion");
            entity.Property(e => e.Calidad)
                .IsUnicode(false)
                .HasColumnName("calidad");
            entity.Property(e => e.CalidadCodigo).HasColumnName("calidadCodigo");
            entity.Property(e => e.Ciudad)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoEmpresaWeb)
                .IsUnicode(false)
                .HasColumnName("codigoEmpresaWeb");
            entity.Property(e => e.CodigoIdioma).HasColumnName("codigoIdioma");
            entity.Property(e => e.Direccion)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Exporta)
                .IsUnicode(false)
                .HasColumnName("exporta");
            entity.Property(e => e.FechaBalance1)
                .IsUnicode(false)
                .HasColumnName("fechaBalance1");
            entity.Property(e => e.FechaBalance2)
                .IsUnicode(false)
                .HasColumnName("fechaBalance2");
            entity.Property(e => e.FechaBalance3)
                .IsUnicode(false)
                .HasColumnName("fechaBalance3");
            entity.Property(e => e.FechaInforme)
                .HasColumnType("datetime")
                .HasColumnName("fechaInforme");
            entity.Property(e => e.Importa)
                .IsUnicode(false)
                .HasColumnName("importa");
            entity.Property(e => e.NombreEmpresa)
                .IsUnicode(false)
                .HasColumnName("nombreEmpresa");
            entity.Property(e => e.NumeroRegistro)
                .IsUnicode(false)
                .HasColumnName("numeroRegistro");
            entity.Property(e => e.Pais)
                .IsUnicode(false)
                .HasColumnName("pais");
            entity.Property(e => e.PaisAbreviatura)
                .IsUnicode(false)
                .HasColumnName("paisAbreviatura");
            entity.Property(e => e.PaisCodigo)
                .IsUnicode(false)
                .HasColumnName("paisCodigo");
            entity.Property(e => e.PaisesExporta)
                .IsUnicode(false)
                .HasColumnName("paisesExporta");
            entity.Property(e => e.PaisesImporta)
                .IsUnicode(false)
                .HasColumnName("paisesImporta");
            entity.Property(e => e.Persona)
                .IsUnicode(false)
                .HasColumnName("persona");
            entity.Property(e => e.Ramo)
                .IsUnicode(false)
                .HasColumnName("ramo");
            entity.Property(e => e.RamoActividad)
                .IsUnicode(false)
                .HasColumnName("ramoActividad");
            entity.Property(e => e.RamoCodigo)
                .IsUnicode(false)
                .HasColumnName("ramoCodigo");
            entity.Property(e => e.Sector)
                .IsUnicode(false)
                .HasColumnName("sector");
            entity.Property(e => e.SectorIngles)
                .IsUnicode(false)
                .HasColumnName("sectorIngles");
            entity.Property(e => e.Telefono)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
