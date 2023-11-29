using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class AddProductImagens
    {
        public int ProductId { get; set; }
        [Display(Name = "Foto")]
        [Required(ErrorMessage =" El Campo {0} es Obligatorio.")]
        
        public IFormFile ImageFile { get; set; }
    }
}
