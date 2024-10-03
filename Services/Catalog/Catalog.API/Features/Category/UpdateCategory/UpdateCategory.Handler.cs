using Catalog.API.Features.Category.Common;
using Catalog.API.Features.Category.Common.Interfaces;
using ErrorOr;

namespace Catalog.API.Features.Category.UpdateCategory;

public sealed partial class UpdateCategory
{
    public sealed class CommandHandler : BaseCommandHandler<ReqCommand, ResCommand>
    {
        private readonly ICategoryRepository _repository;

        public CommandHandler(ICategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<ResCommand> HandleCore(ReqCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository
                .FirstOrDefaultAsync(p => p.Id == request.CategoryId, cancellationToken);
            if (category == null)
            {
                return Failure(Error.NotFound(nameof(CategoryMessages.NotFoundCategory),
                    CategoryMessages.NotFoundCategory));
            }

            category = request.Adapt(category);

            await _repository.Update(category, cancellationToken);

            return new ResCommand { Id = category.Id };
        }
    }
}