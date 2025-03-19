using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamenWebStar.Models
{
    public class EmpleadoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } 

        [Required]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El Correo Electronico es obligatorio.")]
        [StringLength(100)]
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        [Required]
        public int IdArea { get; set; }

        [Required]
        public DateTime Alta { get; set; }

        [NotMapped]
        public string? Area { get; set; }
    }

    public class EmpleadoModelDtos
    {
        public int IdEmpleado { get; set; }

        public string Nombre { get; set; } 

        public int Edad { get; set; }
        public DateTime Alta { get; set; }

        public string CorreoElectronico { get; set; }

        public int IdArea { get; set; }

        public string Area { get; set; }
    }
}
