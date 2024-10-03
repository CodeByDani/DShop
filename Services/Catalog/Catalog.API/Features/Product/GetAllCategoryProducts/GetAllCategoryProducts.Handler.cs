using Catalog.API.Enums;
using Catalog.API.Features.Category.Common;
using Catalog.API.Features.Category.Common.Interfaces;
using Catalog.API.Features.Product.Common;
using Catalog.API.Features.Product.Common.Interfaces;

namespace Catalog.API.Features.Product.GetAllCategoryProducts;

public sealed partial class GetAllCategoryProducts
{
    public sealed class QueryHandler : BaseQueryHandler<ReqQuery, ResQuery>
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public QueryHandler(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        protected override async Task<ResQuery> HandleCore(ReqQuery request, CancellationToken cancellationToken)
        {
            var category =
                await _categoryRepository.FirstOrDefaultAsync(p => p.Id == request.CategoryId, cancellationToken);
            if (category == null)
            {
                return Failure(Error.NotFound(nameof(CategoryMessages.NotFoundCategory),
                    CategoryMessages.NotFoundCategory));
            }

            var result = await _repository.FindWithPagination(
                p => p.Category.Id == request.CategoryId,
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                selector: p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageFile = p.ImageFile,
                    Price = p.Price,
                    Description = p.Description
                }, orderBy: p => p.Id,
                isDescending: request.SortDirection != SortDirection.Ascending,
                cancellationToken);
            if (result.Item1 == null)
            {
                return Failure(Error.NotFound(nameof(ProductMessages.NotFoundProduct),
                    ProductMessages.NotFoundProduct));
            }

            var products = result.Item1.Adapt<IReadOnlyList<GetAllCategoryProductsCommandRes>>();
            var totalCount = result.TotalCount;
            return new ResQuery
            {
                Products = products,
                TotalCount = totalCount
            };
        }
    }
}