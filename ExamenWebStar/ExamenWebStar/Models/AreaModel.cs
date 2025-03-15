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

        [Required]
        [StringLength(70)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(150)]
        public string? Descripcion { get; set; }
    }
}
