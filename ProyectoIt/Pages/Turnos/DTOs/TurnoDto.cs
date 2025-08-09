using Domain.Enums;

namespace WebUI.Pages.Turnos.DTOs
{
    public class TurnoDto
    {
        public int Id { get; set; }
        public int Dni { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateOnly FechaNacimiento { get; set; }
        public string Email { get; set; } = string.Empty;
        public int? Telefono { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond);
        public EstadoTurno Estado { get; set; }
        public int UsuarioId { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public int EstudioId { get; set; }
        public int DocumentosId { get; set; }
        public int EstadoId { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public IEnumerable<IFormFile>? Documentos { get; set; }
    }
}
