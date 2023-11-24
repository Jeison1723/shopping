using Shooping.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Controllers.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name= "Pais")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "No se permiten espacios en blanco.")]
        public string Name { get; set; }
        
        public ICollection<State> States { get; set; }

        [Display(Name = "Estados")]
        public int statesnumber => States == null ? 0 : States.Count;
    }
}


