using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DSS_Scoring.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Objetivo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alternativas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alternativas", x => new { x.Id, x.IdProyecto });
                    table.ForeignKey(
                        name: "FK_Alternativas_Proyectos_IdProyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Criterios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Peso = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterios", x => new { x.Id, x.IdProyecto });
                    table.ForeignKey(
                        name: "FK_Criterios_Proyectos_IdProyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resultados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    IdAlternativa = table.Column<int>(type: "integer", nullable: false),
                    PuntuacionTotal = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resultados", x => new { x.Id, x.IdProyecto, x.IdAlternativa });
                    table.ForeignKey(
                        name: "FK_Resultados_Alternativas_IdAlternativa_IdProyecto",
                        columns: x => new { x.IdAlternativa, x.IdProyecto },
                        principalTable: "Alternativas",
                        principalColumns: new[] { "Id", "IdProyecto" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resultados_Proyectos_IdProyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Puntuaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProyecto = table.Column<int>(type: "integer", nullable: false),
                    IdAlternativa = table.Column<int>(type: "integer", nullable: false),
                    IdCriterio = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puntuaciones", x => new { x.Id, x.IdProyecto, x.IdAlternativa, x.IdCriterio });
                    table.ForeignKey(
                        name: "FK_Puntuaciones_Alternativas_IdAlternativa_IdProyecto",
                        columns: x => new { x.IdAlternativa, x.IdProyecto },
                        principalTable: "Alternativas",
                        principalColumns: new[] { "Id", "IdProyecto" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Puntuaciones_Criterios_IdCriterio_IdProyecto",
                        columns: x => new { x.IdCriterio, x.IdProyecto },
                        principalTable: "Criterios",
                        principalColumns: new[] { "Id", "IdProyecto" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Puntuaciones_Proyectos_IdProyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alternativas_IdProyecto",
                table: "Alternativas",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Criterios_IdProyecto",
                table: "Criterios",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Puntuaciones_IdAlternativa_IdProyecto",
                table: "Puntuaciones",
                columns: new[] { "IdAlternativa", "IdProyecto" });

            migrationBuilder.CreateIndex(
                name: "IX_Puntuaciones_IdCriterio_IdProyecto",
                table: "Puntuaciones",
                columns: new[] { "IdCriterio", "IdProyecto" });

            migrationBuilder.CreateIndex(
                name: "IX_Puntuaciones_IdProyecto",
                table: "Puntuaciones",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Resultados_IdAlternativa_IdProyecto",
                table: "Resultados",
                columns: new[] { "IdAlternativa", "IdProyecto" });

            migrationBuilder.CreateIndex(
                name: "IX_Resultados_IdProyecto",
                table: "Resultados",
                column: "IdProyecto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Puntuaciones");

            migrationBuilder.DropTable(
                name: "Resultados");

            migrationBuilder.DropTable(
                name: "Criterios");

            migrationBuilder.DropTable(
                name: "Alternativas");

            migrationBuilder.DropTable(
                name: "Proyectos");
        }
    }
}
