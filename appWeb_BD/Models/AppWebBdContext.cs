using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace appWeb_BD.Models;

public partial class AppWebBdContext : DbContext
{
    public AppWebBdContext()
    {
    }

    public AppWebBdContext(DbContextOptions<AppWebBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TestHabilidadesBlanda> TestHabilidadesBlandas { get; set; }

    public virtual DbSet<TestTipoIdentificacion> TestTipoIdentificacions { get; set; }

    public virtual DbSet<TestTipoUsuario> TestTipoUsuarios { get; set; }

    public virtual DbSet<TestUsuario> TestUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=workBeanch;Database=appWeb_BD;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestHabilidadesBlanda>(entity =>
        {
            entity.HasKey(e => e.HabilidadBlandaId).HasName("PK__test_Hab__57E1ABDA569090C6");

            entity.ToTable("test_HabilidadesBlandas");

            entity.Property(e => e.HabilidadBlandaId).HasColumnName("HabilidadBlandaID");
            entity.Property(e => e.NombreHabilidadBlanda)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TestTipoIdentificacion>(entity =>
        {
            entity.HasKey(e => e.TipoIdentificacionId).HasName("PK__test_Tip__C774CA5400A91521");

            entity.ToTable("test_TipoIdentificacion");

            entity.Property(e => e.TipoIdentificacionId).HasColumnName("TipoIdentificacionID");
            entity.Property(e => e.NombreTipoIdentificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TestTipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TipoUsuarioId).HasName("PK__test_Tip__7F22C70213F1394A");

            entity.ToTable("test_TipoUsuario");

            entity.Property(e => e.TipoUsuarioId).HasColumnName("TipoUsuarioID");
            entity.Property(e => e.NombreTipoUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TestUsuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__test_Usu__2B3DE79855C5B5B8");

            entity.ToTable("test_Usuarios");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HabilidadBlandaId).HasColumnName("HabilidadBlandaID");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoIdentificacionId).HasColumnName("TipoIdentificacionID");
            entity.Property(e => e.TipoUsuarioId).HasColumnName("TipoUsuarioID");

            entity.HasOne(d => d.HabilidadBlanda).WithMany(p => p.TestUsuarios)
                .HasForeignKey(d => d.HabilidadBlandaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__test_Usua__Habil__2C3393D0");

            entity.HasOne(d => d.TipoIdentificacion).WithMany(p => p.TestUsuarios)
                .HasForeignKey(d => d.TipoIdentificacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__test_Usua__TipoI__2B3F6F97");

            entity.HasOne(d => d.TipoUsuario).WithMany(p => p.TestUsuarios)
                .HasForeignKey(d => d.TipoUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__test_Usua__TipoU__2A4B4B5E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
