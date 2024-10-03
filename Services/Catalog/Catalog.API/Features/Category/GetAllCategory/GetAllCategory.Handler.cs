using Catalog.API.Enums;
using Catalog.API.Features.Category.Common;
using Catalog.API.Features.Category.Common.Interfaces;
using ErrorOr;

namespace Catalog.API.Features.Category.GetAllCategory;

public sealed partial class GetAllCategory
{
    public sealed class QueryHandler : BaseQueryHandler<ReqQuery, ResQuery>
    {
        private readonly ICategoryRepository _repository;

        public QueryHandler(ICategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<ResQuery> HandleCore(ReqQuery request, CancellationToken cancellationToken)
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