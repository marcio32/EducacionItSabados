using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Turnos
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int UsuarioId { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public int EstadoId { get; set; }
        public int DocumentosId { get; set; }
        public int EstudioId { get; set; }
        public Estados? Estado { get; set; }
        public Medicos? Medico { get; set; } 
        public Usuarios? Usuario { get; set; } 
        public Pacientes? Paciente { get; set; }
        public Estudios? Estudio { get; set; }
        public ICollection<Documentos> Documentos { get; set; } = new List<Documentos>();
    }
}
