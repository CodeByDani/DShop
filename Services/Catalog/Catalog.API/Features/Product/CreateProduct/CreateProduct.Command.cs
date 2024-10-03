using BuildingBlocks.Base;
using BuildingBlocks.Behavior;

namespace Catalog.API.Features.Product.CreateProduct;

public sealed partial class CreateProduct
{
    public sealed class ReqCommand : ICommand<ResCommand>
    {
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