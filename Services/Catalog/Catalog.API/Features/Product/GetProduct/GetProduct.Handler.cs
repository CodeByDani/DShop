using Catalog.API.Features.Product.Common;
using Catalog.API.Features.Product.Common.Interfaces;

namespace Catalog.API.Features.Product.GetProduct;

public sealed partial class GetProduct
{
    public sealed class QueryHandler : BaseQueryHandler<ReqQuery, ResQuery>
    {
        private readonly IProductRepository _repository;

        public QueryHandler(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<ResQuery> HandleCore(ReqQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (product == null)
            {
                return Failure(Error.NotFound(nameof(ProductMessages.NotFoundProduct),
                    ProductMessages.NotFoundProduct));
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