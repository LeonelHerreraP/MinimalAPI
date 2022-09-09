using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace proyectoApi.Models
{
    public partial class PruebaContext : DbContext
    {
        public PruebaContext()
        {
        }

        public PruebaContext(DbContextOptions<PruebaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Solicitud> Solicituds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=proyectoApi.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estado>(entity =>
            {
                entity.Property(e => e.Estado1)
                    .HasMaxLength(20)
                    .HasColumnName("Estado");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.Property(e => e.Apellido).HasMaxLength(250);

                entity.Property(e => e.Direccion).HasMaxLength(250);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Foto).HasMaxLength(250);

                entity.Property(e => e.Nombre).HasMaxLength(250);

                entity.Property(e => e.Pasaporte).HasMaxLength(20);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Solicitud>(entity =>
            {
                entity.ToTable("Solicitud");

                entity.Property(e => e.FechaCreacion).HasColumnType("date");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Solicituds)
                    .HasForeignKey(d => d.EstadoId)
                    .HasConstraintName("FK__Solicitud__Estad__164452B1");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Solicituds)
                    .HasForeignKey(d => d.PersonaId)
                    .HasConstraintName("FK__Solicitud__Perso__15502E78");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
