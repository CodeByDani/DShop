namespace Catalog.API.Features.Product.GetProduct;

public sealed partial class GetProduct
{
    public sealed class GetEndPointResponse
    {
        public string Name { get; set; }
        public List<string> Categories { get; set; }
        public string ImageFile { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
    }
}