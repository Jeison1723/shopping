namespace Shopping.Controllers.Data.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }

        public Product product { get; set; }
       
        public Category category { get; set; }
       
    }
}
