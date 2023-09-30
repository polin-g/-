using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace person_money.Models;

public partial class PersonMoneyContext : DbContext
{
    public PersonMoneyContext()
    {
    }

    public PersonMoneyContext(DbContextOptions<PersonMoneyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InCome> InComes { get; set; }

    public virtual DbSet<InComeCategory> InComeCategories { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Waste> Wastes { get; set; }

    public virtual DbSet<WasteCategory> WasteCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=MMysql_1305;database=person_money", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<InCome>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("in_comes");

            entity.HasIndex(e => e.IdInComeCat, "fk_in_comes_cat_idx");

            entity.HasIndex(e => e.IdClient, "fk_in_comes_cli_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateIn)
                .HasColumnType("datetime")
                .HasColumnName("date_in");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.IdInComeCat).HasColumnName("id_in_come_cat");
            entity.Property(e => e.Sum)
                .HasMaxLength(45)
                .HasColumnName("sum");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.InComes)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_in_comes_cli");

            entity.HasOne(d => d.IdInComeCatNavigation).WithMany(p => p.InComes)
                .HasForeignKey(d => d.IdInComeCat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_in_comes_cat");
        });

        modelBuilder.Entity<InComeCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("in_come_category");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reports");

            entity.HasIndex(e => e.IdClient, "fk_reports_cli_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(400)
                .HasColumnName("content");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.IncomesRep)
                .HasMaxLength(200)
                .HasColumnName("incomes_rep");
            entity.Property(e => e.WastesRep)
                .HasMaxLength(200)
                .HasColumnName("wastes_rep");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_reports_cli");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.IdRole, "fk_users_role_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Login)
                .HasMaxLength(45)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_role");
        });

        modelBuilder.Entity<Waste>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("wastes");

            entity.HasIndex(e => e.IdCategory, "fk_wastes_cat_idx");

            entity.HasIndex(e => e.IdClient, "fk_wastes_cli_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DateWaste)
                .HasColumnType("datetime")
                .HasColumnName("date_waste");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.Sum)
                .HasMaxLength(45)
                .HasColumnName("sum");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Wastes)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_wastes_cat");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Wastes)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_wastes_cli");
        });

        modelBuilder.Entity<WasteCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("waste_categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
