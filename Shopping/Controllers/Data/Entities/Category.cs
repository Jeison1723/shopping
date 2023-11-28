using System.ComponentModel.DataAnnotations;

namespace Shopping.Controllers.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        public string Name { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set;}

    }   
}
