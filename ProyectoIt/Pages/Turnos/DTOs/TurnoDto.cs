using Domain.Enums;

namespace WebUI.Pages.Turnos.DTOs
{
    public class TurnoDto
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public EstadoTurno Estado { get; set; }
        public int UsuarioId { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public IEnumerable<IFormFile>? Documentos { get; set; }
    }
}
