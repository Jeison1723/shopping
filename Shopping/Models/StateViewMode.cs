using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class StateViewMode
    {
        public int Id { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        public string Name { get; set; }

        public int CountryId { get; set; }
    }
}
