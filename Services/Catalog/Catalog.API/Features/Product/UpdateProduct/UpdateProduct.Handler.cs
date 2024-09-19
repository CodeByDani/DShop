using Catalog.API.Features.Product.Common;
using Catalog.API.Features.Product.Common.Interfaces;

namespace Catalog.API.Features.Product.UpdateProduct;

public sealed partial class UpdateProduct
{
    public sealed class CommandHandler : ICommandHandler<ReqCommand, ResCommand>
    {
        private readonly IProductRepository _repository;

        public CommandHandler(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ErrorOr<ResCommand>> Handle(ReqCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
            if (product == null)
            {
                return Error.NotFound(nameof(ProductMessages.NotFoundProduct), ProductMessages.NotFoundProduct);
            }

            product = request.Adapt(product);

            await _repository.Update(product, cancellationToken);

            return new ResCommand { Id = product.Id };
        }
    }
}