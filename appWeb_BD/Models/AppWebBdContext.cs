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

    public virtual DbSet<TestTelefono> TestTelefonos { get; set; }

    public virtual DbSet<TestUsuario> TestUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //=> optionsBuilder.UseSqlServer("Server=workBeanch;Database=appWeb_BD;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestHabilidadesBlanda>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__test_hab__3213E83F47ACF1FD");

            entity.ToTable("test_habilidades_blandas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Habilidad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("habilidad");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.TestHabilidadesBlanda)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__test_habi__usuar__2B3F6F97");
        });

        modelBuilder.Entity<TestTelefono>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__test_tel__3213E83F0CFE1336");

            entity.ToTable("test_telefonos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.TestTelefonos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__test_tele__usuar__286302EC");
        });

        modelBuilder.Entity<TestUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__test_usu__3213E83F1C53BF42");

            entity.ToTable("test_usuarios");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__test_usu__5B8A068254EA09CD").IsUnique();

            entity.HasIndex(e => e.NombreUsuario, "UQ__test_usu__D4D22D743D1FDA1E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Clave)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre_completo");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.NumIdentificacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("num_identificacion");
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_identificacion");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_usuario");
            entity.Property(e => e.HabilidadBlanda)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("HabilidadBlanda");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
