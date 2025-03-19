using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamenWebStar.Models
{
    public class AreaModel
    {
        public int IdArea { get; set; }

        [Required]
        public bool Activo { get; set; }
        [NotMapped]
        public int? CantidadEmpleados { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(70, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public DateTime Alta { get; set; }


        [Required(ErrorMessage = "El Descripcion es obligatorio.")]
        [StringLength(150, ErrorMessage = "El Descripcion no puede superar los 100 caracteres.")]
        public string? Descripcion { get; set; }
    }

    public class AreaEmpleadoDto
    {
        public int IdArea { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime Alta { get; set; }
        public bool Activo { get; set; }
        public int? CantidadEmpleados { get; set; }
    }

}
