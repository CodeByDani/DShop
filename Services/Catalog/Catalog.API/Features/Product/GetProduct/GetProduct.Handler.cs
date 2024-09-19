using Catalog.API.Features.Product.Common;
using Catalog.API.Features.Product.Common.Interfaces;

namespace Catalog.API.Features.Product.GetProduct;

public sealed partial class GetProduct
{
    public sealed class QueryHandler : IQueryHandler<ReqQuery, ResQuery>
    {
        private readonly IProductRepository _repository;

        public QueryHandler(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ErrorOr<ResQuery>> Handle(ReqQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (product == null)
            {
                return Error.NotFound(nameof(ProductMessages.NotFoundProduct), ProductMessages.NotFoundProduct);
            }

            return new ResQuery
            {
                Category = product.Category.Adapt<CategoryModelCommandRes>(),
                Description = product.Description,
                ImageFile = product.ImageFile,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}