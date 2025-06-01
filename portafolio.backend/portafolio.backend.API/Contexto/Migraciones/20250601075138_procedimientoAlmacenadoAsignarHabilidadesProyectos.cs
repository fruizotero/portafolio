using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portafolio.backend.API.Contexto.Migraciones
{
    /// <inheritdoc />
    public partial class procedimientoAlmacenadoAsignarHabilidadesProyectos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Procedimiento almacenado para asignar habilidades a proyectos
            migrationBuilder.Sql($@"
                CREATE PROCEDURE AsignarHabilidadesAProyectos 
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    -- Limpiar asignaciones existentes (opcional, depende de si quieres reemplazar todo)
                    -- DELETE FROM HabilidadProyecto;
                    
                    -- Portfolio Personal (ID=1)
                    INSERT INTO HabilidadProyecto (HabilidadesId, ProyectosId)
                    VALUES 
                        (1, 1), -- HTML
                        (2, 1), -- CSS
                        (3, 1), -- JavaScript
                        (4, 1), -- React
                        (5, 1); -- C#

                    -- E-commerce Demo (ID=2)
                    INSERT INTO HabilidadProyecto (HabilidadesId, ProyectosId)
                    VALUES 
                        (1, 2), -- HTML
                        (2, 2), -- CSS
                        (3, 2), -- JavaScript
                        (4, 2); -- React

                    -- Gestor de Tareas (ID=3)
                    INSERT INTO HabilidadProyecto (HabilidadesId, ProyectosId)
                    VALUES 
                        (1, 3), -- HTML
                        (2, 3), -- CSS
                        (3, 3); -- JavaScript

                    -- API RESTful Demo (ID=4)
                    INSERT INTO HabilidadProyecto (HabilidadesId, ProyectosId)
                    VALUES 
                        (5, 4), -- C#
                        (6, 4); -- PHP
                        
                    -- Actualizar fechas de modificación de los proyectos
                    UPDATE Proyectos
                    SET UpdatedAt = GETDATE()
                    WHERE Id IN (1, 2, 3, 4);
                END
            ");

            // Invocar el procedimiento almacenado
            migrationBuilder.Sql("EXEC AsignarHabilidadesAProyectos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar el procedimiento almacenado
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AsignarHabilidadesAProyectos");

            // Eliminar las asignaciones de habilidades a proyectos
            migrationBuilder.Sql("DELETE FROM HabilidadProyecto WHERE ProyectosId IN (1, 2, 3, 4)");
        }
    }
}
