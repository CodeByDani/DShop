using Catalog.API.Features.Category.Common;
using Catalog.API.Features.Category.Common.Interfaces;
using ErrorOr;

namespace Catalog.API.Features.Category.GetCategory;

public sealed partial class GetCategory
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
            var category = await _repository
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (category == null)
            {
                return Failure(Error.NotFound(nameof(CategoryMessages.NotFoundCategory),
                    CategoryMessages.NotFoundCategory));
            }

            return new ResQuery
            {
                Name = category.Name,
                Description = category.Description,
            };
        }
    }
}