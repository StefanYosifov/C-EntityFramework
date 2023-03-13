namespace ProductShop.DTOs.Import
{
    using ProductShop.Models;

    public class ImportCategoryProductsDto
    {

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

    }
}
