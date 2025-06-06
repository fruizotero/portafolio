namespace portafolio.backend.API.Dominio.DTOs.Perfil
{
    public class PerfilRequestDTO
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Saludo { get; set; }
        public string Descripcion { get; set; }
        public string AcercaDeMi { get; set; }
        public IFormFile? Foto { get; set; }
    }
}
