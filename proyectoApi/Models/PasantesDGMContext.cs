﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace proyectoApi.Models
{
    public partial class PasantesDGMContext : DbContext
    {
        public PasantesDGMContext()
        {
        }

        public PasantesDGMContext(DbContextOptions<PasantesDGMContext> options)
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=172.16.10.44;Database=PasantesDGM;User Id=usr_pasantes;Password=RhgA5WO6Lhbvbfos6Uvw;");
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
