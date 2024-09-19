using Catalog.API.Features.Category.Common;
using Catalog.API.Features.Category.Common.Interfaces;
using ErrorOr;

namespace Catalog.API.Features.Category.GetCategory;

public sealed partial class GetCategory
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
            var category = await _repository
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (category == null)
            {
                return Error.NotFound(nameof(CategoryMessages.NotFoundCategory), CategoryMessages.NotFoundCategory);
            }

            return new ResQuery
            {
                Name = category.Name,
                Description = category.Description,
            };
        }
    }
}