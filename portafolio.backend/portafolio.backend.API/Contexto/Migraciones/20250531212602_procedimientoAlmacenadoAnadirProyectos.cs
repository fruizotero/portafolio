using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portafolio.backend.API.Contexto.Migraciones
{
    /// <inheritdoc />
    public partial class procedimientoAlmacenadoAnadirProyectos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Procedimiento almacenado para añadir proyectos iniciales
            migrationBuilder.Sql($@"
                CREATE PROCEDURE AnadirProyectosIniciales 
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    -- Proyectos destacados (orden más alto)
                    INSERT INTO Proyectos (UsuarioAdministradorId, Titulo, Descripcion, ImagenDesktopUrl, ImagenMobileUrl, RepositorioUrl, LiveUrl, Orden, CreatedAt, UpdatedAt)
                    VALUES 
                        (1, 
                        'Portfolio Personal', 
                        'Aplicación web full-stack desarrollada con React en el frontend y ASP.NET Core en el backend. Incluye autenticación JWT, sistema de administración de contenido y diseño responsive.',
                        'https://example.com/images/portfolio-desktop.jpg',
                        'https://example.com/images/portfolio-mobile.jpg',
                        'https://github.com/fruizotero/portfolio',
                        'https://fruizotero.info',
                        10,
                        GETDATE(), 
                        GETDATE()),
                        
                        (1, 
                        'E-commerce Demo', 
                        'Tienda online con catálogo de productos, carrito de compras y procesamiento de pagos. Frontend desarrollado con React y backend con ASP.NET Core API.',
                        'https://example.com/images/ecommerce-desktop.jpg',
                        'https://example.com/images/ecommerce-mobile.jpg',
                        'https://github.com/fruizotero/ecommerce-demo',
                        'https://ecommerce-demo.fruizotero.info',
                        5,
                        GETDATE(), 
                        GETDATE());
                        
                    -- Proyectos secundarios
                    INSERT INTO Proyectos (UsuarioAdministradorId, Titulo, Descripcion, ImagenDesktopUrl, ImagenMobileUrl, RepositorioUrl, LiveUrl, Orden, CreatedAt, UpdatedAt)
                    VALUES 
                        (1, 
                        'Gestor de Tareas', 
                        'Aplicación para gestionar tareas diarias con funcionalidades de creación, edición y eliminación. Permite organizar tareas por categorías y establecer fechas límite.',
                        'https://example.com/images/taskmanager-desktop.jpg',
                        'https://example.com/images/taskmanager-mobile.jpg',
                        'https://github.com/fruizotero/task-manager',
                        'https://tasks.fruizotero.info',
                        0,
                        GETDATE(), 
                        GETDATE()),
                        
                        (1, 
                        'API RESTful Demo', 
                        'API RESTful desarrollada con ASP.NET Core que implementa operaciones CRUD para diferentes entidades. Incluye validación, autenticación y documentación con Swagger.',
                        'https://example.com/images/api-desktop.jpg',
                        null,
                        'https://github.com/fruizotero/rest-api-demo',
                        null,
                        0,
                        GETDATE(), 
                        GETDATE());
                END
            ");

            // Invocar el procedimiento almacenado
            migrationBuilder.Sql("EXEC AnadirProyectosIniciales");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar el procedimiento almacenado
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AnadirProyectosIniciales");
        } 
    }
}
