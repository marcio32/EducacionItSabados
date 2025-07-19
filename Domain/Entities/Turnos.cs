using Domain.Enums;

namespace Domain.Entities
{
    public class Turnos
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public EstadoTurno Estado { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int Usuario_Id { get; set; }
        public int  Medico_Id { get; set; }
        public int Paciente_Id { get; set; }
        public Medicos? Medico { get; set; } 
        public Usuarios? Usuario { get; set; } 
        public Pacientes? Pacientes { get; set; } 

    }
}
