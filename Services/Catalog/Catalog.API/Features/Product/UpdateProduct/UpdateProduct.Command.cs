namespace Catalog.API.Features.Product.UpdateProduct;

public sealed partial class UpdateProduct
{
    public sealed class ReqCommand : ICommand<ResCommand>
    {
        public long ProductId { get; set; }
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