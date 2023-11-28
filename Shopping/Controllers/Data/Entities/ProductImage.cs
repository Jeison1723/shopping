using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Shopping.Controllers.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Product Product { get; set; }
        [Display(Name = "Foto")]
        public byte[] Imagefile { get; set; }   


    }
}
