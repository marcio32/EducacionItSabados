using Domain.Enums;

namespace Domain.Entities
{
    public class Turnos
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public EstadoTurno Estado { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioId { get; set; }
    }
}
