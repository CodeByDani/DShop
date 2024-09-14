namespace Catalog.API.Features.Product.CreateProduct;

public sealed partial class CreateProduct
{
    public sealed class ReqCommand : ICommand<ResCommand>
    {
        public string Name { get; set; }
        public List<string> Categories { get; set; }
        public string ImageFile { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
    }

    public sealed class ResCommand
    {
        public long Id { get; set; }
    }
}