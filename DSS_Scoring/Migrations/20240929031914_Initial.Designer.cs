﻿// <auto-generated />
using System;
using DSS_Scoring.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DSS_Scoring.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240929031914_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DSS_Scoring.Models.Alternativa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IdProyecto")
                        .HasColumnType("integer");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("Id", "IdProyecto")
                        .HasName("Alternativa_pkey");

                    b.HasIndex("IdProyecto");

                    b.HasIndex(new[] { "Id" }, "Alternativa_Id_key")
                        .IsUnique();

                    b.ToTable("Alternativa", (string)null);
                });

            modelBuilder.Entity("DSS_Scoring.Models.Criterio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IdProyecto")
                        .HasColumnType("integer");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<int?>("Peso")
                        .HasColumnType("integer");

                    b.HasKey("Id", "IdProyecto")
                        .HasName("Criterio_pkey");

                    b.HasIndex("IdProyecto");

                    b.HasIndex(new[] { "Id" }, "Criterio_Id_key")
                        .IsUnique();

                    b.ToTable("Criterio", (string)null);
                });

            modelBuilder.Entity("DSS_Scoring.Models.Matriz", b =>
                {
                    b.Property<int>("IdProyecto")
                        .HasColumnType("integer");

                    b.Property<int>("IdAlternativa")
                        .HasColumnType("integer");

                    b.Property<int>("IdCriterio")
                        .HasColumnType("integer");

                    b.Property<int>("Valor")
                        .HasColumnType("integer");

                    b.HasKey("IdProyecto", "IdAlternativa", "IdCriterio")
                        .HasName("Matriz_pkey");

                    b.HasIndex("IdAlternativa");

                    b.HasIndex("IdCriterio");

                    b.ToTable("Matriz", (string)null);
                });

            modelBuilder.Entity("DSS_Scoring.Models.Proyecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Objetivo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("PK_Proyectos");

                    b.ToTable("Proyecto", (string)null);
                });

            modelBuilder.Entity("DSS_Scoring.Models.Resultado", b =>
                {
                    b.Property<int>("IdProyecto")
                        .HasColumnType("integer");

                    b.Property<int>("IdAlternativa")
                        .HasColumnType("integer");

                    b.Property<int?>("Score")
                        .HasColumnType("integer");

                    b.HasKey("IdProyecto", "IdAlternativa")
                        .HasName("Resultado_pkey");

                    b.HasIndex("IdAlternativa");

                    b.ToTable("Resultado", (string)null);
                });

            modelBuilder.Entity("DSS_Scoring.Models.Alternativa", b =>
                {
                    b.HasOne("DSS_Scoring.Models.Proyecto", "IdProyectoNavigation")
                        .WithMany("Alternativas")
                        .HasForeignKey("IdProyecto")
                        .IsRequired()
                        .HasConstraintName("Alternativa_IdProyecto_fkey");

                    b.Navigation("IdProyectoNavigation");
                });

            modelBuilder.Entity("DSS_Scoring.Models.Criterio", b =>
                {
                    b.HasOne("DSS_Scoring.Models.Proyecto", "IdProyectoNavigation")
                        .WithMany("Criterios")
                        .HasForeignKey("IdProyecto")
                        .IsRequired()
                        .HasConstraintName("Criterio_IdProyecto_fkey");

                    b.Navigation("IdProyectoNavigation");
                });

            modelBuilder.Entity("DSS_Scoring.Models.Matriz", b =>
                {
                    b.HasOne("DSS_Scoring.Models.Alternativa", "IdAlternativaNavigation")
                        .WithMany("Matrices")
                        .HasForeignKey("IdAlternativa")
                        .HasPrincipalKey("Id")
                        .IsRequired()
                        .HasConstraintName("Matriz_IdAlternativa_fkey");

                    b.HasOne("DSS_Scoring.Models.Criterio", "IdCriterioNavigation")
                        .WithMany("Matrices")
                        .HasForeignKey("IdCriterio")
                        .HasPrincipalKey("Id")
                        .IsRequired()
                        .HasConstraintName("Matriz_IdCriterio_fkey");

                    b.HasOne("DSS_Scoring.Models.Proyecto", "IdProyectoNavigation")
                        .WithMany("Matrices")
                        .HasForeignKey("IdProyecto")
                        .IsRequired()
                        .HasConstraintName("Matriz_IdProyecto_fkey");

                    b.Navigation("IdAlternativaNavigation");

                    b.Navigation("IdCriterioNavigation");

                    b.Navigation("IdProyectoNavigation");
                });

            modelBuilder.Entity("DSS_Scoring.Models.Resultado", b =>
                {
                    b.HasOne("DSS_Scoring.Models.Alternativa", "IdAlternativaNavigation")
                        .WithMany("Resultados")
                        .HasForeignKey("IdAlternativa")
                        .HasPrincipalKey("Id")
                        .IsRequired()
                        .HasConstraintName("Resultado_IdAlternativa_fkey");

                    b.HasOne("DSS_Scoring.Models.Proyecto", "IdProyectoNavigation")
                        .WithMany("Resultados")
                        .HasForeignKey("IdProyecto")
                        .IsRequired()
                        .HasConstraintName("Resultado_IdProyecto_fkey");

                    b.Navigation("IdAlternativaNavigation");

                    b.Navigation("IdProyectoNavigation");
                });

            modelBuilder.Entity("DSS_Scoring.Models.Alternativa", b =>
                {
                    b.Navigation("Matrices");

                    b.Navigation("Resultados");
                });

            modelBuilder.Entity("DSS_Scoring.Models.Criterio", b =>
                {
                    b.Navigation("Matrices");
                });

            modelBuilder.Entity("DSS_Scoring.Models.Proyecto", b =>
                {
                    b.Navigation("Alternativas");

                    b.Navigation("Criterios");

                    b.Navigation("Matrices");

                    b.Navigation("Resultados");
                });
#pragma warning restore 612, 618
        }
    }
}
