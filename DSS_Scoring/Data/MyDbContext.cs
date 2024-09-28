using Microsoft.EntityFrameworkCore;
using DSS_Scoring.Models;

namespace DSS_Scoring.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Criterio> Criterios { get; set; }
        public DbSet<Alternativa> Alternativas { get; set; }
        public DbSet<Puntuacion> Puntuaciones { get; set; }
        public DbSet<Resultado> Resultados { get; set; }
  
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Clave primaria compuesta para la entidad Alternativa, con generación automática de Id
            modelBuilder.Entity<Alternativa>()
                .HasKey(a => new { a.Id, a.IdProyecto });

            modelBuilder.Entity<Alternativa>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd(); // Generar automáticamente el campo Id

            // Clave primaria compuesta para la entidad Criterio, con generación automática de Id
            modelBuilder.Entity<Criterio>()
                .HasKey(c => new { c.Id, c.IdProyecto });

            modelBuilder.Entity<Criterio>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd(); // Generar automáticamente el campo Id

            // Clave primaria compuesta para la entidad Puntuacion, con generación automática de Id
            modelBuilder.Entity<Puntuacion>()
                .HasKey(p => new { p.Id, p.IdProyecto, p.IdAlternativa, p.IdCriterio });

            modelBuilder.Entity<Puntuacion>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd(); // Generar automáticamente el campo Id

            // Clave primaria compuesta para la entidad Resultado, con generación automática de Id
            modelBuilder.Entity<Resultado>()
                .HasKey(r => new { r.Id, r.IdProyecto, r.IdAlternativa });

            modelBuilder.Entity<Resultado>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd(); // Generar automáticamente el campo Id

            // Configurar relaciones muchos a uno (Alternativa -> Proyecto)
            modelBuilder.Entity<Alternativa>()
                .HasOne(a => a.Proyecto)
                .WithMany(p => p.Alternativas)
                .HasForeignKey(a => a.IdProyecto)
                .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación en cascada

            // Configurar relaciones muchos a uno (Criterio -> Proyecto)
            modelBuilder.Entity<Criterio>()
                .HasOne(c => c.Proyecto)
                .WithMany(p => p.Criterios)
                .HasForeignKey(c => c.IdProyecto)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar relaciones muchos a uno (Puntuacion -> Proyecto)
            modelBuilder.Entity<Puntuacion>()
                .HasOne(p => p.Proyecto)
                .WithMany(pr => pr.Puntuaciones)
                .HasForeignKey(p => p.IdProyecto)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar relaciones muchos a uno (Puntuacion -> Alternativa) con clave compuesta
            modelBuilder.Entity<Puntuacion>()
                .HasOne(p => p.Alternativa)
                .WithMany(a => a.Puntuaciones)
                .HasForeignKey(p => new { p.IdAlternativa, p.IdProyecto }) // Referencia a la clave compuesta de Alternativa
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar relaciones muchos a uno (Puntuacion -> Criterio) con clave compuesta
            modelBuilder.Entity<Puntuacion>()
                .HasOne(p => p.Criterio)
                .WithMany(c => c.Puntuaciones)
                .HasForeignKey(p => new { p.IdCriterio, p.IdProyecto }) // Referencia a la clave compuesta de Criterio
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar relaciones muchos a uno (Resultado -> Proyecto)
            modelBuilder.Entity<Resultado>()
                .HasOne(r => r.Proyecto)
                .WithMany(pr => pr.Resultados)
                .HasForeignKey(r => r.IdProyecto)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar relaciones muchos a uno (Resultado -> Alternativa) con clave compuesta
            modelBuilder.Entity<Resultado>()
                .HasOne(r => r.Alternativa)
                .WithMany(a => a.Resultados)
                .HasForeignKey(r => new { r.IdAlternativa, r.IdProyecto }) // Referencia a la clave compuesta de Alternativa
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
