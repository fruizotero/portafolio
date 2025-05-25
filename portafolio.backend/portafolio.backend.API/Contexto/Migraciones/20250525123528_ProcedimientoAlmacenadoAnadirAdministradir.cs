using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portafolio.backend.API.Contexto.Migraciones
{
    /// <inheritdoc />
    public partial class ProcedimientoAlmacenadoAnadirAdministradir : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Procedimiento almacenado para añadir un usuario administrador
            migrationBuilder.Sql($@"
                CREATE PROCEDURE AnadirUsuarioAdministrador
                    
                AS
                BEGIN
                    SET NOCOUNT ON;
                    INSERT INTO UsuariosAdministradores (Email, Password, FechaCreacion, FechaActualizacion)
                    VALUES ('admin@portafolio.com', '$2y$10$xvRnl.Jz0GDQF.JIgH9bMu1g.Z55V2KTGJZI7kSqR2nqiMlv7n28S', GETDATE(), GETDATE());
                END
            ");

            // Invocar el procedimiento almacenado para añadir un usuario administrador por defecto
            migrationBuilder.Sql("EXEC AnadirUsuarioAdministrador");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar el procedimiento almacenado si es necesario
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AnadirUsuarioAdministrador");

        }
    }
}
