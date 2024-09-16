namespace Catalog.API.Features.Product.DeleteProduct;

public sealed partial class DeleteProduct
{
    public sealed class ReqCommand : ICommand<ResCommand>
    {
        public long Id { get; set; }
    }
    public sealed class ResCommand
    {
    }
}