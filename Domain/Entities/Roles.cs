using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Roles
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        public bool Estados { get; set; }
    }
}
