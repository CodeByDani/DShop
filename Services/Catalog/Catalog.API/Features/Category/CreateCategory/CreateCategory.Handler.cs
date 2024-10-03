using Catalog.API.Features.Category.Common;
using Catalog.API.Features.Category.Common.Interfaces;

namespace Catalog.API.Features.Category.CreateCategory;

public sealed partial class CreateCategory
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
            var category = request.Adapt<Entities.Category>();
            if (category == null)
            {
                return Failure(Error.NotFound(nameof(CategoryMessages.NotFoundCategory),
                    CategoryMessages.NotFoundCategory));
            }
            await _repository.Store(category, cancellationToken);

            return new ResCommand { Id = category.Id };
        }
    }
}