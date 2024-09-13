namespace Catalog.API.Features.Product.CreateProduct;

public sealed partial class CreateProduct
{
    public string Name { get; set; }
    public List<long> CategoriesId { get; set; }
    public string ImageFile { get; set; }
    public Decimal Price { get; set; }
    public string Description { get; set; }
}