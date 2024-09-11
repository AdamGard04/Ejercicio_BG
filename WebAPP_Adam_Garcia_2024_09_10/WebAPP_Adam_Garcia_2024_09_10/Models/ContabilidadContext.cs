using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPP_Adam_Garcia_2024_09_10.Models
{
    public partial class ContabilidadContext : DbContext
    {

        public ContabilidadContext(DbContextOptions<ContabilidadContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AsientoContable> AsientoContables { get; set; } = null!;
        public virtual DbSet<CuentaContable> CuentaContables { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Movimiento> Movimientos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AsientoContable>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Fecha });

                entity.ToTable("AsientoContable");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('descuadrado')");

                entity.HasOne(d => d.Departamento)
                    .WithMany(p => p.AsientoContables)
                    .HasForeignKey(d => d.DepartamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AsientoContable_Departamento");
            });

            modelBuilder.Entity<CuentaContable>(entity =>
            {
                entity.HasKey(e => e.CuentaId)
                    .HasName("PK__CuentaCo__A64A728E0C552E12");

                entity.ToTable("CuentaContable");

                entity.Property(e => e.CuentaId).HasColumnName("CUENTA_ID");

                entity.Property(e => e.NumCuenta)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("NUM_CUENTA");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.DptoId)
                    .HasName("PK__Departam__EF641CB18A755FB3");

                entity.ToTable("Departamento");

                entity.Property(e => e.DptoId).HasColumnName("DPTO_ID");

                entity.Property(e => e.DescDpto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESC_DPTO");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => new { e.AsientoId, e.AsientoFecha, e.CuentaId });

                entity.ToTable("Movimiento");

                entity.Property(e => e.AsientoFecha).HasColumnType("date");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TipoMovimiento)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Cuenta)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.CuentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movimiento_CuentaContable");

                entity.HasOne(d => d.Asiento)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => new { d.AsientoId, d.AsientoFecha })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movimiento_AsientoContable");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
