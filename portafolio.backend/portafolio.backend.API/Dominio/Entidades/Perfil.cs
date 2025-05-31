using Microsoft.AspNetCore.Http.HttpResults;
using System;

namespace portafolio.backend.API.Dominio.Entidades
{
    public class Perfil
    {

        public int Id { get; set; }
        public required UsuarioAdministrador UsuarioAdministrador { get; set; } 
        public int UsuarioAdministradorId { get; set; } 
        public string Nombre { get; set; } 
        public string Apellidos { get; set; } 
        public string Saludo { get; set; } 
        public string Descripcion { get; set; }
        public string AcercaDeMi { get; set; } 
        public string FotoURL { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
