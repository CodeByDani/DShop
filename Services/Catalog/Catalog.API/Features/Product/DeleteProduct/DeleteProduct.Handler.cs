using Catalog.API.Features.Product.Common;
using Catalog.API.Features.Product.Common.Interfaces;

namespace Catalog.API.Features.Product.DeleteProduct;

public sealed partial class DeleteProduct
{
    public sealed class CommandHandler : BaseCommandHandler<ReqCommand, ResCommand>
    {
        private readonly IProductRepository _repository;

        public CommandHandler(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<ResCommand> HandleCore(ReqCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (product == null)
            {
                return Failure(Error.NotFound(nameof(ProductMessages.NotFoundProduct),
                    ProductMessages.NotFoundProduct));
            }
            await _repository.Delete(product, cancellationToken);

            return new ResCommand();
        }
    }
}