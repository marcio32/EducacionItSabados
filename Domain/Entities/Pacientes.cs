using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pacientes
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public decimal Dni { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string? Email { get; set; }
        public decimal? Telefono { get; set; }
    }
}
