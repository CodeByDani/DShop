using BuildingBlocks.Base;

namespace Catalog.API.Features.Product.UpdateProduct;

public sealed partial class UpdateProduct
{
    public sealed class ReqCommand : ICommand<ResCommand>
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public string ImageFile { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
    }

    public sealed class ResCommand : ResponseBaseService
    {
        public long Id { get; set; }
    }
}