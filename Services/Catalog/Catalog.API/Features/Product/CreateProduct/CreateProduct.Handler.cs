using MediatR;

namespace Catalog.API.Features.Product.CreateProduct;

public sealed partial class CreateProduct
{
    public sealed class CommandHandler : IRequestHandler<ReqCommand, ResCommand>
    {
        public async Task<ResCommand> Handle(ReqCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return new ResCommand();
        }
    }
}