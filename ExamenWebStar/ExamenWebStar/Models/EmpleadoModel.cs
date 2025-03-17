using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamenWebStar.Models
{
    public class EmpleadoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public int Edad { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string CorreoElectronico { get; set; } = string.Empty;

        [Required]
        public int IdArea { get; set; }

        [NotMapped]
        public string? Area { get; set; }
    }

    public class EmpleadoModelDtos
    {
        public int IdEmpleado { get; set; }

        public string Nombre { get; set; } 

        public int Edad { get; set; }

        public string CorreoElectronico { get; set; }

        public int IdArea { get; set; }

        public string Area { get; set; }
    }
}
