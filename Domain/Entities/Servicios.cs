using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Servicios
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [MaxLength(500, ErrorMessage = "La descripcion no puede exceder los 500 caracteres")]
        public string Descripcion { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
