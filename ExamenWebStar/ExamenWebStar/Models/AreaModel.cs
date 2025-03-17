using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamenWebStar.Models
{
    public class AreaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdArea { get; set; }

        [Required]
        public bool Activo { get; set; }
        [NotMapped]
        public int? CantidadEmpleados { get; set; }

        [Required]
        [StringLength(70)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public DateTime Alta { get; set; }

        [StringLength(150)]
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
