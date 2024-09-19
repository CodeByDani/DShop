namespace Catalog.API.Features.Product.GetProduct;

public sealed partial class GetProduct
{
    public sealed class GetProductEndPointResponse
    {
        public string Name { get; set; }
        public ProductCategoryModelEndPointRes Category { get; set; }
        public string ImageFile { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
    }

    public sealed class ProductCategoryModelEndPointRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}