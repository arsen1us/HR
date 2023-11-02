using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HR;

public partial class HrV3Context : DbContext
{
    public HrV3Context()
    {
    }

    public HrV3Context(DbContextOptions<HrV3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Profession> Professions { get; set; }

    public virtual DbSet<WorkExperience> WorkExperiences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-9RTLIH5;Initial Catalog=hr_v3; Encrypt=false;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__clients__3213E83FEEC1E448");

            entity.ToTable("clients");

            entity.HasIndex(e => e.Id, "UQ__clients__3213E83E2EBC316E").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Surname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__companie__3213E83FFF4E7158");

            entity.ToTable("companies");

            entity.HasIndex(e => e.Id, "UQ__companie__3213E83EBAC99FA4").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.FkClientId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("fk_client_id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Owner)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("owner");

            entity.HasOne(d => d.FkClient).WithMany(p => p.Companies)
                .HasForeignKey(d => d.FkClientId)
                .HasConstraintName("FK__companies__fk_cl__4E88ABD4");
        });

        modelBuilder.Entity<Profession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__professi__3213E83FD8A6AC09");

            entity.ToTable("professions");

            entity.HasIndex(e => e.Id, "UQ__professi__3213E83E4566A7D7").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.FkCompanyId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("fk_company_id");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Salary).HasColumnName("salary");

            entity.HasOne(d => d.FkCompany).WithMany(p => p.Professions)
                .HasForeignKey(d => d.FkCompanyId)
                .HasConstraintName("FK__professio__fk_co__52593CB8");
        });

        modelBuilder.Entity<WorkExperience>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__work_exp__3213E83F88BDB1E9");

            entity.ToTable("work_experience");

            entity.HasIndex(e => e.Id, "UQ__work_exp__3213E83EE23ED64D").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.BeginningOfWork)
                .HasColumnType("date")
                .HasColumnName("beginning_of_work");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.EndingOfWork)
                .HasColumnType("date")
                .HasColumnName("ending_of_work");
            entity.Property(e => e.FkClientId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("fk_client_id");
            entity.Property(e => e.NowWorking).HasColumnName("now_working");
            entity.Property(e => e.Profession)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("profession");
            entity.Property(e => e.Salary).HasColumnName("salary");

            entity.HasOne(d => d.FkClient).WithMany(p => p.WorkExperiences)
                .HasForeignKey(d => d.FkClientId)
                .HasConstraintName("FK__work_expe__fk_cl__571DF1D5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
