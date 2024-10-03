using BuildingBlocks.Base;

namespace Catalog.API.Features.Product.GetProduct;

public sealed partial class GetProduct
{
    public sealed class ReqQuery : IQuery<ResQuery>
    {
        public long Id { get; set; }
    }
    public sealed class ResQuery : ResponseBaseService
    {
        public string Name { get; set; }
        public CategoryModelCommandRes Category { get; set; }
        public string ImageFile { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
    }
    public sealed class CategoryModelCommandRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}