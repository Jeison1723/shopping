using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Shopping.Controllers.Data.Entities
{
    public class TemporalSale
    {
        public int Id { get; set; } 

        public User user { get; set; }
        public Product product { get; set; }
        [DisplayFormat(DataFormatString ="{0:N2}")]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage ="EL campo {0} es obligatorio" )]

        public float Quantity { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = " Comentarios ")]
        public string ? Remarks { get; set; }
    }
}
