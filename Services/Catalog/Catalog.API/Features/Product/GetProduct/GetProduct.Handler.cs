using Catalog.API.Features.Product.Common;
using Catalog.API.Features.Product.Common.Interfaces;
using ErrorOr;

namespace Catalog.API.Features.Product.GetProduct;

public sealed partial class GetProduct
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
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (product == null)
            {
                return Error.NotFound(nameof(ProductMessages.NotFoundProduct), ProductMessages.NotFoundProduct);
            }

            return new ResCommand
            {
                Categories = product.Categories,
                Description = product.Description,
                ImageFile = product.ImageFile,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}