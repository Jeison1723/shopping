namespace Shopping.Models
{
    public class HomeViewModel
    {
        public ICollection<ProductHomeViewModel> Products { get; set; }

        public float Quantity { get; set; }
    }
}
