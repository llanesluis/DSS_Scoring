using DSS_Scoring.Models;
using Microsoft.EntityFrameworkCore;

namespace DSS_Scoring.Data;

public partial class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alternativa> Alternativas { get; set; }
    public virtual DbSet<Criterio> Criterios { get; set; }
    public virtual DbSet<Matriz> Matrices { get; set; }
    public virtual DbSet<Proyecto> Proyectos { get; set; }
    public virtual DbSet<Resultado> Resultados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alternativa>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.IdProyecto }).HasName("Alternativa_pkey");

            entity.ToTable("Alternativa");

            entity.HasIndex(e => e.Id, "Alternativa_Id_key").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Alternativas)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Alternativa_IdProyecto_fkey");
        });

        modelBuilder.Entity<Criterio>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.IdProyecto }).HasName("Criterio_pkey");

            entity.ToTable("Criterio");

            entity.HasIndex(e => e.Id, "Criterio_Id_key").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Criterios)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Criterio_IdProyecto_fkey");
        });

        modelBuilder.Entity<Matriz>(entity =>
        {
            entity.HasKey(e => new { e.IdProyecto, e.IdAlternativa, e.IdCriterio }).HasName("Matriz_pkey");

            entity.ToTable("Matriz");

            entity.HasOne(d => d.IdAlternativaNavigation).WithMany(p => p.Matrices)
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(d => d.IdAlternativa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Matriz_IdAlternativa_fkey");

            entity.HasOne(d => d.IdCriterioNavigation).WithMany(p => p.Matrices)
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(d => d.IdCriterio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Matriz_IdCriterio_fkey");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Matrices)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Matriz_IdProyecto_fkey");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Proyectos");

            entity.ToTable("Proyecto");
        });

        modelBuilder.Entity<Resultado>(entity =>
        {
            entity.HasKey(e => new { e.IdProyecto, e.IdAlternativa }).HasName("Resultado_pkey");

            entity.ToTable("Resultado");

            entity.HasOne(d => d.IdAlternativaNavigation).WithMany(p => p.Resultados)
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(d => d.IdAlternativa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Resultado_IdAlternativa_fkey");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Resultados)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Resultado_IdProyecto_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
