using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portafolio.backend.API.Contexto.Migraciones
{
    /// <inheritdoc />
    public partial class procedimientoAlmacenadoAnadirConocimientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Procedimiento almacenado para añadir conocimientos iniciales
            migrationBuilder.Sql($@"
                CREATE PROCEDURE AnadirConocimientosIniciales 
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    -- Conocimientos iniciales
                    INSERT INTO Conocimientos (UsuarioAdministradorId, Nombre, CreatedAt, UpdatedAt)
                    VALUES 
                        (1, 'Desarrollo Web Frontend', GETDATE(), GETDATE()),
                        (1, 'Desarrollo Web Backend', GETDATE(), GETDATE()),
                        (1, 'Diseño Responsive', GETDATE(), GETDATE()),
                        (1, 'Bases de Datos SQL', GETDATE(), GETDATE()),
                        (1, 'APIs RESTful', GETDATE(), GETDATE()),
                        (1, 'Autenticación y Autorización', GETDATE(), GETDATE()),
                        (1, 'Control de Versiones Git', GETDATE(), GETDATE()),
                        (1, 'Arquitectura MVC', GETDATE(), GETDATE()),
                        (1, 'Entity Framework Core', GETDATE(), GETDATE()),
                        (1, 'Desarrollo de Aplicaciones SPA', GETDATE(), GETDATE());
                        
                    -- Asignar conocimientos a proyectos existentes
                    -- Portfolio Personal (asumiendo ID = 1)
                    INSERT INTO ConocimientoProyecto (ConocimientosId, ProyectosId)
                    VALUES 
                        (1, 1), -- Desarrollo Web Frontend
                        (2, 1), -- Desarrollo Web Backend
                        (3, 1), -- Diseño Responsive
                        (4, 1), -- Bases de Datos SQL
                        (5, 1), -- APIs RESTful
                        (6, 1), -- Autenticación y Autorización
                        (7, 1), -- Control de Versiones Git
                        (8, 1), -- Arquitectura MVC
                        (9, 1), -- Entity Framework Core
                        (10, 1); -- Desarrollo de Aplicaciones SPA

                    -- E-commerce Demo (asumiendo ID = 2)
                    INSERT INTO ConocimientoProyecto (ConocimientosId, ProyectosId)
                    VALUES 
                        (1, 2), -- Desarrollo Web Frontend
                        (2, 2), -- Desarrollo Web Backend
                        (3, 2), -- Diseño Responsive
                        (4, 2), -- Bases de Datos SQL
                        (5, 2), -- APIs RESTful
                        (6, 2), -- Autenticación y Autorización
                        (10, 2); -- Desarrollo de Aplicaciones SPA

                    -- Gestor de Tareas (asumiendo ID = 3)
                    INSERT INTO ConocimientoProyecto (ConocimientosId, ProyectosId)
                    VALUES 
                        (1, 3), -- Desarrollo Web Frontend
                        (2, 3), -- Desarrollo Web Backend
                        (3, 3), -- Diseño Responsive
                        (4, 3), -- Bases de Datos SQL
                        (7, 3); -- Control de Versiones Git

                    -- API RESTful Demo (asumiendo ID = 4)
                    INSERT INTO ConocimientoProyecto (ConocimientosId, ProyectosId)
                    VALUES 
                        (2, 4), -- Desarrollo Web Backend
                        (4, 4), -- Bases de Datos SQL
                        (5, 4), -- APIs RESTful
                        (6, 4), -- Autenticación y Autorización
                        (7, 4), -- Control de Versiones Git
                        (8, 4), -- Arquitectura MVC
                        (9, 4); -- Entity Framework Core
                END
            ");

            // Invocar el procedimiento almacenado
            migrationBuilder.Sql("EXEC AnadirConocimientosIniciales");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar el procedimiento almacenado
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AnadirConocimientosIniciales");

            // Opcional: Eliminar los datos añadidos por el procedimiento
            migrationBuilder.Sql("DELETE FROM ConocimientoProyecto WHERE ConocimientosId BETWEEN 1 AND 10");
            migrationBuilder.Sql("DELETE FROM Conocimientos WHERE Id BETWEEN 1 AND 10");
        }
    }
}
