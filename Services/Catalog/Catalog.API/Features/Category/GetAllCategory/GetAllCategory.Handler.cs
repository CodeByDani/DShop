using Catalog.API.Enums;
using Catalog.API.Features.Category.Common;
using Catalog.API.Features.Category.Common.Interfaces;
using ErrorOr;

namespace Catalog.API.Features.Category.GetAllCategory;

public sealed partial class GetAllCategory
{
    public sealed class QueryHandler : IQueryHandler<ReqQuery, ResQuery>
    {
        private readonly ICategoryRepository _repository;

        public QueryHandler(ICategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ErrorOr<ResQuery>> Handle(ReqQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllCategories();
            var result = categories.Adapt<IReadOnlyList<GetAllCategoriesCommandRes>>();
            return new ResQuery
            {
                Categories = result,
                TotalCount = result.Count
            };
        }
    }
}