namespace portafolio.backend.API.Dominio.DTOs
{
    public class ApiResponseDTO<T>
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public T Datos { get; set; }
        public int CodigoEstado { get; set; }
    }
}
