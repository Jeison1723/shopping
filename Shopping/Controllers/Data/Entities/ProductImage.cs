using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Shopping.Controllers.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Product Product { get; set; }
        [Display(Name ="Foto")]
        public Guid ImageId { get; set; }   

        public string ImageFullPath => ImageId == Guid.Empty
        ? $"https://localhost:7057/images/noimage.png"
        : $"https://shoppingzulu.blob.core.windows.net/products/{ImageId}";
    }
}
