using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portafolio.backend.API.Contexto.Migraciones
{
    /// <inheritdoc />
    public partial class procedimientoAlamacenadoAnadirPerfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // Procedimiento almacenado para añadir un perfil
            migrationBuilder.Sql($@"
                CREATE PROCEDURE AnadirPerfil 
                AS
                BEGIN
                    SET NOCOUNT ON;
                    INSERT INTO Perfiles (UsuarioAdministradorId, Nombre, Apellidos, Saludo, Descripcion, AcercaDeMi, FotoURL, CreatedAt, UpdatedAt)
                    VALUES (1, 
                            'frank edgar', 
                            'ruiz otero', 
                            'Hola, soy Frank.', 
                            '<p>Soy un Desarrollador full-stack, con conocimiento de React para el front-end. Mi enfoque actual se centra en la creación de experiencias de usuario dinámicas y atractivas. </p><p>En cuanto al back-end, tengo conocimientos en Java y PHP. Aunque puedo trabajar eficientemente con ambos, mi destreza actual se inclina más hacia la creación de APIs utilizando PHP. Estoy comprometido a ampliar mis conocimientos y adaptarme a las tecnologías más actuales para ofrecer mejores soluciones.</p>',
'<p><strong> Hola, soy Frank Ruiz </strong>, Desarrollador Multiplataforma & Web con conocimientos tanto de back como de front. En el último año, he dedicado mi tiempo a perfeccionar mis habilidades en la creación de interfaces atractivas y funcionales a partir de diseños. Mi enfoque principal ha sido construir CRUDs y APIs para el backend, consolidando así una base sólida en mi camino de aprendizaje.</p><p>Si bien tengo planes de adentrarme en frameworks en el futuro, actualmente considero crucial afianzar mis conocimientos fundamentales. Mi objetivo es crecer de manera constante y contribuir al éxito de proyectos desafiantes.</p><p>Fuera del mundo de la programación, disfruto de mis momentos de ocio leyendo mangas y libros. Además, encuentro relajación editando cómics con Photoshop. En ocasiones, me sumerjo en la actividad de subtitulación para agregar variedad a mis pasatiempos.</p><p>En cuanto a idiomas, mi habilidad en inglés es lo suficientemente competente como para leer documentación y comprender respuestas en plataformas como Stack Overflow.</p><p>Estoy ansioso por colaborar con un equipo dinámico que comparta mi entusiasmo y seguir perfeccionando mis habilidades. Si estás interesado en conectarte conmigo, por favor, no dudes en utilizar cualquiera de los medios de contacto que he proporcionado en la sección correspondiente.</p>', 

                            'https://fruizotero.info/assets/img/images/profile_bg_filtro.jpg', 
                            GETDATE(), GETDATE());
                END
            ");

            // Invocar el procedimiento almacenado para añadir un perfil por defecto
            migrationBuilder.Sql("EXEC AnadirPerfil");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar el procedimiento almacenado si es necesario
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AnadirPerfil");
        }
    }
}
