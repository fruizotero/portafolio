using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portafolio.backend.API.Contexto.Migraciones
{
    /// <inheritdoc />
    public partial class tablasProyectoConocimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conocimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioAdministradorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conocimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conocimientos_UsuariosAdministradores_UsuarioAdministradorId",
                        column: x => x.UsuarioAdministradorId,
                        principalTable: "UsuariosAdministradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenDesktopUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagenMobileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepositorioUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiveUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    UsuarioAdministradorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proyectos_UsuariosAdministradores_UsuarioAdministradorId",
                        column: x => x.UsuarioAdministradorId,
                        principalTable: "UsuariosAdministradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConocimientoProyecto",
                columns: table => new
                {
                    ConocimientosId = table.Column<int>(type: "int", nullable: false),
                    ProyectosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConocimientoProyecto", x => new { x.ConocimientosId, x.ProyectosId });
                    table.ForeignKey(
                        name: "FK_ConocimientoProyecto_Conocimientos_ConocimientosId",
                        column: x => x.ConocimientosId,
                        principalTable: "Conocimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConocimientoProyecto_Proyectos_ProyectosId",
                        column: x => x.ProyectosId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HabilidadProyecto",
                columns: table => new
                {
                    HabilidadesId = table.Column<int>(type: "int", nullable: false),
                    ProyectosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabilidadProyecto", x => new { x.HabilidadesId, x.ProyectosId });
                    table.ForeignKey(
                        name: "FK_HabilidadProyecto_Habilidades_HabilidadesId",
                        column: x => x.HabilidadesId,
                        principalTable: "Habilidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabilidadProyecto_Proyectos_ProyectosId",
                        column: x => x.ProyectosId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConocimientoProyecto_ProyectosId",
                table: "ConocimientoProyecto",
                column: "ProyectosId");

            migrationBuilder.CreateIndex(
                name: "IX_Conocimientos_UsuarioAdministradorId",
                table: "Conocimientos",
                column: "UsuarioAdministradorId");

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadProyecto_ProyectosId",
                table: "HabilidadProyecto",
                column: "ProyectosId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_UsuarioAdministradorId",
                table: "Proyectos",
                column: "UsuarioAdministradorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConocimientoProyecto");

            migrationBuilder.DropTable(
                name: "HabilidadProyecto");

            migrationBuilder.DropTable(
                name: "Conocimientos");

            migrationBuilder.DropTable(
                name: "Proyectos");
        }
    }
}
