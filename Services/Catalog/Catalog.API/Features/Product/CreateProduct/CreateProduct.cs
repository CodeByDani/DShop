namespace Catalog.API.Features.Product.CreateProduct;

public sealed partial class CreateProduct
{
    public string Name { get; set; }
    public List<string> Categories { get; set; }
    public string Image { get; set; }
    public Decimal Price { get; set; }
    public string Description { get; set; }
}